﻿using System.Net.Sockets;

namespace Kashkeshet.Common.Communicators
{
    public interface ICommunicator
    {
        TcpClient Client { get; }

        void Send(object senderID, object obj, ChatProtocol protocol);
        (object sender, object obj, ChatProtocol protocol) Receive();
    }
}
