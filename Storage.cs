﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPServer
{
    class Storage
    {
        //static string currentTimeStamp = DateTime.Now.ToString("ddMMyyyy");
        static string currentTimeStamp = DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);
        static string currentFile; // Path to event folder (log)
        static string lastMessageTimeStamp = "Not initialized yet ";
        static string connectionString; // Last updated connection string value
        static string connectionStringPath; // Connection string path

        public static void StorageInit() // Check does folders and files exist, and create them if not, this is for log folder and file
        {
            if (!Properties.Settings.Default.FormMainStorageCheckbox)
            {
                // If checkbox is not checked skip
                return;
            }

            bool IsFileCreated = false;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"log";
            
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path +=  @"\";

            if (!File.Exists(path + GetFolderTimeStamp() + @".txt"))
            {
                using (File.Create(path + GetFolderTimeStamp() + @".txt"))
                {
                    Console.WriteLine("New file created");
                    IsFileCreated = true;
                }
            }
            currentFile = path + GetFolderTimeStamp() + @".txt"; // Pointer to the last file
            if (IsFileCreated)
            {
                using (StreamWriter outputFile = new StreamWriter(currentFile, true))
                {
                    outputFile.WriteLine("Last event arrived at: " + lastMessageTimeStamp + "\r\n\r\n");
                }
            }
        }

        private static string GetFolderTimeStamp()
        {
            //string s = DateTime.Now.ToString("ddMMyyyy");
            string s = DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);

            return s;
        }

        public static void UpdateFolderTimeStamp()
        {
            //string temp = DateTime.Now.ToString("ddMMyyyy"); // Check is it new day
            string temp = DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture); // Check is it new day
            if (!temp.Equals(currentTimeStamp))
            {
                currentTimeStamp = temp;
                StorageInit(); // Update folders
            }

        }

        public static void AppendTextToFile(string s) // This is for log file
        {
            if (!Properties.Settings.Default.FormMainStorageCheckbox)
            {
                // If checkbox is not checked skip
                return;
            }
            
            // Append text to an existing file
            try
            {
                using (StreamWriter outputFile = new StreamWriter(currentFile, true))
                {
                    outputFile.WriteLine(s);
                    lastMessageTimeStamp = GetFolderTimeStamp();
                }
            }
            catch (DirectoryNotFoundException)
            {
                StorageInit();
                using (StreamWriter outputFile = new StreamWriter(currentFile, true))
                {
                    outputFile.WriteLine(s);
                }

            }
            
        }

        public static void test() // Function for testing purpose
        {
            string temp = DateTime.Now.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture);

        }

        public static string ConnectionStringInit() // Init connection string (recreate if deleted and fill wiht default values), and return it
        {
            //string connectionString;
            bool IsFileCreated = false;
            string path = AppDomain.CurrentDomain.BaseDirectory;

            //path += @"\";   "DATA SOURCE=192.168.2.98:1521/M2CDatabase;PERSIST SECURITY INFO=True;USER ID=NTPM;PASSWORD=NeticoPassword123;";

            try
            {
                if (!File.Exists(path + @"constr.txt"))
                {
                    using (File.Create(path + @"constr.txt"))
                    {
                        Console.WriteLine("New constr file created");
                        IsFileCreated = true;
                    }
                }
                connectionStringPath = path + @"constr.txt";
                if (IsFileCreated)
                {
                    using (StreamWriter outputFile = new StreamWriter(connectionStringPath, true))
                    {
                        outputFile.WriteLine("DATA SOURCE=192.168.1.1:1521/M2CDatabase;PERSIST SECURITY INFO=True;USER ID=Username;PASSWORD=TypePasswordHere;");
                        outputFile.WriteLine("Only first line will be read as connection string...");
                    }
                }

                using (StreamReader outputFile = new StreamReader(connectionStringPath, true))
                {
                    connectionString = outputFile.ReadLine(); // Always read first line
                }

                return connectionString;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong with connection string");
                return "DATA SOURCE=192.168.1.1:1521/M2CDatabase;PERSIST SECURITY INFO=True;USER ID=Username;PASSWORD=TypePasswordHere;";
            }
            
        }

        public static string ReadConnectionString() // Get connection string from file
        {
            try
            {
                using (StreamReader outputFile = new StreamReader(connectionStringPath, true))
                {
                    connectionString = outputFile.ReadLine(); // Always read first line
                }
            }
            catch (Exception)
            {

                Console.WriteLine(" Exception trown in ReadConnectionString method ");
            }
            

            return connectionString;
        }

        public static string GetConnectionStringPath() // Get path to connection string
        {
            return connectionStringPath;
        }

        public static void UpdateConnectionString(string s)
        {
            string wholeFIle;
            string subFIle = "";


            try
            {
                using (StreamReader outputFile = new StreamReader(connectionStringPath, true))
                {
                    wholeFIle = outputFile.ReadToEnd();
                }
                File.WriteAllText(connectionStringPath, String.Empty);
                if (!wholeFIle.Equals(""))
                {
                    subFIle = wholeFIle.Substring(wholeFIle.IndexOf(Environment.NewLine) + 2);
                }

                using (StreamWriter outputFile = new StreamWriter(connectionStringPath, true))
                {
                    outputFile.Write(s + Environment.NewLine + subFIle);
                }


            }
            catch (FileNotFoundException) // If file was deleted create a new one, and init with defalut values
            {
                Console.WriteLine("File not found exception, will create new file");
                ConnectionStringInit();
                Thread t = new Thread(() => MessageBox.Show("File not found, new file will be created")); // I dont want blocking message box
                t.Start();


            }
            catch (Exception)
            {
                Console.WriteLine(" Exception trown in UpdateConnectionString method ");
            }
        }
    }
}
