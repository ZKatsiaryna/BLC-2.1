using System.Collections.Generic;
using System.IO;


namespace Task2
{
    class FolderScaner
    {
        public string DestinationPath { get; private set; }
        public string DefaultPath { get; private set; }
        public string ScanFolder { get; private set; }
        public List<string> Extension { get; private set; }
        private ILog Log { get; set; }
        private FileSystemWatcher Watcher { get; set; }

        public FolderScaner(string scanFolder, string destinationPath, string defaultPath, List<string> extension, ILog log)
        {
            Log = log;
            Watcher = new FileSystemWatcher();
            Watcher.Path = scanFolder;
            Watcher.Created += watcher_Created;
            Watcher.Deleted += watcher_Deleted;

            ScanFolder = scanFolder;
            DestinationPath = destinationPath;
            DefaultPath = defaultPath;
            Extension = extension;
        }

        public void StartScan()
        {
            Watcher.EnableRaisingEvents = true;
        }

        public void StopScan()
        {
            Watcher.EnableRaisingEvents = false;
        }

        private void watcher_Deleted(object sender, FileSystemEventArgs e)
        {

            Log.WriteDeleteFileMessage(e);
        }

        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            string folder;
            FileInfo file = new FileInfo(e.FullPath);
            Log.WriteCreationFileMessage(file);

            DirectoryInfo desDir = new DirectoryInfo(DestinationPath);
            DirectoryInfo defaultDir = new DirectoryInfo(DefaultPath);
            if (!desDir.Exists)
            {
                desDir.Create();
            }
            if (!defaultDir.Exists)
            {
                defaultDir.Create();
            }

            folder = Extension.Exists(i => i.Equals(file.Extension)) ? DestinationPath : DefaultPath;

            try
            {
                WaitForDocToPublish(file);
                MoveFileToFolder(e, folder);
                Log.WriteMoveFileMessage(e, folder);
            }
            catch
            {
                Log.WriteUnableMoveMessage(e, folder);
            }
        }

        private bool IsFileInUse(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            //file is not locked
            return false;
        }

        private void WaitForDocToPublish(FileInfo file, int time = 100)
        {
            while (IsFileInUse(file) || time > 60000)
            {
                time += 100;
            }
        }

        private void MoveFileToFolder(FileSystemEventArgs e, string destinationPath)
        {
            File.Move(e.FullPath, Path.Combine(destinationPath, e.Name));
        }

    }
}
