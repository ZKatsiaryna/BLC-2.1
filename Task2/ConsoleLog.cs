using System;
using System.IO;

namespace Task2
{
    class ConsoleLog : ILog
    {
        public void WriteCreationFileMessage(FileInfo file)
        {
            Console.WriteLine($"File created. Name: {file.Name}, Date: {file.CreationTime}");
        }

        public void WriteDeleteFileMessage(FileSystemEventArgs e)
        {
            Console.WriteLine($"File deleted. Name: {e.Name}");
        }

        public void WriteMoveFileMessage(FileSystemEventArgs e, string folder)
        {
            Console.WriteLine($"The rule is exist. The file {e.Name} has been moved to {folder} folder.");
        }

        public void WriteUnableMoveMessage(FileSystemEventArgs e, string folder)
        {
            Console.WriteLine($"The file {e.Name} has not been moved to {folder} folder.");
        }
    }
}
