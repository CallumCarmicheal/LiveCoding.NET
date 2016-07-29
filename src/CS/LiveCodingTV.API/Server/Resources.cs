using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.TV.NET_API_Wrapper.LiveCodingTV.API.Server {
    class Resources {
        public static string LOCATION_APP               = "http://callumcarmicheal.com/LCAPI/";


        public static string LOCATION_APP_API           = LOCATION_APP + "apiApp.php";
        public static string LOCATION_APP_GUID_CHECK    = LOCATION_APP + "apiCheckGUID.php";
        public static string LOCATION_APP_TOKEN_REFRESH = LOCATION_APP + "apiRefreshToken.php";
        public static string LOCATION_APP_TOKEN_GET     = LOCATION_APP + "apiGetBearer.php";
    }
}
