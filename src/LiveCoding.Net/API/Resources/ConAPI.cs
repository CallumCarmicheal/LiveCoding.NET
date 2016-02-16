using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCoding.Net.API.Resources {
    class ConAPI {
        public static void Write(string text, bool showDate = false, string category = "") {
            printText(text, showDate, category);
        }

        public static void WriteLine(string text, bool showDate = false, string category = "") {
            printTextLine(text, showDate, category);
        }

        public static string generateCategory(string c) {
            if (!string.IsNullOrWhiteSpace(c))
                c = "§r[§b" + c + "§r] ";
            return c;
        }

        public static void printText(string text, bool showDate = false, string category = "") {
            text = " " + text;
            category = generateCategory(category);
            ConsoleIO.WriteFormated((showDate ? getTimeStamp() : "") + category + text);
        }

        public static void printTextLine(string text, bool showDate = false, string category = "") {
            text = " " + text;
            category = generateCategory(category);
            ConsoleIO.WriteLineFormatted((showDate ? getTimeStamp() : "") + category + text);
        }

        public static string getAndVerifyPassword(string Display = "Enter Password") {
            bool correct = false;
            string
                passwordFinal = "",
                password1,
                password2;

            do {
                printText(Display + " (1/2): ", true, "Auth");
                password1 = readPassword(true);
                printText(Display + " (2/2): ", true, "Auth");
                password2 = readPassword(true);


                if (string.IsNullOrWhiteSpace(password1) || string.IsNullOrWhiteSpace(password2)) {
                    printTextLine("One or 2 of the passwords are nulled/whitespaced", true, "Auth");
                    continue;
                }

                if (password1 != password2) {
                    printTextLine("Passwords dont match, please try again", true, "Auth");
                } else {
                    printTextLine("Password set", true, "Auth");
                    correct = true;
                    continue;
                }
            } while (!correct);
            return passwordFinal;
        }

        public static string getTimeStamp() { return "{§4" + DateTime.Now.ToString("HH:mm:ss") + "§r} "; }
        public static string readLine(bool DisableSpace = false) { string o = ConsoleIO.ReadLine(DisableSpace); ConsoleIO.Write("\n"); return o; }
        public static string readPassword(bool nla = false) { string p = ConsoleIO.ReadPassword(); if (nla) ConsoleIO.Write(""); return p; }
        public static void waitForInput() { System.Console.ReadKey(); }
        public static bool waitForYesNo(string p1, bool p2, string p3) { return false; }




    }

}
