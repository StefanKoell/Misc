## WinForms on ARM64 with .NET 7

### Abstract
WinForms was introduced 20 years ago in 2002 with the release of .NET Framework 1.0. It's a "thin wrapper" around the native Win32 APIs, which is still the foundation of millions of apps - including first-party Microsoft apps which ship with Windows. It enables rapid app development in VB.NET and C# and is still used in open-source, commercial, and internal apps around the globe.

One such commercial app is **Royal TS**, a WinForms app, written in C#. **Royal TS** has been available since 2003 and combines multiple remote management technologies in a modern user interface which kind of looks like a cross-over between Office and Visual Studio. Even though Royal TS is also almost 20 years old, it has constantly been updated to keep the technical debt at a minimum. Moved from .NET Framework to .NET 5, 6, and 7 on launch date, incorporating Dependency Injection, In-App Pub/Sub Messaging, HighDPI support, SVG-based icons, UI theming, Royal TS has been kept up to date and modern all the way.

As of .NET 7, Microsoft officially supports ARM64 for WinForms and WPF. So, can I just compile the app for the ARM64 platform and everything will work? Why not just compile to "Any CPU"? There are some caveats, especially when dealing with 3rd party dependencies which utilize native code targeted for x64 platforms.

Also, why would anyone want to compile the app for the ARM64 platform in the first place? What are the benefits? What about the Intel Bridge Technology on Windows 11? Which hardware runs Windows on ARM?

In this talk, I will share my experience, pitfalls, and learnings, focusing on the x64 to ARM64 transition. If you have WinForms (or WPF) based apps and want to keep them relevant and up to date, don't miss it.

Your mileage may vary but for Royal TS, running natively on ARM CPUs is an investment for the future. ARM is on the rise and may soon be much more attractive to run Windows as Intel/x64-based CPUs.

### Presenter: Stefan Koell
Co-founder and CEO of Royal Apps GmbH and Windows lead developer for Royal TS, a multi-platform, multi-protocol remote management solution for Windows, macOS, and mobile. Supporting RDP, VNC, SSH, Telnet, Web, and many more. Long time Microsoft MVP (2010-2020) supporting communities on- and offline as well as speaking at user groups and conferences about DevOps and other software development topics.

Outline Slides:

#### Cover
    WinForms on ARM64 with .NET 7

#### About Me
    Stefan Koell
    Co-founder/CEO Royal Apps GmbH
    e: stefan.koell(-at-)royalapps.com
    m: @stefankoell@masto.ai
    t: @stefankoell

#### About Royal TS
    - WinForms, C#
    - Offers RDP, VNC, Terminal (SSH), Web, and more
    - Cross-Platform Codebase (Windows, macOS, mobile)
    - DI, Messaging, HighDPI, SVG, UI Theming
    - .NET Framework -> .NET 5, 6, 7
#### Why WinForms?
    - First release 2003
    - Interop / Win32 access
    - 3rd party component ecosystem
    - UWP/WinUI issues and maturity
    - Rewrite is expensive
    - Maybe WPF/Avalonia
#### ARM64: What/Why?
    - SoC
    - High Performance
    - High Efficiency
    - Great Battery Life
    - Low Heat/Noise
#### WoA: Windows on ARM
    - "special" Windows SKU
    - Intel Bridge (Windows 11 only!) vs Rosetta 2 (macOS)
    - Licensing
    - Windows Insider
    - Parallels on Apple M1/M2
#### Hardware
    - Ultrabooks / Notebooks
      - Microsoft: Surface Pro 9 and X
      - Lenovo: Flex, Thinkpad
      - HP: Elite Folio
      - Acer: Spin 7
    - Project Volterra
    - Ampere Computing
      https://solutions.amperecomputing.com/systems/altra/kraken-comhpc-WS
    - Apple: M1/M2 with Parallels/vmware Fusion
#### Software supporting ARM64
    - Windows 11
    - .NET 7
    - Visual Studio 2022
    - JetBrains Rider
    - VS Code
    - Edge
#### NuGet Packages
    - Using pure (.NET only) packages
    - Using packages with native binaries
    - Creating packages with native binaries:
        - Use compiler directives (#if)
        - Use RuntimeInformation API
          (External Apps)[https://github.com/royalapplications/royalapps-community-externalapps]
#### Royal TS for ARM64
    - Any CPU vs x64 vs ARM64
    - PInvoke Considerations
    - 3rd Party Dependencies
    - Self Contained
    - CI/CD Pipeline (nuke)
    - Build ARM64 artifact on x64 (Build Server)
#### Real World Performance
    - x64 Dev Machine (16-core Ryzen 9 5950X, 3,4GHz, 64GB RAM)
        - Compile Time: ~ 12s
        - Startup Time: ~ 8s
     - ARM64 Dev VM (8-core Apple Silicon, 3,2GHz, 24GB RAM)*
        - Compile Time: ~ 15s
        - Startup Time: ~ 8s
     * Mac Studio 2022, M1 Max (10-core), 64GB
#### Q&A
    Thank you!
    Btw, we're hiring! ;) https://royalapps.com/jobs
    e: stefan.koell(-at-)royalapps.com
    m: @stefankoell@masto.ai
    t: @stefankoell
