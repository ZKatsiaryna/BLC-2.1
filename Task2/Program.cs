using System;
using System.Collections.Generic;
using System.Configuration;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string scanPath = @"d:\1";


            Console.WriteLine("The monitors of {0} started", scanPath);
            Console.WriteLine("Please press Ctrl + C to finished the programm");


            string defaultPath = ConfigurationSettings.AppSettings["defaultPath"];
            string destinationPath = ConfigurationSettings.AppSettings["destinationPath"];
            
            List<string> extension = new List<string> { ".pdf", ".txt", ".png", ".docx" };

            ILog ConsoleLog = new ConsoleLog();
            FolderScaner folder = new FolderScaner(scanPath, destinationPath, defaultPath, extension, ConsoleLog);
            folder.StartScan();

            Console.ReadKey();
            folder.StopScan();

        }
    }
}
