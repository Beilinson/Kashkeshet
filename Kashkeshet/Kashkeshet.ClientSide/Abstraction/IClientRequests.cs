using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientSide.Abstraction
{
    public interface IClientRequests : IClientRunnable
    {
        void CreateGroupChat();
        void CreatePrivateChat();
        void GetAvailableChats();
        void EnterChat();
        void LeaveChat();
    }
}
