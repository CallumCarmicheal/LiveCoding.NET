using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveCoding.Net {
    static class Program {
        public static API.Engine.LiveCodingAPI livecodingAPI;






        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            /* Setup API */ {
                string apiKey = ""; // Not needed for some of the api
                livecodingAPI = new API.Engine.LiveCodingAPI(apiKey);
            }

            Application.Run(new GUI.MainGUI());
        }
    }
}
