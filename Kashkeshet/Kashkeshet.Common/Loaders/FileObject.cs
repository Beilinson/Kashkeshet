using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kashkeshet.Common.Loaders
{
    [Serializable]
    public class FileObject
    {
        private readonly string _fullPath;
        private readonly string _fileType;
        private readonly byte[] _data;

        public FileObject(string path)
        {
            _fullPath = Path.GetFullPath(path);
            _data = File.ReadAllBytes(_fullPath);
            _fileType = Path.GetExtension(_fullPath);
        }

        public void WriteFileToPath(string path)
        {
            var fs = new FileStream(path += _fileType, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(_data);
            fs.Close();
        }

        public override string ToString()
        {
            return _fullPath;
        }
    }
}
