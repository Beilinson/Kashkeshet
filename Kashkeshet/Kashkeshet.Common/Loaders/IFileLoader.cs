using Kashkeshet.Common.FileTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kashkeshet.Common.UI
{
    public interface IFileLoader
    {
        bool TryLoadFile(object possiblePath, out IFile file);
        bool IsFile(object possibleFile, out IFile file);
    }
}
