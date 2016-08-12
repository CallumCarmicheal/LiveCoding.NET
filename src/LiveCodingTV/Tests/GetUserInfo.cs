using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiveCodingTV.API;
using LiveCodingTV.Server;
using static LiveCodingTV.Server.APISetupResponse;
using static LiveCodingTV.Program;

namespace LiveCodingTV.Tests {
    class GetUserInfo {

        public void consoleHeavyVersion(string user) {
            Console.WriteLine("Creating Object!");

            var serverAPIURL = "http://callumcarmicheal.com/LCAPI/"; 
            Request apiRequest = new Request(new ServResources(serverAPIURL));

            Console.WriteLine("Requesting a GUID!");
            var guidReqResponse = apiRequest.getNewGUID();

            Console.WriteLine("Recieved Object Back!");
            Console.WriteLine("Guid:   " + guidReqResponse.GUID);
            Console.WriteLine("Url:    " + guidReqResponse.URL);
            Console.WriteLine("Err:    " + guidReqResponse.Error);
            Console.WriteLine("ErrMSG: " + guidReqResponse.Error_Message);

            Console.WriteLine("");

            Console.WriteLine("Requesting a Authorization GUID and URL");
            var apiSetupResponse = apiRequest.SetupAPIRequest(false); // When using our own checking method, we just check to see if its "RETURN"

            if (apiSetupResponse.getState() != APISetupResponse.ResponseState.RETURN) {
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

                if (i <= 5) {
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
            var jsonString = aReq.getAPIJson(APIResources.getUser(user), oaCreds, true);

            Console.WriteLine("JSON STRING: ");
                Console.WriteLine(jsonString);
            Console.ReadKey();
        }

        public void simpleVersion(string user) {
            var serverAPIURL     = "http://callumcarmicheal.com/LCAPI/";
            Request apiRequest   = new Request(new ServResources(serverAPIURL));

            var guidReqResponse  = apiRequest.getNewGUID();
            var apiSetupResponse = apiRequest.SetupAPIRequest(false); // When using our own checking method, we just check to see if its "RETURN"

            if (apiSetupResponse.getState() != APISetupResponse.ResponseState.RETURN) {
                Console.WriteLine("Unexpected Response (" + apiSetupResponse.getState().ToString() + "): " + apiSetupResponse.getMessage());
                Console.ReadKey(); return;
            }

            // Get our token state
            GUID_Status guidStatus;
            Console.WriteLine("Calling API To Check Token!");

            int i = 0;
            while (true) {
                // Now we play the waiting game!
                // Check our GUID State
                guidStatus = apiRequest.getGUIDState();

                if (guidStatus == GUID_Status.STATE_ID_VALID) {
                    Console.WriteLine("Token recieved.");
                    break;
                }

                // Wait 0.5 seconds before checking for a token!
                System.Threading.Thread.Sleep(500);

                if (++i <= 20) Console.WriteLine("Still waiting for token.");
                i = (i <= 20 ? 0 : i);
            }

            var apiAccess = new APIAccess(apiRequest);
            var apiToken = apiAccess.getBearerCode();

            if (apiToken.Error) {
                Console.WriteLine("Failed to get token from api.");
                Console.WriteLine("EMsg: " + apiToken.Error_Message);
                Console.ReadKey(); return;
            }

            oAuthAuth oaCreds   = new oAuthAuth(apiToken.Token);
            var aReq            = new APIRequestHandler();
            var jsonString      = aReq.getAPIJson(APIResources.getUser(user), oaCreds);

            Console.WriteLine("JSON STRING: ");
                Console.WriteLine(jsonString);
            Console.ReadKey();
        }
    }
}
