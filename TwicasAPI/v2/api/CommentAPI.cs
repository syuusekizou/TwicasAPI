using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 取得コメント数最大値
        /// </summary>
        public const int MAX_COMMENT = 50;
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

        /// <summary>
        /// 保存済みコメント一覧
        /// </summary>
        private List<(string id, string message)> Comments { get; set; } = new List<(string, string)>();
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
            return Deserialize<PostCommentResponse>(result);
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
        public string GetRandom()
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

        /// <summary>
        /// コメントを作成日時の降順で取得する。
        /// </summary>
        /// <param name="movie_id">ライブID</param>
        /// <param name="offset">先頭からの位置</param>
        /// <param name="limit">取得件数(場合により、指定件数に満たない数のコメントを返す可能性があります)</param>
        /// <param name="slice_id">このコメントID以降のコメントを取得します。このパラメータを指定した場合はoffsetは無視されます。</param>
        /// <returns>取得コメント</returns>
        public GetCommentResponse GetComment(string movie_id, int offset = 0, int limit = 10, string slice_id = null)
        {
            var sliceId = string.IsNullOrWhiteSpace(slice_id) ? string.Empty : $"&slice_id={slice_id}";
            var param = new Parameter
            {
                Method = HttpMethod.Get,
                Request = $"/movies/{movie_id}/comments?offset={offset}&limit={limit}{sliceId}",
                Config = base.Config,
                AuthorizationIndex = base.AuthorizationIndex
            };
            var result = SendRequest(param);
            return Deserialize<GetCommentResponse>(result);
        }

        /// <summary>
        /// コメントを作成日時の降順で取得する。
        /// </summary>
        /// <returns>取得コメント</returns>
        public GetCommentResponse GetResponse(int limit = 10)
        {
            var user = new UserAPI(this.Path);
            var movieId = user.GetLastMovieId();
            return GetComment(movieId, limit: limit);
        }

        /// <summary>
        /// 取得コメントのリストを取得する
        /// </summary>
        /// <param name="input">取得コメント</param>
        /// <returns>取得コメントのリスト</returns>
        public List<(string, string)> GetComments(GetCommentResponse input)
        {
            return input.Comments.Select(x => (id: x.Id, message: x.Message)).ToList();
        }

        /// <summary>
        /// 取得コメントのリストを取得する
        /// </summary>
        /// <returns>取得コメントのリスト</returns>
        public List<(string, string)> GetComments(int limit = 10)
        {
            return GetComments(GetResponse(limit));
        }

        /// <summary>
        /// 取得コメントを保存する
        /// </summary>
        /// <param name="input">取得コメント</param>
        public void SaveComments(List<(string, string)> input)
        {
            Comments.AddRange(input);
            Comments = Comments.Distinct().ToList();
        }

        /// <summary>
        /// コメントを保存する
        /// </summary>
        /// <param name="input">コメントオブジェクト</param>
        public void SaveComments(Comment input)
        {
            Comments.Add((input.Id, input.Message));
            Comments = Comments.Distinct().ToList();
        }

        /// <summary>
        /// キーワードが存在するか
        /// </summary>
        /// <param name="list">取得コメントのリスト</param>
        /// <param name="keyword">キーワード</param>
        /// <returns>存在するか</returns>
        public bool Contains(IEnumerable<(string id, string message)> list, string keyword)
        {
            return list.Any(x => x.message.Contains(keyword));
        }

        /// <summary>
        /// 保存済みコメントを取得する
        /// </summary>
        /// <returns>保存済みコメント</returns>
        public List<(string, string)> GetSaveComments()
        {
            return Comments;
        }
    }
}
