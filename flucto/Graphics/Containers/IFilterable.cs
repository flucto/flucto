﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Graphics.Containers
{
    public interface IFilterable : IHasFilterTerms
    {
        /// <summary>
        /// Whether the current object is matching (ie. visible) given the current filter criteria of a parent.
        /// </summary>
        bool MatchingFilter { set; }

        /// <summary>
        /// Whether a filter is currently being performed.
        /// </summary>
        bool FilteringActive { set; }
    }
}
