﻿// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using flucto.Platform;
using osuTK;
using osuTK.Graphics.ES30;

namespace flucto.Graphics.OpenGL.Buffers
{
    internal class RenderBuffer : IDisposable
    {
        private readonly RenderbufferInternalFormat format;
        private readonly int renderBuffer;
        private readonly int sizePerPixel;

        private FramebufferAttachment attachment;

        public RenderBuffer(RenderbufferInternalFormat format)
        {
            this.format = format;

            renderBuffer = GL.GenRenderbuffer();

            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);

            // OpenGL docs don't specify that this is required, but seems to be required on some platforms
            // to correctly attach in the GL.FramebufferRenderbuffer() call below
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, format, 1, 1);

            switch (format)
            {
                case RenderbufferInternalFormat.DepthComponent16:
                    attachment = FramebufferAttachment.DepthAttachment;
                    GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment, RenderbufferTarget.Renderbuffer, renderBuffer);
                    sizePerPixel = 2;
                    break;

                case RenderbufferInternalFormat.Rgb565:
                case RenderbufferInternalFormat.Rgb5A1:
                case RenderbufferInternalFormat.Rgba4:
                    attachment = FramebufferAttachment.ColorAttachment0;
                    GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment, RenderbufferTarget.Renderbuffer, renderBuffer);
                    sizePerPixel = 2;
                    break;

                case RenderbufferInternalFormat.StencilIndex8:
                    attachment = FramebufferAttachment.StencilAttachment;
                    GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment, RenderbufferTarget.Renderbuffer, renderBuffer);
                    sizePerPixel = 1;
                    break;
            }
        }

        private Vector2 internalSize;
        private NativeMemoryTracker.NativeMemoryLease memoryLease;

        public void Bind(Vector2 size)
        {
            size = Vector2.Clamp(size, Vector2.One, new Vector2(GLWrapper.MaxRenderBufferSize));

            // See: https://www.khronos.org/registry/OpenGL/extensions/EXT/EXT_multisampled_render_to_texture.txt
            //    + https://developer.apple.com/library/archive/documentation/3DDrawing/Conceptual/OpenGLES_ProgrammingGuide/WorkingwithEAGLContexts/WorkingwithEAGLContexts.html
            // OpenGL ES allows the driver to discard renderbuffer contents after they are presented to the screen, so the storage must always be re-initialised for embedded devices.
            // Such discard does not exist on non-embedded platforms, so they are only re-initialised when required.
            if (GLWrapper.IsEmbedded || internalSize.X < size.X || internalSize.Y < size.Y)
            {
                GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, renderBuffer);
                GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, format, (int)Math.Ceiling(size.X), (int)Math.Ceiling(size.Y));

                if (!GLWrapper.IsEmbedded)
                {
                    memoryLease?.Dispose();
                    memoryLease = NativeMemoryTracker.AddMemory(this, (long)(size.X * size.Y * sizePerPixel));
                }

                internalSize = size;
            }
        }

        public void Unbind()
        {
            if (GLWrapper.IsEmbedded)
            {
                // Renderbuffers are not automatically discarded on all embedded devices, so invalidation is forced for extra performance and to unify logic between devices.
                GL.InvalidateFramebuffer(FramebufferTarget.Framebuffer, 1, ref attachment);
            }
        }

        #region Disposal

        ~RenderBuffer()
        {
            GLWrapper.ScheduleDisposal(() => Dispose(false));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if (renderBuffer != -1)
            {
                memoryLease?.Dispose();
                GL.DeleteRenderbuffer(renderBuffer);
            }

            isDisposed = true;
        }

        #endregion
    }
}
