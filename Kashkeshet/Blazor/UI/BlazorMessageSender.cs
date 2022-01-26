using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.Communicators;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.UI
{
    public class BlazorMessageSender : IClientRunnable
    {
        public string Input { get; set; }

        public void Run(ICommunicator communicator)
        {
            communicator.Send((communicator.ToString(), Input, ChatProtocol.Message));
        }
    }
}
