using System;

namespace Kashkeshet.Common.Communicators
{
    [Serializable]
    public enum ChatProtocol
    {
        Message,
        File,
        Audio,

        RequestHistory,
        RequestAllUsers,
        RequestLeaveGroup,
        RequestCreateGroup,
        RequestAddUser,
        RequestChangeGroup,
        RequestAvailableGroups,
    }
}
