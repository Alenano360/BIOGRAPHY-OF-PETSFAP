using System.Web;
using System.Web.Mvc;

namespace BIOGRAPHY_OF_PETSFAP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
