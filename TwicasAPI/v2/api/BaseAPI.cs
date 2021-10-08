using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using TwicasAPI.v2.config;

namespace TwicasAPI.v2.api
{
    public class BaseAPI
    {
        #region プロパティ
        protected string Path { get; set; }
        protected Config Config { get; set; }
        private int authorindex = -1;
        public int MaxIndex;
        protected int AuthorizationIndex
        {
            get
            {
                var list = new List<int>
                {
                    MaxIndex,
                    Config.AccessTokenBearer.Count - 1
                };
                if (list.Contains(authorindex))
                {
                    authorindex = 0;
                }
                else
                {
                    authorindex++;
                }

                return authorindex;
            }
        }

        /// <summary>
        /// コメント投稿の待機時間
        /// </summary>
        protected int WaitTime
        {
            get
            {
                var result = Constant.SLEEP_TIME;
                if ((MaxIndex > 0) && (Config.AccessTokenBearer.Count > 1))
                {
                    result /= Config.AccessTokenBearer.Count;
                }
                return (int)result;
            }
        }
        #endregion

        #region コンストラクタ
        public BaseAPI(string path)
        {
            this.Path = path;
            this.Config = new Config(path);
        }
        #endregion

        /// <summary>
        /// WebAPIにメッセージを送信
        /// </summary>
        /// <param name="req">HTTPリクエスト</param>
        /// <returns>HTTPレスポンス</returns>
        public static HttpResponseMessage SendRequest(HttpRequestMessage req)
        {
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = client.SendAsync(req).Result;
                if ((int)result.StatusCode >= 400)
                {
                    throw new model.TwicasException(result);
                }
            }
            return result;
        }

        /// <summary>
        /// WebAPIにメッセージを送信
        /// </summary>
        /// <param name="parameter">APIパラメータ</param>
        /// <returns>HTTPレスポンス</returns>
        protected static HttpResponseMessage SendRequest(Parameter parameter)
        {
            var req = parameter.GetHttpRequestMessage();
            return SendRequest(req);
        }

        /// <summary>
        /// HTTPレスポンスからWebAPIのレスポンスを取得
        /// </summary>
        /// <typeparam name="T">WebAPIのレスポンスクラス</typeparam>
        /// <param name="response">HTTPレスポンス</param>
        /// <returns>WebAPIのレスポンス</returns>
        protected static T GetResponseObject<T>(HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<T>(response.Content.ReadAsStringAsync().Result);
        }
    }
}
