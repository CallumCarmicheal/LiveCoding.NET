using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using LiveEdu.API;
using LiveEdu.Server;
using LiveEdu.Server.Models;

namespace WinForms_ImplementingAGUI {
    static class Program {

        // Store API Variables that would be required later!
        public static oAuthAuth     oAuthCreds;
        public static APIAccess     apiAccess;
        public static Request       apiRequest;
        public static Engine        apiEngine;
        public static ServResources servResources;

        private static Forms.SplashScreens.Splashscreen splash;
        public static Forms.Main mFrm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // Enable the Visual Styles
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Because it can take awhile to recieve the data from the server,
            // when creating a gui application - it would be best to get the 
            // user to authenticate themselfs when the application first opens.
            // A good way to do this would be creating a SplashScreen.
            splash = new Forms.SplashScreens.Splashscreen();

            // Wait until the splashscreen has loaded
            splash.Shown += Splash_Shown;
            splash.Show();
            Application.Run();
        }

        private static void Splash_Shown(object sender, EventArgs e) {
            // Start the code on another thread
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork             += ConnectToApi;
            bw.RunWorkerCompleted += ShowApplication;
            bw.RunWorkerAsync();
        }

        private static void ShowApplication(object sender, RunWorkerCompletedEventArgs e) {
            // How we have connected to the api, show our form

            // Hide the form before showing the other one
            // NOTE: DO NOT CLOSE THE FORM BEFORE OPENING 
            //       THE NEW ONE OR THE APPLICAITON WILL 
            //       CLOSE!
            splash.Hide();

            // Show our main form
            mFrm = new Forms.Main();
            mFrm.Show();

            // Now we can close it
            splash.Close();
        }

        private static void ConnectToApi(object sender, DoWorkEventArgs e) {
            /* Initiate connection to API */ {
                // You could add this into another thread
                // to have a animation while the application is connecting
                // etc.
                splash.SetText("Creating Object!");

                // Setup our sided api
                var serverAPIURL = "http://callumcarmicheal.com/LCAPI/"; // LINK TO WHERE YOU UPLOADED AND SETUP THE PHP SCRIPT
                apiRequest = new Request(servResources = new ServResources(serverAPIURL));

                // Get a new GUID for our API Access
                var guidReqResponse = apiRequest.getNewGUID();

                // Print our information from the guidReqResponse
                splash.SetText("Requesting a Authorization GUID and URL");
                var apiSetupResponse = apiRequest.SetupAPIRequest(false); // When using our own checking method, we just check to see if its "RETURN"

                // Check if our state == return (We will handle our own checking of the token)
                if (apiSetupResponse.getState() != ResponseState.RETURN) {
                    splash.SetText("Unexpected Response (" + apiSetupResponse.getState().ToString() + "): " + apiSetupResponse.getMessage());
                }

                // Get our token state
                GUID_Status guidStatus;
                splash.SetText("Waiting for user to accept POPUP");

                while (true) {
                    // Now we play the waiting game!
                    //splash.SetText("State: " + state.ToString() + "\n");

                    // Check our GUID State
                    guidStatus = apiRequest.getGUIDState();

                    if (guidStatus == GUID_Status.STATE_ID_VALID) {
                        splash.SetText("Server recieved token.");
                        break;
                    }

                    // Wait 0.5 seconds before checking
                    // for a token! NOTE: since this is a splashscreen
                    // we dont need to do Application.DoEvents to redraw
                    // the form since it is static.
                    Thread.Sleep(500);
                }


                splash.SetText("Requesting Token");

                apiAccess = new APIAccess(apiRequest);
                var apiToken = apiAccess.getBearerCode();

                if (apiToken.Error) {
                    // TODO: Handle Error
                }

                splash.SetText("Creating extra objects");
                oAuthCreds = new oAuthAuth(apiToken.Token);
                apiEngine  = new Engine(oAuthCreds);


                splash.SetText("Done.");
            }
        }
    }
}
