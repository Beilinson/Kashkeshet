using Kashkeshet.Common.Factories.Abstractions;
using Kashkeshet.Common.FileTypes;

namespace Kashkeshet.Common.Factories.Implementations
{
    public class GenericFileFactory : IFileFactory
    {
        public IFile CreateFile(string path)
        {
            return new GenericFile(path);
        }
    }
}
