using System;
using System.Collections.Generic;
using System.Text;

namespace AlChavoMobilePayroll.Services
{
    public partial class BaseService
    {
        private static string ApiKey = "https://osiwebapipayments.azurewebsites.net/api/";
        private static string ApiPAKey = "https://osiwebapipayroll.azurewebsites.net/api/";
        private static string AC30ApiKey = "https://api.alchavo.com/api/";

        private static string AC30ApiLocalKey = "https://4e39-13-90-157-18.ngrok.io/api/";



        public static string GetAPIKEY(string key)
        {
            switch (key)
            {
                case "ApiKey":
                    return ApiKey;
                case "AC30ApiKey":
                    return AC30ApiKey;
                case "LocalAC30":
                    return AC30ApiLocalKey;
                case "ApiPAKey":
                    return ApiPAKey;
            }

            return null;
        }

    }
}
