using Kashkeshet.Common.FileTypes;

namespace Kashkeshet.Common.Factories.Abstractions
{
    public interface IFileFactory
    {
        IFile CreateFile(string path);
    }
}
