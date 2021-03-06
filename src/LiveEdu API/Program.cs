﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using LiveEdu.Server;
using Newtonsoft.Json;
using static LiveEdu.Server.APISetupResponse;

namespace LiveEdu {

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
            
        }
    }
}
