using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TwicasAPI.v2.config;

namespace TwicasAPI.v2.api
{
    public class Config
    {
        #region 定数
        const string SectionUserID = "user_id";
        const string SectionAccessTokenBearer = "access_token_bearer";
        const string SectionComment = "comment";
        const string SectionKeyword = "keyword";
        #endregion

        #region プロパティ
        /// <summary>
        /// 配信者のUserId
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// ツール実行者のAccessToken
        /// </summary>
        public List<string> AccessTokenBearer { get; }

        /// <summary>
        /// 投稿コメントのリスト
        /// </summary>
        public List<string> Comment { get; set; }

        /// <summary>
        /// キーワードの一覧
        /// </summary>
        public Dictionary<string, List<string>> Keyword { get; set; }

        /// <summary>
        /// アプリケーションのClientId
        /// </summary>
        public string ClientId
        {
            get
            {
                return Constant.CLIENT_ID;
            }
        }

        /// <summary>
        /// アプリケーションのClientSecret
        /// </summary>
        public string ClientSecret
        {
            get
            {
                return Constant.CLIENT_SECRET;
            }
        }
        #endregion

        #region コンストラクター
        private Config()
        {
        }

        public Config(string path)
        {
            var builder = new ConfigurationBuilder().AddJsonFile(path);
            var config = builder.Build();

            UserId = config[SectionUserID];
            AccessTokenBearer = new List<string>(GetList(config, SectionAccessTokenBearer));
            Keyword = GetKeyword(config);

            //投稿コメントをシャッフル
            var list = new List<string>(GetList(config, SectionComment));
            Comment = GetShuffle(list);
        }
        #endregion

        /// <summary>
        /// 配列を取得
        /// </summary>
        /// <param name="config">アプリ設定</param>
        /// <param name="key">プロパティ名</param>
        /// <returns></returns>
        private List<string> GetList(IConfigurationRoot config, string key)
        {
            var result = new List<string>(
                config.AsEnumerable()
                .Where(x => x.Value != null)
                .OrderBy(x => x.Key)
                .Where(x => x.Key.Contains(key))
                .Select(x => x.Value)
            );

            return result;
        }

        /// <summary>
        /// 引数をシャッフルした配列を取得
        /// </summary>
        /// <param name="input"></param>
        /// <returns>シャッフルした配列</returns>
        private List<string> GetShuffle(List<string> input)
        {
            var result = new List<string>();
            var random = new Random();
            var work = new List<string>(input);
            while (work.Count > 0)
            {
                var index = random.Next(0, work.Count);
                result.Add(work[index]);
                work.RemoveAt(index);
            }
            return result;
        }

        /// <summary>
        /// コメントBOT用のキーワード取得
        /// </summary>
        /// <param name="config">アプリ設定</param>
        /// <returns>キーワード</returns>
        private Dictionary<string, List<string>> GetKeyword(IConfigurationRoot config)
        {
            var result = new Dictionary<string, List<string>>();
            var section = config.GetSection(SectionKeyword);
            var keys = section.GetChildren().AsEnumerable().Select(x => x.Key);
            foreach (var key in keys)
            {
                var value = section.GetSection(key).GetChildren().AsEnumerable()
                            .Select(x => x.Value).ToList();
                result.Add(key, value);
            }
            return result;
        }
    }
}
