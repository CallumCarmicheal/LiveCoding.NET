using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveCoding.Net.API.Resources {
    public static class ConsoleIO {
        public static void Reset() { if (reading) { reading = false; System.Console.Write("\b \b"); } }
        public static void SetAutoCompleteEngine(IAutoComplete engine) { autocomplete_engine = engine; }
        public static bool basicIO = false;
        private static IAutoComplete autocomplete_engine;
        private static LinkedList<string> previous = new LinkedList<string>();
        private static string buffer = "";
        private static string buffer2 = "";
        private static bool reading = false;
        private static bool reading_lock = false;
        private static bool writing_lock = false;

        /// <summary>
        /// Read a password from the standard input
        /// </summary>

        public static string ReadPassword() {
            string password = "";
            ConsoleKeyInfo k = new ConsoleKeyInfo();
            while (k.Key != ConsoleKey.Enter) {
                k = System.Console.ReadKey(true);
                switch (k.Key) {
                    case ConsoleKey.Enter:
                        System.Console.Write('\n');
                        return password;

                    case ConsoleKey.Backspace:
                        if (password.Length > 0) {
                            System.Console.Write("\b \b");
                            password = password.Substring(0, password.Length - 1);
                        }
                        break;

                    case ConsoleKey.Escape:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Home:
                    case ConsoleKey.End:
                    case ConsoleKey.Delete:
                    case ConsoleKey.Oem6:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.Tab:
                        break;

                    default:
                        if (k.KeyChar != 0) {
                            System.Console.Write('*');
                            password += k.KeyChar;
                        }
                        break;
                }
            }
            return password;
        }

        /// <summary>
        /// Read a line from the standard input
        /// </summary>

        public static string ReadLine(bool disablespace = false) {
            if (basicIO) { return System.Console.ReadLine(); }
            ConsoleKeyInfo k = new ConsoleKeyInfo();
            /*System.Console.*/
            //Write("> ");
            reading = true;
            buffer = "";
            buffer2 = "";

            while (k.Key != ConsoleKey.Enter) {
                k = System.Console.ReadKey(true);
                while (writing_lock) { }
                reading_lock = true;
                if (k.Key == ConsoleKey.V && k.Modifiers == ConsoleModifiers.Control) {
                    string clip = ReadClipboard();
                    foreach (char c in clip)
                        AddChar(c);
                } else {
                    switch (k.Key) {
                        case ConsoleKey.Escape:
                            ClearLineAndBuffer();
                            break;
                        case ConsoleKey.Backspace:
                            RemoveOneChar();
                            break;
                        case ConsoleKey.Enter:
                            //System.Console.Write('\n');
                            System.Console.SetCursorPosition(0, System.Console.CursorTop);
                            break;
                        case ConsoleKey.LeftArrow:
                            GoLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            GoRight();
                            break;
                        case ConsoleKey.Home:
                            while (buffer.Length > 0) { GoLeft(); }
                            break;
                        case ConsoleKey.End:
                            while (buffer2.Length > 0) { GoRight(); }
                            break;
                        case ConsoleKey.Delete:
                            if (buffer2.Length > 0) {
                                GoRight();
                                RemoveOneChar();
                            }
                            break;
                        case ConsoleKey.Oem6:
                            break;
                        case ConsoleKey.DownArrow:
                            if (previous.Count > 0) {
                                ClearLineAndBuffer();
                                buffer = previous.First.Value;
                                previous.AddLast(buffer);
                                previous.RemoveFirst();
                                System.Console.Write(buffer);
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (previous.Count > 0) {
                                ClearLineAndBuffer();
                                buffer = previous.Last.Value;
                                previous.AddFirst(buffer);
                                previous.RemoveLast();
                                System.Console.Write(buffer);
                            }
                            break;
                        case ConsoleKey.Tab:
                            if (autocomplete_engine != null && buffer.Length > 0) {
                                string[] tmp = buffer.Split(' ');
                                if (tmp.Length > 0) {
                                    string word_tocomplete = tmp[tmp.Length - 1];
                                    string word_autocomplete = autocomplete_engine.AutoComplete(buffer);
                                    if (!String.IsNullOrEmpty(word_autocomplete) && word_autocomplete != word_tocomplete) {
                                        while (buffer.Length > 0 && buffer[buffer.Length - 1] != ' ') { RemoveOneChar(); }
                                        foreach (char c in word_autocomplete) { AddChar(c); }
                                    }
                                }
                            }
                            break;
                        case ConsoleKey.Spacebar:
                            if (!disablespace)
                                AddChar(k.KeyChar);

                            break;
                        default:
                            if (k.KeyChar != 0)
                                AddChar(k.KeyChar);
                            break;
                    }
                }
                reading_lock = false;
            }
            while (writing_lock) { }
            reading = false;
            previous.AddLast(buffer + buffer2);
            return buffer + buffer2;
        }

        /// <summary>
        /// Write a string to the standard output, without newline character
        /// </summary>
        public static void WriteFormated(string str, bool skipStartChar = false) {
            if (basicIO) { System.Console.Write(str); return; }
            if (!String.IsNullOrEmpty(str)) {
                //int hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second;
                //ConsoleIO.Write(hour.ToString("00") + ':' + minute.ToString("00") + ':' + second.ToString("00") + ' ');

                if (ConsoleIO.basicIO) {
                    ConsoleIO.CWrite(str, skipStartChar); return;
                }

                string[] subs = str.Split(new char[] { '§' });
                if (subs[0].Length > 0) { ConsoleIO.CWrite(subs[0], true); }
                for (int i = 1; i < subs.Length; i++) {
                    if (subs[i].Length > 0) {
                        switch (subs[i][0]) {
                            case '0': System.Console.ForegroundColor = ConsoleColor.Gray; break; //Should be Black but Black is non-readable on a black background
                            case '1': System.Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                            case '2': System.Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                            case '3': System.Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                            case '4': System.Console.ForegroundColor = ConsoleColor.DarkRed; break;
                            case '5': System.Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                            case '6': System.Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                            case '7': System.Console.ForegroundColor = ConsoleColor.Gray; break;
                            case '8': System.Console.ForegroundColor = ConsoleColor.DarkGray; break;
                            case '9': System.Console.ForegroundColor = ConsoleColor.Blue; break;
                            case 'a': System.Console.ForegroundColor = ConsoleColor.Green; break;
                            case 'b': System.Console.ForegroundColor = ConsoleColor.Cyan; break;
                            case 'c': System.Console.ForegroundColor = ConsoleColor.Red; break;
                            case 'd': System.Console.ForegroundColor = ConsoleColor.Magenta; break;
                            case 'e': System.Console.ForegroundColor = ConsoleColor.Yellow; break;
                            case 'f': System.Console.ForegroundColor = ConsoleColor.White; break;
                            case 'r': System.Console.ForegroundColor = ConsoleColor.White; break;
                        }

                        if (subs[i].Length > 1) {
                            ConsoleIO.CWrite(subs[i].Substring(1, subs[i].Length - 1), skipStartChar);
                        }
                    }
                }
            }
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void CWrite(string text, bool skipStartChar = false) {
            Write(text, skipStartChar);
        }

        public static void Write(string text, bool skipStartChar = false) {
            if (basicIO) { System.Console.Write(text); return; }
            while (reading_lock) { }
            writing_lock = true;
            if (reading) {
                ConsoleColor fore = System.Console.ForegroundColor;
                ConsoleColor back = System.Console.BackgroundColor;
                string buf = buffer;
                string buf2 = buffer2;
                ClearLineAndBuffer();
                if (System.Console.CursorLeft == 0) {
                    System.Console.CursorLeft = System.Console.BufferWidth - 1;
                    System.Console.CursorTop--;
                    System.Console.Write(' ');
                    System.Console.CursorLeft = System.Console.BufferWidth - 1;
                    System.Console.CursorTop--;
                } else System.Console.Write("\b \b");
                System.Console.Write(text);
                System.Console.ForegroundColor = ConsoleColor.Gray;
                System.Console.BackgroundColor = ConsoleColor.Black;
                buffer = buf;
                buffer2 = buf2;
                if (!skipStartChar) {
                    System.Console.Write("> " + buffer);
                }
                if (buffer2.Length > 0) {
                    System.Console.Write(buffer2 + " \b");
                    for (int i = 0; i < buffer2.Length; i++) { GoBack(); }
                }
                System.Console.ForegroundColor = fore;
                System.Console.BackgroundColor = back;
            } else System.Console.Write(text);
            writing_lock = false;
        }

        /// <summary>
        /// Write a string to the standard output with a trailing newline
        /// </summary>

        public static void WriteLine(string line) {
            Write(line + '\n');
        }

        /// <summary>
        /// Write a single character to the standard output
        /// </summary>

        public static void Write(char c) {
            Write("" + c);
        }

        /// <summary>
        /// Write a Minecraft-Formatted string to the standard output, using §c color codes
        /// </summary>
        /// <param name="str">String to write</param>
        /// <param name="acceptnewlines">If false, space are printed instead of newlines</param>
        public static void WriteLineFormatted(string str, bool acceptnewlines = true, bool Skip = true, bool setToLeft = false) {
            if (basicIO) { System.Console.WriteLine(str); return; }
            if (!String.IsNullOrEmpty(str)) {
                //int hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second;
                //ConsoleIO.Write(hour.ToString("00") + ':' + minute.ToString("00") + ':' + second.ToString("00") + ' ');

                if (setToLeft) { System.Console.CursorLeft = 0; }
                if (!acceptnewlines) { str = str.Replace('\n', ' '); }
                if (ConsoleIO.basicIO) {
                    ConsoleIO.WriteLine(str); return;
                }

                string[] subs = str.Split(new char[] { '§' });
                if (subs[0].Length > 0) { ConsoleIO.Write(subs[0], true); }
                for (int i = 1; i < subs.Length; i++) {
                    if (subs[i].Length > 0) {
                        switch (subs[i][0]) {
                            case '0': System.Console.ForegroundColor = ConsoleColor.Gray; break; //Should be Black but Black is non-readable on a black background
                            case '1': System.Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                            case '2': System.Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                            case '3': System.Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                            case '4': System.Console.ForegroundColor = ConsoleColor.DarkRed; break;
                            case '5': System.Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                            case '6': System.Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                            case '7': System.Console.ForegroundColor = ConsoleColor.Gray; break;
                            case '8': System.Console.ForegroundColor = ConsoleColor.DarkGray; break;
                            case '9': System.Console.ForegroundColor = ConsoleColor.Blue; break;
                            case 'a': System.Console.ForegroundColor = ConsoleColor.Green; break;
                            case 'b': System.Console.ForegroundColor = ConsoleColor.Cyan; break;
                            case 'c': System.Console.ForegroundColor = ConsoleColor.Red; break;
                            case 'd': System.Console.ForegroundColor = ConsoleColor.Magenta; break;
                            case 'e': System.Console.ForegroundColor = ConsoleColor.Yellow; break;
                            case 'f': System.Console.ForegroundColor = ConsoleColor.White; break;
                            case 'r': System.Console.ForegroundColor = ConsoleColor.White; break;
                        }

                        if (subs[i].Length > 1) {
                            ConsoleIO.Write(subs[i].Substring(1, subs[i].Length - 1), Skip);
                        }
                    }
                }
                ConsoleIO.Write("\n", Skip);
            }
            System.Console.ForegroundColor = ConsoleColor.Gray;
        }

        #region Subfunctions
        public static void ClearLineAndBuffer() {
            while (buffer2.Length > 0) { GoRight(); }
            while (buffer.Length > 0) { RemoveOneChar(); }
        }
        private static void RemoveOneChar() {
            if (buffer.Length > 0) {
                if (System.Console.CursorLeft == 0) {
                    System.Console.CursorLeft = System.Console.BufferWidth - 1;
                    System.Console.CursorTop--;
                    System.Console.Write(' ');
                    System.Console.CursorLeft = System.Console.BufferWidth - 1;
                    System.Console.CursorTop--;
                } else System.Console.Write("\b \b");
                buffer = buffer.Substring(0, buffer.Length - 1);

                if (buffer2.Length > 0) {
                    System.Console.Write(buffer2 + " \b");
                    for (int i = 0; i < buffer2.Length; i++) { GoBack(); }
                }
            }
        }
        private static void GoBack() {
            if (System.Console.CursorLeft == 0) {
                System.Console.CursorLeft = System.Console.BufferWidth - 1;
                System.Console.CursorTop--;
            } else System.Console.Write('\b');
        }
        private static void GoLeft() {
            if (buffer.Length > 0) {
                buffer2 = "" + buffer[buffer.Length - 1] + buffer2;
                buffer = buffer.Substring(0, buffer.Length - 1);
                System.Console.Write('\b');
            }
        }
        private static void GoRight() {
            if (buffer2.Length > 0) {
                buffer = buffer + buffer2[0];
                System.Console.Write(buffer2[0]);
                buffer2 = buffer2.Substring(1);
            }
        }
        private static void AddChar(char c) {
            System.Console.Write(c);
            buffer += c;
            System.Console.Write(buffer2);
            for (int i = 0; i < buffer2.Length; i++) { GoBack(); }
        }
        #endregion

        #region Clipboard management
        private static string ReadClipboard() {
            string clipdata = "";
            Thread staThread = new Thread(new ThreadStart(
                delegate {
                    try {
                        clipdata = Clipboard.GetText();
                    } catch { }
                }
            ));
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return clipdata;
        }
        #endregion
    }

    public interface IAutoComplete {
        string AutoComplete(string BehindCursor);
    }
}
