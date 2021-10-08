using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    public class CategoryModel
    {
        /// <summary>
        /// Categoryオブジェクトの配列
        /// </summary>
        [JsonPropertyName("categories")]
        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        /// <summary>
        /// カテゴリID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
		/// カテゴリ名
		/// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
		/// Sub categoryオブジェクトの配列
		/// </summary>
        [JsonPropertyName("sub_categories")]
        public List<SubCategory> SubCategories { get; set; }
    }

    public class SubCategory
    {
        /// <summary>
		/// サブカテゴリID
		/// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
		/// サブカテゴリ名
		/// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
		/// サブカテゴリ名
		/// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
