using System.Text;
using System.Text.Json.Serialization;

namespace TwicasAPI.v2.model
{
    class ErrorModel
    {
        /// <summary>
        /// Errorオブジェクト
        /// </summary>
        [JsonPropertyName("error")]
        public Error ErrorObj { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"code:{ErrorObj.Code}");
            sb.AppendLine($"message:{ErrorObj.Message}");
            sb.AppendLine($"details:{ErrorObj.Details}");
            return sb.ToString();
        }
    }

    public class Error
    {
        /// <summary>
		/// エラー識別コード
		/// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
		/// ヒューマンリーダブルなエラーメッセージ
		/// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
		/// Validation Errorの場合のみ存在するフィールド
		/// </summary>
        [JsonPropertyName("details")]
        public object Details { get; set; }
    }
}
