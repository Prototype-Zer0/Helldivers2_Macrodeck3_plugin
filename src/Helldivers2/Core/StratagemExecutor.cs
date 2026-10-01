using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Helldivers2.Core;

/// <summary>
/// Executes Helldivers 2 stratagem input sequences using the Windows keyboard API.
/// </summary>
/// <remarks>
/// This class is intentionally Windows-specific. The rest of the plugin uses
/// platform-independent stratagem types, allowing a different executor to be
/// introduced later for macOS/Linux.
/// </remarks>
public static class StratagemExecutor
{
    private static readonly Random Random = new();
    private static readonly object SyncRoot = new();

    private const int MinKeyPressTime = 55;
    private const int MaxKeyPressTime = 105;
    private const int MinBetweenPresses = 35;
    private const int MaxBetweenPresses = 75;

    private static readonly Dictionary<StratagemDirection, KeySpec> KeyMap = new()
    {
        { StratagemDirection.Up, new KeySpec(0x26, 0x48, true) },
        { StratagemDirection.Down, new KeySpec(0x28, 0x50, true) },
        { StratagemDirection.Left, new KeySpec(0x25, 0x4B, true) },
        { StratagemDirection.Right, new KeySpec(0x27, 0x4D, true) },
    };

    private static readonly KeySpec LeftControlKey = new(0xA2, 0x1D, false);

    public static void Execute(StratagemDefinition stratagem)
    {
        ArgumentNullException.ThrowIfNull(stratagem);

        lock (SyncRoot)
        {
            Thread.Sleep(Random.Next(50, 155));

            // Hold Left Ctrl while entering the stratagem sequence.
            KeyEvent(LeftControlKey, keyUp: false);

            try
            {
                foreach (var direction in stratagem.Sequence)
                {
                    if (!KeyMap.TryGetValue(direction, out var spec))
                    {
                        continue;
                    }

                    Thread.Sleep(
                        Random.Next(MinBetweenPresses, MaxBetweenPresses));

                    // Press direction.
                    KeyEvent(spec, keyUp: false);

                    // Hold direction briefly.
                    Thread.Sleep(
                        Random.Next(MinKeyPressTime, MaxKeyPressTime));

                    // Release direction.
                    KeyEvent(spec, keyUp: true);
                }

                Thread.Sleep(
                    Random.Next(MinBetweenPresses, MaxBetweenPresses));
            }
            finally
            {
                // Never leave Ctrl stuck down if execution fails.
                KeyEvent(LeftControlKey, keyUp: true);
            }
        }
    }

    private static void KeyEvent(KeySpec spec, bool keyUp)
    {
        uint flags = spec.Extended
            ? KEYEVENTF_EXTENDEDKEY
            : 0;

        if (keyUp)
        {
            flags |= KEYEVENTF_KEYUP;
        }

        keybd_event(
            spec.VirtualKey,
            spec.ScanCode,
            flags,
            UIntPtr.Zero);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern void keybd_event(
        byte bVk,
        byte bScan,
        uint dwFlags,
        UIntPtr dwExtraInfo);

    private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    private readonly struct KeySpec
    {
        public KeySpec(byte virtualKey, byte scanCode, bool extended)
        {
            VirtualKey = virtualKey;
            ScanCode = scanCode;
            Extended = extended;
        }

        public byte VirtualKey { get; }

        public byte ScanCode { get; }

        public bool Extended { get; }
    }
}
