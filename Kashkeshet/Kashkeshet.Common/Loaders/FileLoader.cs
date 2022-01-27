using Kashkeshet.Common.Factories.Abstractions;
using Kashkeshet.Common.FileTypes;
using Kashkeshet.Common.UI;
using System.IO;

namespace Kashkeshet.Common.Loaders
{
    public class FileLoader : IFileLoader
    {
        private readonly IFileFactory _fileFactory;

        public FileLoader(IFileFactory fileFactory)
        {
            _fileFactory = fileFactory;
        }

        public bool TryLoadFile(object possiblePath, out IFile file)
        {
            var fileExists = File.Exists(possiblePath.ToString());

            if (fileExists)
            {
                file = _fileFactory.CreateFile(possiblePath.ToString());
            }
            else
            {
                file = default;
            }
            return fileExists;
        }

        public bool IsFile(object possibleFile, out IFile file)
        {
            var isFile = possibleFile.GetType() == typeof(IFile);
            if (isFile)
            {
                file = possibleFile as IFile;
            }
            else
            {
                file = default;
            }
            return isFile;
        }
    }
}
