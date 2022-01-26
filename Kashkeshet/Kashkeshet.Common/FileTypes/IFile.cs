using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.Common.FileTypes
{
    public interface IFile
    {
        void WriteFileToPath(string directory, string name);
    }
}
