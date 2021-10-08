using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    class GiftModel
    {
        /// <summary>
        /// 次にAPIを呼び出すときに指定する slice_id
        /// </summary>
        [JsonPropertyName("slice_id")]
        public int SliceId { get; set; }

        /// <summary>
		/// Giftオブジェクトの配列
		/// </summary>
        [JsonPropertyName("gifts")]
        public List<Gift> Gifts { get; set; }
    }

    public class Gift
    {
        /// <summary>
		/// アイテム送信ID
		/// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
		/// アイテム送信時のメッセージ本文
		/// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
		/// アイテム画像のURL
		/// </summary>
        [JsonPropertyName("item_image")]
        public string ItemImage { get; set; }

        /// <summary>
		/// アイテム送信時に選択された画像があれば画像のURL
		/// </summary>
        [JsonPropertyName("item_sub_image")]
        public string ItemSubImage { get; set; }

        /// <summary>
		/// アイテムのID
		/// </summary>
        [JsonPropertyName("item_id")]
        public string ItemId { get; set; }

        /// <summary>
		/// アイテムのMP
		/// </summary>
        [JsonPropertyName("item_mp")]
        public string ItemMp { get; set; }

        /// <summary>
		/// アイテム名
		/// </summary>
        [JsonPropertyName("item_name")]
        public string ItemName { get; set; }

        /// <summary>
		/// ユーザアイコンのURL
		/// </summary>
        [JsonPropertyName("user_image")]
        public string UserImage { get; set; }

        /// <summary>
		/// アイテムが送信された時点でのユーザーの screen_id
		/// </summary>
        [JsonPropertyName("user_screen_id")]
        public string UserScreenId { get; set; }

        /// <summary>
		/// ヒューマンリーダブルな screen_id
		/// </summary>
        [JsonPropertyName("user_screen_name")]
        public string UserScreenName { get; set; }

        /// <summary>
		/// ヒューマンリーダブルなユーザの名前
		/// </summary>
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }
    }
}
