using System.IO;


namespace Task2
{
    interface ILog
    {
        void WriteDeleteFileMessage(FileSystemEventArgs e);

        void WriteCreationFileMessage(FileInfo e);

        void WriteMoveFileMessage(FileSystemEventArgs e, string folder);

        void WriteUnableMoveMessage(FileSystemEventArgs e, string folder);
    }
}
