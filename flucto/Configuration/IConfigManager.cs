﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace flucto.Configuration
{
    public interface IConfigManager
    {
        /// <summary>
        /// Loads this config.
        /// </summary>
        void Load();

        /// <summary>
        /// Saves this config.
        /// </summary>
        /// <returns>Whether the operation succeeded.</returns>
        bool Save();
    }
}
