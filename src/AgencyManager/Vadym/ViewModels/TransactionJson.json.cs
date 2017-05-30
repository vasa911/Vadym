using Starcounter;
using System;
using MyFacebookClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace Vadym.ViewModels
{
    /// <summary>
    /// Represent transaction entity
    /// </summary>
    partial class TransactionJson : Json
    {
        static TransactionJson()
        {
            DefaultTemplate.TransactionHomeAddress.InstanceType = typeof(AddressJson);
        }

        /// <summary>
        /// Handle registering new transaction
        /// </summary>
        /// <param name="action"></param>
        void Handle(Input.RegisterNewTransaction action)
        {
            //Create new address object
            var address = new Database.Address
            {
                Street = TransactionHomeAddress.Street,
                Number = TransactionHomeAddress.Number,
                ZipCode = TransactionHomeAddress.ZipCode,
                City = TransactionHomeAddress.City,
                Country = TransactionHomeAddress.Country
            };

            //add new transaction
            new Database.Transaction
            {
                Date = Convert.ToDateTime(Date),
                Commission = Commission,
                SalesPrice = SalesPrice,
                Owner = (Parent as FranchiseJson).Data as Database.Franchise,

                Home = new Database.Home
                {
                    Address = address
                }
            };

            Transaction.Commit();

            //if Post on Wall checkbox is check => home and transaction info are posted to facebook wall
            if (PostOnFacebook)
            {
                var message = $"Home at Address: {address.FullAddress} was sold for {SalesPrice} on {Date}.";
                PostOnFacebookWall(message);
            }
        }

        //TODO: adjust below methods

        /// <summary>
        /// Post message on Facebook Wall
        /// </summary>
        /// <param name="message"></param>
        void PostOnFacebookWall(string message)
        {
            var token = Session.Data["FacebookAccessToken"].ToString();
           
            //FOR TEST PURPOSE ONLY
            //this is test token for test user
            token = "EAAapHHWFRZBwBAKypLJqQ71rUGMV8bkYmoyxaZCBo1iO2JpJZA24Q71YW6HeKWl5EVMdkcNdsxg1XwpvvOm3QhiHi1oYaebZADaeGLMeP9pn2j8gu8vHF8Wv2YgoNKZBVPO8k0zO3UbtgAuouTNyNTKBCEZAHMstvETy6gkEFg7gZDZD";
            ////////////////////////

            if (string.IsNullOrEmpty(token))
                RedirectToFacebookOAuthPage();
          
            MyFacebookClient.MyFacebookClient client = new MyFacebookClient.MyFacebookClient(token);
            client.PostOnWall(message);
        }

        /// <summary>
        /// Redirect user to facebook login page in order to get access token
        /// </summary>
        void RedirectToFacebookOAuthPage()
        {
            var uri = this.Session.SessionUri;
            var fr = (Parent as FranchiseJson).Data as Database.Franchise;
            var redirectionUrl = $"localhost:8080/Vadym/franchise/{fr.GetObjectID()}";

            var oauthUrl = MyFacebookClient.MyFacebookClient.GetOAuthUrl(redirectionUrl);

            //uncomment in order to redirect 
            //this.RedirectUrl = oauthUrl;

            //TODO: Redirect user to oauthUrl
            //Self.GET(oauthUrl);
        }
    }
}
