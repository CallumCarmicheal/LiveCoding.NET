using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveCodingTV.API;
using LiveCodingTV.Server;
using LiveCodingTV.Server.Models;
using LiveCodingTV.API.Wrappers.Models;

namespace Console_GetUserInformation {
    class Program {

        // This is a getting started project,
        // it will be updated overtime as methods get changed etc.
        // As of current (12/08/16 23:37) the json responses have not
        // been wrapped so for now all that is returned is json!
        //
        // As of (13/08/16 01:06) there is a few wrappers, to call them use
        // the serializer as shown below!
        
        public static void ConColB(ConsoleColor c) { Console.BackgroundColor = c; }
        public static void ConColF(ConsoleColor c) { Console.ForegroundColor = c; }
        public static void ConColFB(ConsoleColor f, ConsoleColor b) {
            Console.ForegroundColor = f; Console.BackgroundColor = b;
        } public static void ConColBF(ConsoleColor b, ConsoleColor f) { ConColFB(f, b); }

        public static void ConPrintCol(string left, string right, ConsoleColor cl = ConsoleColor.White, ConsoleColor cr = ConsoleColor.Yellow) {
            left = left.PadRight(20);

            var tmp = Console.ForegroundColor;
            ConColF(cl);        Console.Write(left);
            ConColF(cr);        Console.WriteLine(right);

            ConColF(tmp);
        }

        static void Main(string[] args) {
            ConsoleOnlyTest();
        }

        static void ConsoleOnlyTest() {
            string testUser = "callumc";

            Console.WriteLine("Creating Object!");

            var serverAPIURL = "http://callumcarmicheal.com/LCAPI/"; // LINK TO WHERE YOU UPLOADED AND SETUP THE PHP SCRIPT
            Request apiRequest = new Request(new ServResources(serverAPIURL));

            Console.WriteLine("Requesting a GUID!");
            var guidReqResponse = apiRequest.getNewGUID();

            // Print our information from the guidReqResponse
            Console.WriteLine("Recieved Object Back!");
            Console.WriteLine("Guid:   " + guidReqResponse.GUID);
            Console.WriteLine("Url:    " + guidReqResponse.URL);
            Console.WriteLine("Err:    " + guidReqResponse.Error);
            Console.WriteLine("ErrMSG: " + guidReqResponse.Error_Message);

            Console.WriteLine("");

            Console.WriteLine("Requesting a Authorization GUID and URL");
            var apiSetupResponse = apiRequest.SetupAPIRequest(false); // When using our own checking method, we just check to see if its "RETURN"

            if (apiSetupResponse.getState() != ResponseState.RETURN) {
                Console.WriteLine("Unexpected Response (" + apiSetupResponse.getState().ToString() + "): " + apiSetupResponse.getMessage());
                Console.ReadKey(); return;
            }

            // Get our token state
            GUID_Status guidStatus;
            Console.WriteLine("Calling API To Check Token!");

            int i = 0;
            while (true) {
                // Now we play the waiting game!
                //Console.WriteLine("State: " + state.ToString() + "\n");

                // Check our GUID State
                guidStatus = apiRequest.getGUIDState();

                if (guidStatus == GUID_Status.STATE_ID_VALID) {
                    ConColF(ConsoleColor.Green);
                    Console.WriteLine("Token recieved.");
                    ConColF(ConsoleColor.Black);
                    break;
                }

                // Wait 0.5 seconds before checking
                // for a token!
                System.Threading.Thread.Sleep(500);

                i++;

                if (i <= 10000) {
                    i = 0;

                    ConColF(ConsoleColor.Yellow);
                    Console.WriteLine("Still waiting for token.");
                    ConColF(ConsoleColor.Black);
                }
            }

            ConColF(ConsoleColor.Cyan);
            Console.Write("Requesting Token: ");

            ConColF(ConsoleColor.Green);

            var apiAccess = new APIAccess(apiRequest);
            var apiToken = apiAccess.getBearerCode();

            if (apiToken.Error) {
                ConColF(ConsoleColor.Red);
                Console.WriteLine("Failed.");

                Console.WriteLine("EMsg: " + apiToken.Error_Message);
                Console.ReadKey(); return;
            }

            Console.WriteLine("Success.");
            ConColF(ConsoleColor.White);

            Console.WriteLine("Bearer Code: " + apiToken.Token);

            // Call the API
            Console.Write("Test Calling the API: ");

            oAuthAuth oaCreds = new oAuthAuth(apiToken.Token);
            var aReq = new APIRequestHandler();
            var jsonString = aReq.getAPIJson(APIResources.getUser(testUser), oaCreds, true);
            
            Console.WriteLine("JSON STRING: ");
                Console.WriteLine(jsonString + "\n\n");

            Console.Write("Attempting to serialize json: "); {
                var eng = new Engine(oaCreds);
                var ser = new LiveCodingTV.API.Wrappers.Serializer();

                User user = new User();
                
                try {
                    // Surround with a try and catch!
                    user = eng.User.GetUser(testUser);
                } catch(Exception ex) {
                    ConColF(ConsoleColor.Red);
                    Console.WriteLine("Error.");
                    Console.WriteLine("EMsg: " + ex.Message);
                    Console.ReadKey(); return;
                }

                // Set the console color and print Success
                ConColF(ConsoleColor.Green);
                    Console.WriteLine("Success");
                ConColF(ConsoleColor.DarkMagenta);

                Console.WriteLine("============================\tUser Information");

                // Print some information about the user
                ConPrintCol("Username",     user.Username);
                ConPrintCol("Country",      user.Country);
                ConPrintCol("Fav Line",     user.FavoriteLineOfCode);
            } 

            Console.ReadKey();
        }
    }
}
