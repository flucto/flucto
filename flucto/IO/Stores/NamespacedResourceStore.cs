// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;

namespace flucto.IO.Stores
{
    public class NamespacedResourceStore<T> : ResourceStore<T>
    {
        public string Namespace;

        /// <summary>
        /// Initializes a resource store with a single store.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="ns">The namespace to add.</param>
        public NamespacedResourceStore(IResourceStore<T> store, string ns)
            : base(store)
        {
            Namespace = ns;
        }

        protected override IEnumerable<string> GetFilenames(string name) => base.GetFilenames($@"{Namespace}/{name}");

        public override IEnumerable<string> GetAvailableResources() => base.GetAvailableResources()
                                                                           .Where(x => x.StartsWith($"{Namespace}/"))
                                                                           .Select(x => x.Remove(0, $"{Namespace}/".Length));
    }
}
