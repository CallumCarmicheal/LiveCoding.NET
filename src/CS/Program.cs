using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCoding.TV.NET_API_Wrapper.LiveCodingTV.API.Server;
using static LiveCoding.TV.NET_API_Wrapper.LiveCodingTV.API.Server.APISetupResponse;

namespace LiveCoding.TV.NET_API_Wrapper {
    static class Program {

        public static void ConColB(ConsoleColor c) { Console.BackgroundColor = c; }
        public static void ConColF(ConsoleColor c) { Console.ForegroundColor = c; }
        public static void ConColFB(ConsoleColor f, ConsoleColor b) {
            Console.ForegroundColor = f; Console.BackgroundColor = b;
        } public static void ConColBF(ConsoleColor b, ConsoleColor f) { ConColFB(f, b); }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Console.WriteLine("Creating Object!");
            Request req = new Request();

            Console.WriteLine("Requesting a GUID!");
            var x = req.getNewGUID();

            Console.WriteLine("Recieved Object Back!");
            Console.WriteLine("Guid:   " + x.GUID);
            Console.WriteLine("Url:    " + x.URL);
            Console.WriteLine("Err:    " + x.Error);
            Console.WriteLine("ErrMSG: " + x.Error_Message);

            Console.WriteLine("");

            Console.WriteLine("Requesting a Authorization GUID and URL");
            var y = req.SetupAPIRequest(false); // When using our own checking method, we just check to see if its "RETURN"

            if(y.getState() != APISetupResponse.ResponseState.RETURN) {
                Console.WriteLine("Unexpected Response (" + y.getState().ToString() + "): " + y.getMessage());
            }

            GUID_Status state;
            while (true) {
                // Now we play the waiting game!

                Console.WriteLine("Calling API To Check Token!");
                state = req.getGUIDState();

                Console.WriteLine("State: " + state.ToString() + "\n");

                if(state == GUID_Status.STATE_ID_VALID) {
                    MessageBox.Show("ITS VALID NO GO HOME!");
                    break;
                } System.Threading.Thread.Sleep(500);
            }
        }
    }
}
