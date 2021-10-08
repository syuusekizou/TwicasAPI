using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using TwicasAPI.v2.model;

namespace TwicasAPI.v2.api
{
    public class CommentAPI : BaseAPI
    {
        #region  定数
        public enum SNS
        {
            none,
            normal,
            reply
        }
        #endregion

        #region プロパティ
        private int Length = -1;
        private int AddLength
        {
            get
            {
                if (Length > 10)
                {
                    Length = -1;
                }
                return ++Length;
            }
        }
        #endregion

        #region コンストラクタ
        public CommentAPI(string path) : base(path) { }
        #endregion

        /// <summary>
        /// コメントを投稿する。 ユーザ単位でのみ実行可能です。
        /// </summary>
        /// <param name="movie_id">ライブID</param>
        /// <param name="comment">コメント文章</param>
        /// <param name="snsMode">sns同時投稿</param>
        /// <returns></returns>
        public PostCommentResponse PostComment(string movie_id, string comment, SNS snsMode = SNS.none)
        {
            var content = new Dictionary<string, string>()
            {
                { "comment", comment},
                { "sns", Enum.GetName(snsMode)}
            };
            var param = new Parameter
            {
                Method = HttpMethod.Post,
                Request = $"/movies/{movie_id}/comments",
                Content = content,
                Config = base.Config,
                AuthorizationIndex = base.AuthorizationIndex
            };
            var result = SendRequest(param);
            return GetResponseObject<PostCommentResponse>(result);
        }

        /// <summary>
        /// 指定した枠に設定ファイルのコメントを送信し続ける
        /// </summary>
        /// <param name="movie_id">ライブID</param>
        public void PostComment(string movie_id)
        {
            while (true)
            {
                foreach (var item in this.Config.Comment)
                {
                    try
                    {
                        var response = PostComment(movie_id, $"{item}{GetAddString(AddLength)}");
                        Thread.Sleep(this.WaitTime);
                    }
                    catch (TwicasException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine(ex.Json);
                        Thread.Sleep(4000);
                    }
                }
            }
        }

        /// <summary>
        /// 設定ファイルで指定された配信者の枠にコメントを送信し続ける
        /// </summary>
        public void PostComment()
        {
            var user = new UserAPI(this.Path);
            var movieId = user.GetLastMovieId();
            PostComment(movieId);
        }

        /// <summary>
        /// ランダムな文字を取得
        /// </summary>
        /// <returns>ランダムな文字</returns>
        private string GetRandom()
        {
            var list = new List<string>() { ".", " ", "_", ",", "-", "`", ";", ":" };
            var index = new Random().Next(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// 指定数のランダム文字列を取得
        /// </summary>
        /// <param name="length">指定数</param>
        /// <returns>ランダム文字列</returns>
        private String GetAddString(int length)
        {
            var result = new StringBuilder();
            while (result.Length < length)
            {
                result.Append(GetRandom());
            }
            return result.ToString();
        }
    }
}
