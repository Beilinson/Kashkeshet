using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kashkeshet.Common.Loaders
{
    [Serializable]
    public class GenericFile
    {
        private readonly string _fullPath;
        private readonly string _fileType;
        private readonly byte[] _data;

        public GenericFile(string path)
        {
            _fullPath = Path.GetFullPath(path);
            _data = File.ReadAllBytes(_fullPath);
            _fileType = Path.GetExtension(_fullPath);
        }

        public void WriteFileToPath(string directory, string name)
        {
            CheckDirectory(directory);
            var fs = new FileStream(JoinPath(directory, name), FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(_data);
            fs.Close();
        }

        public override string ToString()
        {
            return _fullPath;
        }

        private void CheckDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        private string JoinPath(string directory, string name)
        {
            return $"{directory}/{name}{_fileType}";
        }
    }
}
