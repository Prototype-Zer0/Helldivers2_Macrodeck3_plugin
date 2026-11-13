using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace Helldivers2.Core;

public static class StratagemRegistry
{
    private static readonly ImmutableDictionary<string, StratagemDefinition> Definitions =
        BuildDefinitions();

    private static ImmutableDictionary<string, StratagemDefinition> BuildDefinitions()
    {
        var builder = ImmutableDictionary.CreateBuilder<string, StratagemDefinition>(
            StringComparer.OrdinalIgnoreCase
        );

        foreach (var definition in new[]
        {
        CreateDefinition("A/AC-8 Autocannon Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up),
        CreateDefinition("A/ARC-3 Tesla Tower", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("A/FLAM-40 Flame Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("A/G-16 Gatling Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Left),
        CreateDefinition("A/GM-17 Gas Mortar Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left),
        CreateDefinition("A/LAS-98 Laser Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("A/M-12 Mortar Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("A/M-23 EMS Mortar Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("A/MG-43 Machine Gun Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("A/MLS-4X Rocket Sentry", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Left),
        CreateDefinition("AC-8 Autocannon", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("Activate E-711 Extraction Drill", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("AMW-20 Apex", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Left),
        CreateDefinition("APW-1 Anti-Materiel Rifle", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Down),
        CreateDefinition("Aquifer Drill", StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("ARC-3 Arc Thrower", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Left),
        CreateDefinition("AX/AR-23 Guard Dog", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("AX/ARC-3 K-9", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Left),
        CreateDefinition("AX/FLAM-75 Hot Dog", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Left),
        CreateDefinition("AX/LAS-5 Rover", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Right),
        CreateDefinition("AX/TX-13 Dog Breath", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("B-1 Supply Pack", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down),
        CreateDefinition("B-100 Portable Hellbomb", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("B/FLAM-80 Cremator", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("B/MD C4 Pack", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("Call In Super Destroyer", StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("Cargo Container", StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("CQC-1 One True Flag", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("CQC-20 Breaching Hammer", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Up),
        CreateDefinition("CQC-9 Defoliation Tool", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("Dark Fluid Vessel", StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("E/AT-12 Anti-Tank Emplacement", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Right),
        CreateDefinition("E/GL-21 Grenadier Battlement", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("E/MG-101 HMG Emplacement", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Left),
        CreateDefinition("Eagle 110mm Rocket Pods", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left),
        CreateDefinition("Eagle 500kg Bomb", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("Eagle Airstrike", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("Eagle Cluster Bomb", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("Eagle Fly By", StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("Eagle Gas Airstrike", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("Eagle Napalm Airstrike", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("Eagle Rearm", StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("Eagle Smoke Strike", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Down),
        CreateDefinition("Eagle Strafing Run", StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Right),
        CreateDefinition("EAT-17 Expendable Anti-Tank", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("EAT-411 Leveller", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down),
        CreateDefinition("EAT-700 Expendable Napalm", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Left),
        CreateDefinition("EXO-45 Patriot Exosuit", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("EXO-49 Emancipator Exosuit", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("EXO-51 Lumberer Exosuit", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Up),
        CreateDefinition("EXO-55 Breakthrough Exosuit", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("FAF-14 Spear", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("FLAM-40 Flamethrower", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("FX-12 Shield Generator Relay", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("GL-21 Grenade Launcher", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Down),
        CreateDefinition("GL-28 Belt-Fed Grenade Launcher", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("GL-52 De-Escalator", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("GR-8 Recoilless Rifle", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Left),
        CreateDefinition("Hive Breaker Drill", StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("LAS-44 Nova", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("LAS-98 Laser Cannon", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left),
        CreateDefinition("LAS-99 Quasar Cannon", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("LIFT-182 Warp Pack", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("LIFT-850 Jump Pack", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("Hover Pack", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("M-1000 Maxigun", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("M-102 Gunner FRV", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("M-103 Supply FRV", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("M-104 Incinerator FRV", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("M-105 Stalwart", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Left),
        CreateDefinition("MD-17 Anti-Tank Mines", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("MD-6 Anti-Personnel Minefield", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("MD-8 Gas Mines", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("MD-I4 Incendiary Mines", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Down),
        CreateDefinition("MG-206 Heavy Machine Gun", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("MG-43 Machine Gun", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("MGX-42 Bullet Storm", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Left),
        CreateDefinition("MLS-4X Commando", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("MS-11 Solo Silo", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("Hellbomb", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("Orbital 120mm HE Barrage", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("Orbital 380mm HE Barrage", StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("Orbital Airburst Strike", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Right),
        CreateDefinition("Orbital Confetti", StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("Orbital EMS Strike", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Down),
        CreateDefinition("Orbital Gas Strike", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("Orbital Gatling Barrage", StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("Orbital Illumination Flare", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Left),
        CreateDefinition("Orbital Laser", StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("Orbital Napalm Barrage", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("Orbital Precision Strike", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("Orbital Railcannon Strike", StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("Orbital Smoke Strike", StratagemDirection.Right, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("Orbital Walking Barrage", StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("PLAS-45 Epoch", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("Portable Comms Relay", StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Down),
        CreateDefinition("Prospecting Drill", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("Cargo Container", StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down),
        CreateDefinition("Reinforce", StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Up),
        CreateDefinition("Link Hellpods to Destroyer", StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("Resupply", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("RL-77 Airburst Rocket Launcher", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("RS-422 Railgun", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("S-11 Speargun", StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Right),
        CreateDefinition("SEAF Artillery", StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Down),
        CreateDefinition("Seismic Probe", StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down),
        CreateDefinition("SSSD Delivery", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("Super Earth Flag", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("SH-20 Ballistic Shield Backpack", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left),
        CreateDefinition("SH-32 Shield Generator Pack", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("SH-51 Directional Shield", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("SoS Beacon", StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Up),
        CreateDefinition("Super Earth Flag", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("SSSD Delivery", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("SSSD Delivery", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Up),
        CreateDefinition("StA-X3 W.A.S.P. Launcher", StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Right),
        CreateDefinition("Super Earth Flag", StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("Tactical Video Camera", StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Left, StratagemDirection.Up),
        CreateDefinition("TD-110 Maelstrom", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Left, StratagemDirection.Right),
        CreateDefinition("TD-220 Bastion MK XVI", StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Right, StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up),
        CreateDefinition("Tectonic Drill", StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Up, StratagemDirection.Down),
        CreateDefinition("TX-41 Sterilizer", StratagemDirection.Down, StratagemDirection.Left, StratagemDirection.Up, StratagemDirection.Down, StratagemDirection.Left),
        CreateDefinition("Upload Data", StratagemDirection.Left, StratagemDirection.Right, StratagemDirection.Up, StratagemDirection.Up, StratagemDirection.Up)
        })

        {
            builder[definition.Id] = definition;
        }

        return builder.ToImmutable();
    }

    private static StratagemDefinition CreateDefinition(
        string displayName,
        params StratagemDirection[] sequence
    )
    {
        var id = Regex.Replace(displayName, "[^A-Za-z0-9]+", "-").Trim('-');

        if (string.IsNullOrWhiteSpace(id))
        {
            id = Guid.NewGuid().ToString("N");
        }

        return new StratagemDefinition(id, displayName, sequence.ToImmutableArray());
    }

    public static StratagemDefinition Default =>
        Definitions.TryGetValue("Reinforce", out var stratagem)
            ? stratagem
            : Definitions.Values.First();

    public static IEnumerable<StratagemDefinition> GetAll() =>
        Definitions.Values.OrderBy(s => s.DisplayName);

    public static bool TryGet(string? id, out StratagemDefinition stratagem)
    {
        if (
            !string.IsNullOrWhiteSpace(id)
            && Definitions.TryGetValue(id, out var found)
            && found is not null
        )
        {
            stratagem = found;
            return true;
        }

        stratagem = Default;
        return false;
    }
}
