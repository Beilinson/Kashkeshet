using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.Common.Communicators
{
    [Serializable]
    public enum ChatProtocol
    {
        Message,
        File,
        Audio,

        RequestAllUsers,
        RequestLeaveGroup,
        RequestCreateGroup,
        RequestAddUser,
        RequestChangeGroup,
        RequestAvailableGroups,
    }
}
