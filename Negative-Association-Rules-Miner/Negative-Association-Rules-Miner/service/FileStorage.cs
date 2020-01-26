using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Negative_Association_Rules_Miner.service
{
    public interface IFileStorage
    {
        IList<StorageFile> GetFiles();
        bool AddNewFile(string path, bool copy = true);
        bool RemoveFile(int key);
        int GetNumberOfFiles();
    }
    public class FileStorage:IFileStorage
    {
        private string AppDataPath { get; set; }
        private string SetsPath { get; set; }
        private List<StorageFile> StoredFiles { get; set; } = new List<StorageFile>();
        private int CurrentIndex { get; set; } = 0;

        public FileStorage()
        {
            AppDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "PoNowemu",
                "RulesMiner");
            Directory.CreateDirectory(AppDataPath);
            SetsPath = Path.Combine(AppDataPath, "Sets");
            Directory.CreateDirectory(SetsPath);
            AddLocalFiles();
        }

        public IList<StorageFile> GetFiles()
        {
            return StoredFiles;
        }

        public bool AddNewFile(string path, bool copy = true)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException();
                if (copy)
                    File.Copy(path,Path.Combine(SetsPath,Path.GetFileName(path)));
                StoredFiles.Add(new StorageFile
                {
                    Key = CurrentIndex,
                    Path = path
                });
                CurrentIndex++;
                return true;
            }
            catch (FileNotFoundException ex)
            {
                Logger.Log(ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                return false;
            }
        }

        public bool RemoveFile(int key)
        {
            try
            {
                if (StoredFiles.Where(x => x.Key == key).Count() == 0)
                {
                    throw new KeyNotFoundException();
                }
                StoredFiles.RemoveAll(x => x.Key == key);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetNumberOfFiles()
        {
            return CurrentIndex;
        }

        private void AddLocalFiles()
        {
            foreach (var file in Directory.GetFiles(SetsPath))
            {
                AddNewFile(file,false);
            }
        }
    }
}
