using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerFactories.Implementations
{
    public class ProtocolResponseAlerts
    {
        public string ChatLeavingAlert { get; }
        public string ChatCreatedAlert { get; }
        public string AddedUserAlert { get; }
        public string NonExistantUserAlert { get; }
        public string EnteredChatAlert { get; }
        public string NonExistantChatAlert { get; }

        public ProtocolResponseAlerts(
            string chatLeavingAlert, 
            string chatCreatedAlert, 
            string addedUserAlert, 
            string nonExistantUserAlert, 
            string enteredChatAlert, 
            string nonExistantChatAlert)
        {
            ChatLeavingAlert = chatLeavingAlert;
            ChatCreatedAlert = chatCreatedAlert;
            AddedUserAlert = addedUserAlert;
            NonExistantUserAlert = nonExistantUserAlert;
            EnteredChatAlert = enteredChatAlert;
            NonExistantChatAlert = nonExistantChatAlert;
        }
    }
}
