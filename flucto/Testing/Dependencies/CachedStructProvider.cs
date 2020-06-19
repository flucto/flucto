// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Allocation;

namespace flucto.Testing.Dependencies
{
    /// <summary>
    /// This is used for internal <see cref="DependencyContainer"/> testing purposes.
    /// </summary>
    internal class CachedStructProvider
    {
        [Cached]
        public Struct CachedObject { get; } = new Struct { Value = 10 };

        public struct Struct
        {
            public int Value;
        }
    }
}
