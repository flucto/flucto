// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Platform;
using flucto.Platform.Linux;
using flucto.Platform.MacOS;
using flucto.Platform.Windows;
using System;
using osuTK;

namespace flucto
{
    public static class Host
    {
        public static DesktopGameHost GetSuitableHost(string gameName, bool bindIPC = false, bool portableInstallation = false)
        {
            var toolkitOptions = new ToolkitOptions
            {
                EnableHighResolution = true,
                Backend = RuntimeInfo.OS == RuntimeInfo.Platform.Linux ? PlatformBackend.Default : PlatformBackend.PreferNative
            };

            switch (RuntimeInfo.OS)
            {
                case RuntimeInfo.Platform.MacOsx:
                    return new MacOSGameHost(gameName, bindIPC, toolkitOptions, portableInstallation);

                case RuntimeInfo.Platform.Linux:
                    return new LinuxGameHost(gameName, bindIPC, toolkitOptions, portableInstallation);

                case RuntimeInfo.Platform.Windows:
                    return new WindowsGameHost(gameName, bindIPC, toolkitOptions, portableInstallation);

                default:
                    throw new InvalidOperationException($"Could not find a suitable host for the selected operating system ({Enum.GetName(typeof(RuntimeInfo.Platform), RuntimeInfo.OS)}).");
            }
        }
    }
}
