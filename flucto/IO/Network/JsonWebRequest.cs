// Copyright (c) 2020 Flucto Team and others. Licensed under the MIT Licence.
// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Newtonsoft.Json;

namespace flucto.IO.Network
{
    /// <summary>
    /// A web request with a specific JSON response format.
    /// </summary>
    /// <typeparam name="T">the response format.</typeparam>
    public class JsonWebRequest<T> : WebRequest
    {
        protected override string Accept => "application/json";

        public JsonWebRequest(string url = null, params object[] args)
            : base(url, args)
        {
        }

        protected override void ProcessResponse() => ResponseObject = JsonConvert.DeserializeObject<T>(GetResponseString());

        public T ResponseObject { get; private set; }
    }
}
