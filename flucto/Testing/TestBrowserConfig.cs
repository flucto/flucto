// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.Configuration;
using flucto.Platform;

namespace flucto.Testing
{
    internal class TestBrowserConfig : IniConfigManager<TestBrowserSetting>
    {
        protected override string Filename => @"visualtests.cfg";

        public TestBrowserConfig(Storage storage)
            : base(storage)
        {
        }

        protected override void InitialiseDefaults()
        {
            base.InitialiseDefaults();
            Set(TestBrowserSetting.LastTest, string.Empty);
        }
    }

    internal enum TestBrowserSetting
    {
        LastTest,
    }
}
