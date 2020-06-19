﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Graphics.Sprites;
using osuTK.Graphics;

namespace flucto.Graphics.UserInterface
{
    public class BasicTabControl<T> : TabControl<T>
    {
        protected override Dropdown<T> CreateDropdown()
            => new BasicDropdown<T>();

        protected override TabItem<T> CreateTabItem(T value)
            => new BasicTabItem(value);

        public class BasicTabItem : TabItem<T>
        {
            private readonly SpriteText text;

            public BasicTabItem(T value)
                : base(value)
            {
                AutoSizeAxes = Axes.Both;

                Add(text = new SpriteText
                {
                    Margin = new MarginPadding(2),
                    Text = value.ToString(),
                    Font = new FontUsage(size: 18),
                });
            }

            protected override void OnActivated()
                => text.Colour = Color4.MediumPurple;

            protected override void OnDeactivated()
                => text.Colour = Color4.White;
        }
    }
}
