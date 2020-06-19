// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using JetBrains.Annotations;

namespace flucto.Testing
{
    /// <summary>
    /// Denotes a method which adds <see cref="TestScene"/> steps.
    /// Invoked via <see cref="TestScene.RunSetUpSteps"/> (which is called from nUnit's [SetUp] or <see cref="TestBrowser.LoadTest"/>).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [MeansImplicitUse]
    public class SetUpStepsAttribute : Attribute
    {
    }
}
