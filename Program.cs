using System;
using System.IO;
using System.Reflection;

namespace mwfpsunlocker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.Write("roblox fps unlocker by ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Manti\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            try
            {
                Console.WriteLine("obtaining local application data folder path");
                string versionspath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                Console.WriteLine("loading fflags file (from this exe)");
                Stream resource = Assembly.GetCallingAssembly().GetManifestResourceStream("mwfpsunlocker.resource");
                if (resource == null)
                {
                    throw new Exception("resource file null");
                }
                Console.WriteLine("finding roblox versions folder");
                versionspath = versionspath + "\\Roblox\\Versions";
                if (!Directory.Exists(versionspath))
                {
                    throw new Exception("roblox versions folder not found");
                }
                Console.Write("versions folder found: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(versionspath);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("finding valid version folders");
                bool foundany = false;
                bool editedany = false;
                foreach (string dir in Directory.GetDirectories(versionspath)) 
                { 
                    if (File.Exists(dir+"\\RobloxPlayerBeta.exe"))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        if (foundany)
                        {
                            Console.Write("found another roblox version folder: ");
                        }
                        else
                        {
                            Console.Write("found roblox version folder: ");
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(dir);
                        string jsonpath = dir + "\\ClientSettings\\ClientAppSettings.json";
                        Console.Write("write file at ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\"" + jsonpath + "\"");
                        Console.WriteLine("? (y/n)");
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (Console.ReadLine().ToLower()=="y")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("writing to " + jsonpath);
                            Directory.CreateDirectory(dir + "\\ClientSettings");
                            FileStream jsonfile = File.Open(jsonpath, FileMode.OpenOrCreate);
                            resource.CopyTo(jsonfile);
                            jsonfile.Close();
                            editedany = true;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("cancelled");
                        }
                        foundany = true;
                    }
                }
                if (!foundany)
                {
                    throw new Exception("no valid roblox version folder found");
                }
                if (editedany) 
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("successfully attempted to unlock fps, restart roblox to apply changes");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("no changes applied");
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("press enter to exit");
            Console.ReadLine();
        }
    }
}
