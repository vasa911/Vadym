using System;
using Starcounter;
using Vadym.Database;
using Vadym.ViewModels;
using System.Collections.Generic;
using Vadym.Api;

namespace Vadym
{
    class Program
    {
        static void Main()
        {

            IHandler mainHandler = new MainHandler();
            mainHandler.Register();
        }
    }
}