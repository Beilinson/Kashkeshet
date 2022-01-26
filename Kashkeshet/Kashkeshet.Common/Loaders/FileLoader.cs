using Kashkeshet.Common.UI;
using System.IO;

namespace Kashkeshet.Common.Loaders
{
    public class FileLoader : IFileLoader
    {
        public bool TryLoadFile(object possiblePath, out GenericFile file)
        {
            var fileExists = File.Exists(possiblePath.ToString());

            if (fileExists)
            {
                file = new GenericFile(possiblePath.ToString());
            }
            else
            {
                file = default;
            }
            return fileExists;
        }

        public bool IsFile(object possibleFile, out GenericFile file)
        {
            var isFile = possibleFile is GenericFile;
            if (isFile)
            {
                file = possibleFile as GenericFile;
            }
            else
            {
                file = default;
            }
            return isFile;
        }
    }
}
