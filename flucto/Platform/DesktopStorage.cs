// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.IO;
using flucto.Configuration;

namespace flucto.Platform
{
    public class DesktopStorage : NativeStorage
    {
        public DesktopStorage(string baseName, DesktopGameHost host)
            : base(baseName, host)
        {
            if (host.IsPortableInstallation || File.Exists(FrameworkConfigManager.FILENAME))
            {
                BasePath = "./";
                BaseName = string.Empty;
            }
        }
    }
}
