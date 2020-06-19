// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using flucto.IO.Stores;

namespace flucto.Audio
{
    public interface IAdjustableResourceStore<T> : IResourceStore<T>, IAdjustableAudioComponent, IAggregateAudioAdjustment
        where T : AudioComponent
    {
    }
}
