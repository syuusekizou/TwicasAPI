using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    public class SearchUsers
    {
        /// <summary>
        /// Userオブジェクトの配列
        /// </summary>
        [JsonPropertyName("users")]
        public List<User> Users { get; set; }
    }

    public class SearchLiveMovies
    {
        /// <summary>
        /// Movieオブジェクトの配列
        /// </summary>
        [JsonPropertyName("movies")]
        public List<Movie> Movies { get; set; }
    }
}
