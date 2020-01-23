using Negative_Association_Rules_Miner.model.mining;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Negative_Association_Rules_Miner.service
{
    interface IFileStorage
    {
        IList<StorageFile> GetFiles();
        bool AddNewFile(string path);
        bool RemoveFile(int key);
    }
    class FileStorage:IFileStorage
    {
        private List<StorageFile> StoredFiles { get; set; } = new List<StorageFile>();
        private int CurrentIndex { get; set; } = 0;
        public IList<StorageFile> GetFiles()
        {
            return StoredFiles;
        }

        public bool AddNewFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException();
                StoredFiles.Add(new StorageFile
                {
                    Key = CurrentIndex,
                    Path = path
                });
                CurrentIndex++;
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception)
            {
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
    }
}
