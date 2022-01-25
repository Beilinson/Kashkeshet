using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.Common.Communicators
{
    public enum ChatProtocol
    {
        Message,
        File,
        Audio,
        DataRequest,

        RequestUsers,
        RequestGroups,
        CreateGroup,
        ChangeGroup,
    }
}
