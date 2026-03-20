# AmigaKlang

AmigaKlang is a modular sample synthesizer for Amiga and Atari ST musicians. It renders instruments offline via a node graph (similar to 4klang) and exports them to ProTracker-compatible samples, MOD templates, WAV files, or text descriptions. This repository contains the original WinForms tool plus all supporting assets (fonts, cursors, icons) required to build it from source.

## Features

- Up to 16 slots per instrument with freely routable node graphs
- 25+ synthesis, filter, envelope, and utility nodes including vocoder, comb, and reverb blocks
- Amiga PAL clock playback model with real-time preview, waveform display, zoom, and loop markers
- Sample import pipeline (WAV/RAW) with optional delta encoding for small executables
- Exporters for `.akp` patch banks, `.aki` single instruments, `.mod` templates, `.wav`, and `.txt`
- Optional Atari ST palette, cursors, and toolbar for cross-platform demo aesthetics

## Repository Layout

```
AmigaKlang/
├── README.md
├── .gitignore
├── AmigaklangGUI.sln          # Visual Studio solution (WinForms)
├── AmigaKlangGUI/             # C# source, resources, and project file
│   ├── Properties/
│   ├── Resources/
│   ├── App.config, app.manifest
│   └── *.cs, *.Designer.cs, *.resx
├── Topaznew.ttf               # Amiga Topaz font
├── amiga.cur / atari.cur      # Pointer themes
├── busy.cur / wait.cur        # Hourglass cursors
├── pointer.cur / pointer_help.cur
└── trashcan.png               # UI icon asset
```

All build outputs, `.vs/` caches, `bin/`, `obj/`, and NuGet `packages/` folders are intentionally omitted. Restore packages on demand (see below) to keep the public repo lean.

## Building

### Requirements

- Windows with Visual Studio 2022 (Community Edition is sufficient)
- .NET Framework 4.7.2 Developer Pack
- NuGet packages defined in `AmigaKlangGUI/packages.config` (currently NAudio 1.10.0)

### Steps

1. Clone this repository:
   ```powershell
   git clone https://github.com/virgill1974/AmigaKlang.git
   cd AmigaKlang
   ```
2. Restore NuGet packages (Visual Studio will prompt automatically, or run `nuget restore AmigaklangGUI.sln`).
3. Open `AmigaklangGUI.sln` in Visual Studio.
4. Build the solution in Debug or Release.
5. Run the resulting `AmigaKlangGUI.exe` from `bin/<config>/`.

### Assets

The cursor, icon, and font files at the repo root must remain next to the executable if you copy it outside the build folder. The WinForms project references them via relative paths.

## Packaging & Distribution

- Keep generated `.exe` or `.mod` exports out of the repository.
- If you need to share binaries, create GitHub Releases or upload archives separately.
- When adding new assets (fonts, images), place them in the repo root or under `AmigaKlangGUI/Resources/` and update the project file accordingly.

## Contributing

1. Fork the repository.
2. Create a feature branch (`feature/waveform-zoom`).
3. Work on the C# sources; keep resources embedded via `.resx` when possible.
4. Ensure the solution builds on a clean machine (after `git clean -xfd`).
5. Open a pull request with a concise summary plus screenshots, audio snippets, or MOD examples if helpful.

## License

To be defined. Until a license is published, treat the code as "all rights reserved" and request permission before redistributing binaries or using it commercially.
