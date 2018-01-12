using System.Web;
using System.Web.Mvc;

namespace AutenticateWithOutIdentity
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
