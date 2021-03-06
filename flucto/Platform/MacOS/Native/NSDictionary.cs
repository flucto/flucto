﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;

namespace flucto.Platform.MacOS.Native
{
    internal struct NSDictionary
    {
        internal IntPtr Handle { get; private set; }

        private static IntPtr classPointer = Class.Get("NSDictionary");

        internal NSDictionary(IntPtr handle)
        {
            Handle = handle;
        }
    }
}
