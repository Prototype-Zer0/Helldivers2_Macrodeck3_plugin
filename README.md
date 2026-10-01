Helldivers 2 Stratagems — Macro Deck 3 Plugin

A Macro Deck 3
 plugin for executing Helldivers 2 stratagem input sequences from Macro Deck.

Platform: Windows
Target framework: .NET 10
Macro Deck: 3.x

Features

Execute Helldivers 2 stratagems directly from Macro Deck.

Select the desired stratagem through a Macro Deck action parameter.

Supports the standard Helldivers 2 directional stratagem inputs.

Uses randomized input timing between key presses.

Includes localization infrastructure for future translations.

Packaged as a .macroDeckPlugin file for installation.

Requirements

To build the plugin, install:

.NET 10 SDK

Macro Deck 3

The plugin currently targets Windows because keyboard input is implemented through the Windows user32.dll keyboard API.

Cross-platform support may be added in the future.

Repository Structure
.
├── Directory.Build.props
├── Directory.Packages.props
├── NuGet.config
├── README.md
└── src/
    └── Helldivers2/
        ├── Actions/
        ├── Assets/
        ├── Core/
        ├── Localization/
        ├── Helldivers2.csproj
        ├── macrodeck-build.json
        └── manifest.json


Build output is intentionally excluded from source control.

Clone the Repository
git clone <repository-url>
cd Helldivers2_Macrodeck3_plugin

Restore Dependencies

From the repository root:

dotnet restore


NuGet package versions are centrally managed through Directory.Packages.props.

Compile

Navigate to the plugin project:

cd src\Helldivers2


Then build:

dotnet build


For a Release build:

dotnet build -c Release


The compiled assembly will be placed under:

src/Helldivers2/bin/Release/net10.0/

Publish

To create a normal Release publish directory:

dotnet publish


The default output will be under:

src/Helldivers2/bin/Release/net10.0/publish/


The published directory contains the plugin executable, assemblies, manifest, assets, and localization files required by the plugin.

Build a Macro Deck Plugin Package

The repository uses the Macro Deck plugin build configuration in macrodeck-build.json.

Install the Macro Deck plugin CLI if it is not already available, then verify it is accessible:

macrodeck-plugin --version


From the plugin directory, build the installable package with:

macrodeck-plugin build --source . --output .\artifacts


A successful build produces a file similar to:

artifacts/
└── stratagems.helldivers2-1.0.0.macroDeckPlugin


The .macroDeckPlugin file is the distributable/installable plugin package.

Target Platforms

The build configuration currently defines these targets:

win-x64

osx-arm64

linux-x64

The plugin's actual stratagem keyboard execution is currently Windows-specific. The non-Windows targets are therefore placeholders for future cross-platform implementation.

Installing the Plugin

After creating the .macroDeckPlugin package, install it through Macro Deck 3 using the plugin installation mechanism.

The package contains the plugin manifest and published runtime files required by Macro Deck.

Updating the Version

The plugin version is defined in:

src/Helldivers2/manifest.json


For example:

"version": "1.0.0"


Update this value when preparing a new release.

After changing the version, rebuild the package:

macrodeck-plugin build --source . --output .\artifacts


The resulting package will use the version from manifest.json.

Localization

Localization resources live in:

src/Helldivers2/Localization/


The default resource file is:

Strings.resx


Additional languages can be added using the appropriate culture suffix, for example:

Strings.de.resx
Strings.pt-BR.resx


The Macro Deck localization source generator creates the strongly typed Strings class during compilation.

Generated files should not be committed to the repository.

Building Against a Local Macro Deck SDK

The repository supports overriding the centrally managed Macro Deck SDK version.

For example:

dotnet build -p:MacroDeckSdkVersion=3.0.0


If using a locally packed SDK, place the package in:

local-feed/


The local feed is intentionally ignored by Git.

This allows development against an unreleased/local Macro Deck SDK without committing local NuGet packages to the repository.

Clean Build

If the build gets into an unexpected state, clean the generated files:

dotnet clean


For a more thorough cleanup, remove bin and obj:

Remove-Item -Recurse -Force .\bin, .\obj -ErrorAction SilentlyContinue


Then restore and build again:

dotnet restore
dotnet build

Git / Generated Files

The following directories are generated and should not be committed:

**/bin/
**/obj/
**/publish/
**/artifacts/
local-feed/


Build configuration, source code, manifests, localization resources, and plugin assets should remain tracked.

License

This project is licensed under the MIT License.
