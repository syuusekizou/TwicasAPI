using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace TwicasAPI.v2.api
{
    public class Parameter
    {
        #region プロパティ
        #region URL
        const string BaseURL = "https://apiv2.twitcasting.tv";
        public string Request { get; set; }
        public string Url { get { return $"{BaseURL}{Request}"; } }
        #endregion

        public HttpMethod Method { get; set; }
        public object Content { get; set; }
        public Config Config { get; set; }
        public int AuthorizationIndex { get; set; }
        #endregion

        public HttpRequestMessage GetHttpRequestMessage()
        {
            var result = new HttpRequestMessage();
            result.Headers.Add("Accept", "application/json");
            result.Headers.Add("X-Api-Version", "2.0");
            result.Headers.Add("Authorization", GetAuthorization());
            result.Method = Method;
            result.RequestUri = new System.Uri(Url);
            result.Content = GetStringContent(Content);

            return result;
        }

        private string GetAuthorization()
        {
            string result = $"Bearer {Config.AccessTokenBearer[AuthorizationIndex]}";
            return result;
        }

        /// <summary>
        /// HTTP コンテンツを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        private StringContent GetStringContent<T>(T input)
        {
            var json = JsonSerializer.Serialize(input);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
