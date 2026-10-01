# Helldivers 2 Macro Deck 3 Plugin

A Macro Deck 3 plugin for executing Helldivers 2 stratagems from Macro Deck. The plugin provides a selectable list of Helldivers 2 stratagems and sends the corresponding directional input sequence when the action is triggered.

---

## Requirements

* **Macro Deck 3**
* **.NET 10 SDK**
* **Windows** (required for the current `user32.dll` keyboard input implementation)
* The Macro Deck 3 SDK packages used by the project

The plugin currently targets:
```xml
net10.0
```

---

## Repository Structure

```text
.
├── Directory.Build.props
├── Directory.Packages.props
├── NuGet.config
├── .gitignore
├── README.md
└── src
    └── Helldivers2
        ├── Actions
        │   └── HelldiversStratagemAction.cs
        ├── Assets
        │   └── helldivers2.png
        ├── Core
        │   ├── StratagemDefinition.cs
        │   ├── StratagemDirection.cs
        │   ├── StratagemExecutor.cs
        │   └── StratagemRegistry.cs
        ├── Localization
        │   └── Strings.resx
        ├── Helldivers2.csproj
        ├── PluginIntegration.cs
        ├── Program.cs
        ├── macrodeck-build.json
        └── manifest.json
```

---

## Building

1. Clone the repository and change into the plugin project directory:
   ```bash
   cd src\Helldivers2
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

A successful build should produce output similar to:
```text
Restore complete
Helldivers2 net10.0 succeeded
Build succeeded
```

The compiled plugin assembly will be located under:
```text
bin\Debug\net10.0\
```

---

## Publishing

For a Release publish:
```bash
dotnet publish
```

The published files will be placed under:
```text
bin\Release\net10.0\publish\
```

The publish directory contains the plugin executable, assemblies, manifest, assets, and localization files required by the plugin.

---

## Creating a Macro Deck Plugin Package

The repository uses the Macro Deck plugin build configuration located in:
```text
src\Helldivers2\macrodeck-build.json
```

After installing the Macro Deck plugin tooling, run:
```bash
macrodeck-plugin build --source . --output .\artifacts
```

This command builds the configured runtime target and creates a `.macroDeckPlugin` package in the `artifacts` directory, for example:
```text
artifacts\
└── stratagems.helldivers2-1.0.0.macroDeckPlugin
```
This file is your installable Macro Deck plugin package.

---

## Runtime Targets

The build configuration currently defines the following targets:
* `win-x64`
* `osx-arm64`
* `linux-x64`

> **Note:** The project currently contains Windows-specific keyboard input code, so only the Windows target is functional at this time. macOS and Linux targets are included in the packaging configuration for future cross-platform support.

---

## Installing the Plugin

Once the `.macroDeckPlugin` package has been created, install it directly through Macro Deck. 
The package contains the plugin manifest and published files required by Macro Deck. The plugin manifest itself is located at:
```text
src\Helldivers2\manifest.json
```

---

## Adding or Modifying Stratagems

Stratagem definitions are maintained in:
```text
src\Helldivers2\Core\StratagemRegistry.cs
```

Each stratagem consists of:
* An identifier
* A display name
* A directional input sequence

The available directions are defined by:
```text
src\Helldivers2\Core\StratagemDirection.cs
```

The enum currently contains:
```csharp
public enum StratagemDirection
{
    Up,
    Down,
    Left,
    Right,
}
```
The executor converts these directions into keyboard input whenever a stratagem action is triggered.

---

## Localization

Localization resources are stored under:
```text
src\Helldivers2\Localization\
```
* **Default resource file:** `Strings.resx`

Additional translations can be added using the standard `.resx` naming convention:
* `Strings.de.resx`
* `Strings.fr.resx`
* `Strings.pt-BR.resx`

The Macro Deck localization source generator automatically generates the strongly typed `Strings` class during the build process.

---

## Configuration

Package versions are managed centrally in:
```text
Directory.Packages.props
```

The project uses the `MacroDeckSdkVersion` property to control the Macro Deck SDK package version. The default configuration uses:
```xml
<MacroDeckSdkVersion>3.0.0-*</MacroDeckSdkVersion>
```

A specific SDK version can also be supplied during a build:
```bash
dotnet build -p:MacroDeckSdkVersion=3.0.0
```

### Local Macro Deck SDK Builds
`NuGet.config` includes a local package feed:
```text
./local-feed
```
This can be used when developing against an unreleased or locally built Macro Deck SDK. Place locally built NuGet packages in `local-feed\` and build the project normally.

---

## Cleaning the Project

To remove generated build output:
```bash
dotnet clean
```

To completely remove the generated `bin` and `obj` directories:
```powershell
Get-ChildItem -Path . -Directory -Recurse -Force |
    Where-Object { $_.Name -in @("bin", "obj") } |
    Remove-Item -Recurse -Force
```

After cleaning, restore and build again:
```bash
dotnet restore
dotnet build
```

---

## Git and Generated Files

Build output and generated files should not be committed to the repository. Specifically, the following directories are generated and ignored:
* `bin\`
* `obj\`
* `artifacts\`

The repository `.gitignore` contains the project-specific exclusions.

---

## Development Workflow

A typical development cycle looks like this:
```bash
cd src\Helldivers2

dotnet restore
dotnet build
dotnet publish
macrodeck-plugin build --source . --output .\artifacts
```
The final installable package can then be found in the `artifacts\` folder.

---

## Current Limitations

* Keyboard input is currently implemented using Windows APIs (`user32.dll`).
* macOS support is not currently implemented.
* Linux/BSD support is not currently implemented.
* The plugin currently targets .NET 10.
* Cross-platform input support will require platform-specific input modules.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for the full license text.

---

## Repository

Source code and development history are available on GitHub:
[https://github.com/Prototype-Zer0/Helldivers2_Macrodeck3_plugin](https://github.com/Prototype-Zer0/Helldivers2_Macrodeck3_plugin)
