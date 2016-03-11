using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Chess
{
    public static class FileManager
    {
        #region Settings
        /// <summary>
        /// option dictionary
        /// </summary>
        public static Dictionary<string, string> options = new Dictionary<string, string>();
        private const string settings = "settings.txt";
        public static void Read_settings_file()
        {
            // default values
            options["Fullscreen"] = "False";
            options["BoardPosition"] = "Left";
            options["Animation"] = "False";
            options["ChessEngine"] = "Default";

            if (File.Exists(settings))
            {
                using (StreamReader file = new StreamReader(settings))
                {
                    string line;
                    string[] data;
                    while (!file.EndOfStream)
                    {
                        line = file.ReadLine();
                        data = line.Split();
                        options[data[0]] = data[1];
                    }
                }
            }
        }
        /// <summary>
        /// Save new option
        /// </summary>
        /// <param name="key">Option name</param>
        /// <param name="value">Option value</param>
        public static void Save_option(string key, string value)
        {
            options[key] = value;
        }
        public static void Write_settings_file()
        {
            if (File.Exists(settings))
                File.Delete(settings);

            using (StreamWriter file = File.AppendText(settings))
            {
                foreach (var data in options)
                    file.WriteLine(data.Key + " " + data.Value);
            }
        }
        #endregion

        #region Saves
        #endregion

        #region Log
        //private static List<string> logs = new List<string>();
        private const string log_file = "Log.txt";
        public static List<string> Log = new List<string>();
        public static List<string> ReadLog()
        {
            if (File.Exists(log_file))
            {
                List<string> log = new List<string>();
                using (StreamReader file = new StreamReader(log_file))
                {
                    while (!file.EndOfStream)
                    {
                        log.Add(file.ReadLine());
                    }
                }
                return log;
            }
            else return null;
        }
        public static void StartNewLog()
        {
            if (File.Exists(log_file))
                File.Delete(log_file);
        }
        public static void WriteToLog(string line)
        {
            using (StreamWriter file = File.AppendText(log_file))
            {
                Log.Add(line);
                file.WriteLine(line);
            }
        }
        public static void SaveGame(string game_name)
        {
            if (!Directory.Exists("Saves"))
                Directory.CreateDirectory("Saves");
            StreamWriter file = new StreamWriter("Saves\\" + game_name + ".log");
            foreach (string move in Log)
            {
                file.WriteLine(move);
            }
            file.Close();
        }
        #endregion

        //last game save
    }
}
