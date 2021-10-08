using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    public class SupportingStatus
    {
        /// <summary>
        /// サポーターかどうか
        /// </summary>
        [JsonPropertyName("is_supporting")]
        public bool IsSupporting { get; set; }

        /// <summary>
        /// 対象ユーザ情報 Userオブジェクト
        /// </summary>
        [JsonPropertyName("target_user")]
        public User TargetUser { get; set; }
    }

    public class SupportUser
    {
        /// <summary>
        /// サポーター登録を行った件数
        /// </summary>
        [JsonPropertyName("added_count")]
        public int AddedCount { get; set; }
    }

    public class UnsupportUser
    {
        /// <summary>
        /// サポーター解除を行った件数
        /// </summary>
        [JsonPropertyName("removed_count")]
        public int RemovedCount { get; set; }
    }

    public class SupportingList
    {
        /// <summary>
		/// 全レコード数(実際に取得できる件数と異なる場合があります)
		/// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
		/// SupporterUserオブジェクトの配列
		/// </summary>
        [JsonPropertyName("supporting")]
        public List<SupporterUser> Supporting { get; set; }
    }

    public class SupporterList
    {
        /// <summary>
		/// 全レコード数(実際に取得できる件数と異なる場合があります)
		/// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
		/// SupporterUserオブジェクトの配列
		/// </summary>
        [JsonPropertyName("supporters")]
        public List<SupporterUser> Supporters { get; set; }
    }

    public class SupporterUser : User
    {
        /// <summary>
        /// アイテム・スコア
        /// </summary>
        [JsonPropertyName("point")]
        public int Point { get; set; }

        /// <summary>
		/// 累計スコア
		/// </summary>
        [JsonPropertyName("total_point")]
        public int TotalPoint { get; set; }
    }
}
