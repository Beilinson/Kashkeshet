using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kashkeshet.Common.Loaders
{
    public class FileLoader : IFileLoader
    {
        public bool TryLoadFile(object possiblePath, out FileObject file)
        {
            var path = possiblePath as string;
            var fileExists = File.Exists(path);
            Console.WriteLine(fileExists ? $"File - {path} - exists." : $"File - {path} - does not exist.");
            if (fileExists)
            {
                file = new FileObject(path);
            }
            else
            {
                file = default;
            }
            return fileExists;
        }
    }
}
