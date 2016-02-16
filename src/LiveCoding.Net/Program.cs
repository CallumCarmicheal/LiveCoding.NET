using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCoding.Net.API.Resources;

namespace LiveCoding.Net {
    static class Program {
        public static API.Engine.LiveCodingAPI livecodingAPI;


        const Int32 SW_MINIMIZE = 6;

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Console.Clear();
            //Console.SetBufferSize(150, Console.BufferHeight);
            //Console.SetWindowSize(150, Console.WindowHeight + 3);
            IntPtr hWndConsole = GetConsoleWindow();
            ShowWindow(hWndConsole, SW_MINIMIZE);

            /* Setup API */
            {
                //ConAPI.Write("§cEnter API KEY§r : §3", true, "§3APIKEY");
                //string apiKey = ConAPI.readPassword(); // Not needed for some of the api
                livecodingAPI = new API.Engine.LiveCodingAPI(
                    System.IO.File.ReadAllText("bin/client_ID.txt"),
                    System.IO.File.ReadAllText("bin/client_Secret.txt")
                );
            }

            Application.Run(new GUI.MainGUI());
        }
    }
}
