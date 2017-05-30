using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vadym.Database;
using Vadym.ViewModels;

namespace Vadym.Api
{
    /// <summary>
    /// Main Handler of application
    /// </summary>
    internal class MainHandler : IHandler
    {
        public void Register()
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            //Handler of application start page
            Handle.GET("/Vadym/start", () =>
            {
                return Db.Scope(() =>
                {
                    var json = new StartPage();

                    if (Session.Current == null)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }

                    json.Session = Session.Current;
                    return json;
                });

            });


            //Handler for specific franchise page
            Handle.GET("/Vadym/franchise/{?}", (string franchiseId) =>
            {
                return Db.Scope(() =>
                {
                    var f = Db.SQL<Franchise>("Select f FROM Franchise f where f.ObjectId = ?", franchiseId).First;
                    var json = new FranchiseJson
                    {
                        Data = f
                    };

                    if (Session.Current == null)
                    {
                        Session.Current = new Session(SessionOptions.PatchVersioning);
                    }

                    json.Session = Session.Current;
                    return json;
                });
            });

            //TODO: Make below work

            var url = "https://www.facebook.com/v2.9/dialog/oauth?client_id=1874789556111340&redirect_uri=http://localhost:8080/Vadym/start&response_type=token";

            Handle.GET("/redirectToFacebook", () =>
            {
                var resp = new Response()
                {
                    StatusCode = 302,
                    StatusDescription = "Moved Permanently"
                };
                resp.Headers["Location"] = url;
                return resp;
            });

            var sharpEncoded = WebUtility.UrlEncode("#");

            //Handling response from facebook oauth
            // TODO: make it work, because Starcounter cannot recognize '#' sign
            Handle.GET("/Vadym/start" + sharpEncoded + "access_token={?}&expires_in={?}", (Request request, string data1, string data2) =>
               {
                   return Db.Scope(() =>
                   {
                       var json = new StartPage();

                       if (Session.Current == null)
                       {
                           Session.Current = new Session(SessionOptions.PatchVersioning);
                       }

                       json.Session = Session.Current;
                       return json;
                   });

               });


        }
    }
}
