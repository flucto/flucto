﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Platform.Linux.Native;
using flucto.Platform.Linux.Sdl;
using osuTK;

namespace flucto.Platform.Linux
{
    public class LinuxGameHost : DesktopGameHost
    {
        internal LinuxGameHost(string gameName, bool bindIPC = false, ToolkitOptions toolkitOptions = default, bool portableInstallation = false)
            : base(gameName, bindIPC, toolkitOptions, portableInstallation)
        {
        }

        protected override void SetupForRun()
        {
            base.SetupForRun();

            Window = new LinuxGameWindow();

            // required for the time being to address libbass_fx.so load failures (see https://github.com/ppy/osu/issues/2852)
            Library.Load("libbass.so", Library.LoadFlags.RTLD_LAZY | Library.LoadFlags.RTLD_GLOBAL);
        }

        protected override Storage GetStorage(string baseName) => new LinuxStorage(baseName, this);

        public override Clipboard GetClipboard()
        {
            if (((LinuxGameWindow)Window).IsSdl)
            {
                return new SdlClipboard();
            }
            else
            {
                return new LinuxClipboard();
            }
        }
    }
}
