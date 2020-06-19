// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.UserInterface;

namespace flucto.Graphics.Cursor
{
    public class BasicContextMenuContainer : ContextMenuContainer
    {
        protected override Menu CreateMenu() => new BasicMenu(Direction.Vertical);
    }
}
