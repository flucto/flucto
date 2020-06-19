// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace flucto.Testing
{
    /// <summary>
    /// Denotes a "visual" test which should only be run in a headless context.
    /// This will stop the test from showing up in a <see cref="TestBrowser"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [MeansImplicitUse]
    public class HeadlessTestAttribute : Attribute
    {
    }
}
