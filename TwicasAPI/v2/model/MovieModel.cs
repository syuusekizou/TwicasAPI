using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    public class MovieModel
    {
        /// <summary>
		/// Movieオブジェクト
		/// </summary>
        [JsonPropertyName("movie")]
        public Movie Movie { get; set; }

        /// <summary>
		/// 配信者のユーザ情報 Userオブジェクト
		/// </summary>
        [JsonPropertyName("broadcaster")]
        public User Broadcaster { get; set; }

        /// <summary>
		/// 設定されているタグの配列
		/// </summary>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
    }

    public class Movie
    {
        /// <summary>
		/// ライブID
		/// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
		/// ライブ配信者のユーザID
		/// </summary>
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        /// <summary>
		/// タイトル
		/// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
		/// テロップ
		/// </summary>
        [JsonPropertyName("subtitle")]
        public string Subtitle { get; set; }

        /// <summary>
		/// ライブ配信者の最新コメントの文章
		/// </summary>
        [JsonPropertyName("last_owner_comment")]
        public string LastOwnerComment { get; set; }

        /// <summary>
		/// カテゴリID
		/// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
		/// ライブ(録画)へのリンクURL
		/// </summary>
        [JsonPropertyName("link")]
        public string Link { get; set; }

        /// <summary>
		/// ライブ配信中かどうか
		/// </summary>
        [JsonPropertyName("is_live")]
        public bool IsLive { get; set; }

        /// <summary>
		/// 録画が公開されているかどうか
		/// </summary>
        [JsonPropertyName("is_recorded")]
        public bool IsRecorded { get; set; }

        /// <summary>
		/// 総コメント数
		/// </summary>
        [JsonPropertyName("comment_count")]
        public int CommentCount { get; set; }

        /// <summary>
		/// サムネイル画像(大)のURL
		/// </summary>
        [JsonPropertyName("large_thumbnail")]
        public string LargeThumbnail { get; set; }

        /// <summary>
		/// サムネイル画像(小)のURL
		/// </summary>
        [JsonPropertyName("small_thumbnail")]
        public string SmallThumbnail { get; set; }

        /// <summary>
		/// 配信地域(国コード)
		/// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
		/// 配信時間(秒)
		/// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
		/// 配信開始日時のunixタイムスタンプ
		/// </summary>
        [JsonPropertyName("created")]
        public int Created { get; set; }

        /// <summary>
		/// コラボ配信かどうか
		/// </summary>
        [JsonPropertyName("is_collabo")]
        public bool IsCollabo { get; set; }

        /// <summary>
		/// 合言葉配信かどうか
		/// </summary>
        [JsonPropertyName("is_protected")]
        public bool IsProtected { get; set; }

        /// <summary>
		/// 最大同時視聴数(配信中の場合0)
		/// </summary>
        [JsonPropertyName("max_view_count")]
        public int MaxViewCount { get; set; }

        /// <summary>
		/// 現在の同時視聴者数(配信中ではない場合0)
		/// </summary>
        [JsonPropertyName("current_view_count")]
        public int CurrentViewCount { get; set; }

        /// <summary>
		/// 総視聴者数
		/// </summary>
        [JsonPropertyName("total_view_count")]
        public int TotalViewCount { get; set; }

        /// <summary>
		/// HTTP Live Streaming再生用のURL
		/// </summary>
        [JsonPropertyName("hls_url")]
        public string HlsUrl { get; set; }
    }

    public class MoviesByUser
    {
        /// <summary>
        /// 指定フィルター条件での総件数
        /// </summary>
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Movieオブジェクト の配列
        /// </summary>
        [JsonPropertyName("movies")]
        public List<MovieModel> Movies { get; set; }
    }

    public class CurrentLive
    {
        /// <summary>
        /// Movieオブジェクト
        /// </summary>
        [JsonPropertyName("movie")]
        public MovieModel Movie { get; set; }

        /// <summary>
        /// 配信者のユーザ情報 Userオブジェクト
        /// </summary>
        [JsonPropertyName("broadcaster")]
        public User Broadcaster { get; set; }

        /// <summary>
        /// 設定されているタグの配列
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
    }

    public class LiveSubtitle
    {
        /// <summary>
        /// ライブID
        /// </summary>
        [JsonPropertyName("movie_id")]
        public string MovieId { get; set; }

        /// <summary>
        /// テロップ
        /// </summary>
        [JsonPropertyName("subtitle")]
        public string Subtitle { get; set; }
    }

    public class LiveHashtag
    {
        /// <summary>
        /// ライブID
        /// </summary>
        [JsonPropertyName("movie_id")]
        public string MovieId { get; set; }

        /// <summary>
        /// ハッシュタグ
        /// </summary>
        [JsonPropertyName("hashtag")]
        public string Hashtag { get; set; }
    }
}
