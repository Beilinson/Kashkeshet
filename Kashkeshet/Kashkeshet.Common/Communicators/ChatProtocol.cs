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

        RequestUsers,
        LeaveGroup,
        CreateGroup,
        AddUser,
        ChangeGroup,
        GetAvailableGroups,
    }
}
