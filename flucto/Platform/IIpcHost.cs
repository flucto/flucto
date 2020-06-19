// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Threading.Tasks;

namespace flucto.Platform
{
    public interface IIpcHost
    {
        event Action<IpcMessage> MessageReceived;

        Task SendMessageAsync(IpcMessage ipcMessage);
    }
}
