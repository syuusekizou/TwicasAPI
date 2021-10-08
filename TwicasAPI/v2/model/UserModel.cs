using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    public class UserModel
    {
        /// <summary>
		/// Userオブジェクト
		/// </summary>
        [JsonPropertyName("user")]
        public User User { get; set; }

        /// <summary>
		/// ユーザーのサポーターの数
		/// </summary>
        [JsonPropertyName("supporter_count")]
        public int SupporterCount { get; set; }

        /// <summary>
        /// ユーザーがサポートしている数
        /// </summary>
        [JsonPropertyName("supporting_count")]
        public int SupportingCount { get; set; }
    }

    public class User
    {
        /// <summary>
        /// ユーザID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// id同様にユーザを特定する識別子ですが、
        /// screen_idはユーザによって変更される場合があります。
        /// </summary>
        [JsonPropertyName("screen_id")]
        public string ScreenId { get; set; }

        /// <summary>
        /// ヒューマンリーダブルなユーザの名前
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// ユーザアイコンのURL
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }

        /// <summary>
        /// プロフィール文章
        /// </summary>
        [JsonPropertyName("profile")]
        public string Profile { get; set; }

        /// <summary>
        /// ユーザのレベル
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        /// ユーザが最後に配信したライブのID
        /// </summary>
        [JsonPropertyName("last_movie_id")]
        public object LastMovieId { get; set; }

        /// <summary>
        /// 現在ライブ配信中かどうか
        /// </summary>
        [JsonPropertyName("is_live")]
        public bool IsLive { get; set; }

        /// <summary>
        /// 非推奨
        /// </summary>
        [JsonPropertyName("supporter_count")]
        public int SupporterCount { get; set; }

        /// <summary>
        /// 非推奨
        /// </summary>
        [JsonPropertyName("supporting_count")]
        public int SupportingCount { get; set; }

        /// <summary>
        /// 非推奨
        /// </summary>
        [JsonPropertyName("created")]
        public int Created { get; set; }
    }
}
