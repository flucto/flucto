﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Allocation;
using flucto.Bindables;
using flucto.Graphics.Primitives;
using flucto.Platform;

namespace flucto.Graphics.Containers
{
    /// <summary>
    /// A <see cref="Container"/> that is automatically cached and provides a <see cref="BindableSafeArea"/> representing
    /// the desired safe area margins. Should be used in conjunction with child <see cref="SafeAreaContainer"/>s.
    /// The root of the scenegraph contains an instance of this container, with <see cref="BindableSafeArea"/> automatically bound
    /// to the host <see cref="GameWindow"/>'s <see cref="GameWindow.SafeAreaPadding"/>.
    /// </summary>
    [Cached(typeof(ISafeArea))]
    public class SafeAreaDefiningContainer : Container<Drawable>, ISafeArea
    {
        private readonly bool usesCustomBinding;

        private readonly BindableSafeArea safeArea = new BindableSafeArea();

        /// <summary>
        /// Initialises a <see cref="SafeAreaDefiningContainer"/> by optionally providing a custom <see cref="BindableSafeArea"/>.
        /// If no such binding is provided, the container will default to <see cref="GameWindow.SafeAreaPadding"/>.
        /// </summary>
        /// <param name="safeArea">The custom <see cref="BindableSafeArea"/> to bind to, if required.</param>
        public SafeAreaDefiningContainer(BindableSafeArea safeArea = null)
        {
            if (safeArea != null)
            {
                usesCustomBinding = true;
                this.safeArea.BindTo(safeArea);
            }
        }

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            if (!usesCustomBinding && host.Window != null)
                safeArea.BindTo(host.Window.SafeAreaPadding);
        }

        #region ISafeArea Implementation

        RectangleF ISafeArea.AvailableNonSafeSpace => DrawRectangle;

        Quad ISafeArea.ExpandRectangleToSpaceOfOtherDrawable(IDrawable other) => ToSpaceOfOtherDrawable(DrawRectangle, other);

        BindableSafeArea ISafeArea.SafeAreaPadding => safeArea;

        #endregion
    }
}
