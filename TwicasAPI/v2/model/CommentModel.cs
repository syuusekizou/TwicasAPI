using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    public class CommentModel
    {
        /// <summary>
		/// ライブID
		/// </summary>
        [JsonPropertyName("movie_id")]
        public string MovieId { get; set; }

        /// <summary>
        /// 総コメント数
        /// </summary>
        [JsonPropertyName("all_count")]
        public int AllCount { get; set; }

        /// <summary>
        /// Commentオブジェクトの配列
        /// </summary>
        [JsonPropertyName("comments")]
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        /// <summary>
		/// コメントID
		/// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// コメントID
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// コメント投稿者の情報 Userオブジェクト
        /// </summary>
        [JsonPropertyName("from_user")]
        public User FromUser { get; set; }

        /// <summary>
        /// コメント投稿日時のunixタイムスタンプ
        /// </summary>
        [JsonPropertyName("created")]
        public int Created { get; set; }
    }

    public class PostCommentResponse
    {
        /// <summary>
        /// ライブID
        /// </summary>
        [JsonPropertyName("movie_id")]
        public string MovieId { get; set; }

        /// <summary>
        /// 総コメント数
        /// </summary>
        [JsonPropertyName("all_count")]
        public int AllCount { get; set; }

        /// <summary>
        /// Commentオブジェクト
        /// </summary>
        [JsonPropertyName("comment")]
        public Comment Comment { get; set; }
    }

    public class DeleteCommentResponse
    {
        /// <summary>
        /// 削除したコメントのID
        /// </summary>
        [JsonPropertyName("comment_id")]
        public string CommentId { get; set; }
    }
}
