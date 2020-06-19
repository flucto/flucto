// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using System.Reflection;
using osuTK;

namespace flucto.Platform.Linux
{
    public class LinuxGameWindow : DesktopGameWindow
    {
        public bool IsSdl { get; private set; }

        public LinuxGameWindow()
        {
            Load += OnLoad;
        }

        protected void OnLoad(object sender, EventArgs e)
        {
            var implementationField = typeof(NativeWindow).GetField("implementation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(implementationField != null, "Reflection is broken!");

            var windowImpl = implementationField.GetValue(Implementation);

            IsSdl = windowImpl.GetType().Name == "Sdl2NativeWindow";
        }
    }
}
