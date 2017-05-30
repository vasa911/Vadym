using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyFacebookClient
{
    /// <summary>
    /// Client for facebook access and posting message to wall
    /// </summary>
    public class MyFacebookClient
    {
        #region Fields

        private const string app_secret = "ac1e043f196e3034960adaabe2a76c9f";
        private const string app_id = "1874789556111340";
        private const string app_code_client = "db4512c88d227021f28b41b0323f64c9";

        /// <summary>
        /// Token is required for every call to facebook API
        /// </summary>
        private static string _accessToken;

        /// <summary>
        /// Third party facebook client
        /// </summary>
        private static FacebookClient _fbClient;

        /// <summary>
        /// Id of authenticated user
        /// </summary>
        private static string _userId;

        #endregion

        #region Constructors

        public MyFacebookClient(string accessToken)
        {
            _fbClient = new FacebookClient(accessToken);
            dynamic result = _fbClient.Get("me", new { fields = "name,id" });
            _userId = result.id;
            _accessToken = accessToken;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Returns url where application need to navigate for get access token
        /// User will be able to login in facebook at this url
        /// Then it will redirect to "redirectUrl" and pass access token as parameter
        /// </summary>
        /// <param name="recirectUrl"></param>
        /// <returns></returns>
        public static string GetOAuthUrl(string recirectUrl)
        {
            var url = $"https://www.facebook.com/v2.9/dialog/oauth?client_id={app_id}&redirect_uri={recirectUrl}";
            return url;
        }


        /// <summary>
        /// Post message on facebook wall
        /// </summary>
        /// <param name="message"></param>
        public void PostOnWall(string message)
        {
            string url = string.Format("{0}/{1}", _userId, "feed");

            var argList = new Dictionary<string, object>();
            argList["message"] = message;

            try
            {
                _fbClient.Post(url, argList);
            }
            catch (FacebookOAuthException ex)
            {
                //Handle duplicate status message exception
                if (ex.ErrorCode != 506)
                    throw ex;
            }

        }

        #endregion
    }
}
