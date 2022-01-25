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

        RequestUsers,
        RequestGroups,
        CreateGroup,
        ChangeGroup,
    }
}
