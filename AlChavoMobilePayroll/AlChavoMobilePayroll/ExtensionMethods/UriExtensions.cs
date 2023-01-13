using System;
using System.Web;

namespace AlChavoMobilePayroll.ExtensionMethods
{
    public static class UriExtensions
    {
        public static UriBuilder AddQuery(this UriBuilder uriBuilder, string property, string value)
        {
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[property] = value;
            uriBuilder.Query = query.ToString();
            return uriBuilder;
        }
    }
}
