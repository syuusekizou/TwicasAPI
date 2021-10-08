using System;
using System.Net.Http;

namespace TwicasAPI.v2.model
{
    class TwicasException : Exception
    {
        /// <summary>
        /// HTTPレスポンス
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// WebAPIのレスポンス
        /// </summary>
        public string Json { get; set; }

        public TwicasException(HttpResponseMessage response)
        {
            Response = response;
            Json = response.Content.ReadAsStringAsync().Result;
        }
    }
}
