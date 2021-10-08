using System.Net.Http;
using TwicasAPI.v2.model;

namespace TwicasAPI.v2.api
{
    public class UserAPI : BaseAPI
    {
        #region コンストラクタ
        public UserAPI(string path) : base(path) { }
        #endregion

        /// <summary>
        /// ユーザーオブジェクトを取得
        /// </summary>
        /// <param name="user_id">ユーザーID</param>
        /// <returns></returns>
        public UserModel GetUserInfo(string user_id)
        {
            var param = new Parameter
            {
                Method = HttpMethod.Get,
                Request = $"/users/{user_id}",
                Config = base.Config
            };
            var result = SendRequest(param);
            return GetResponseObject<UserModel>(result);
        }

        /// <summary>
        /// 最後に配信したライブIDを取得
        /// </summary>
        /// <returns>ライブID</returns>
        public string GetLastMovieId()
        {
            var obj = GetUserInfo(base.Config.UserId);
            return obj.User.LastMovieId.ToString();
        }
    }
}
