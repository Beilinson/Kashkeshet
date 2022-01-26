using BlazorClient.UI;
using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.ClientSide.Implementations;
using Kashkeshet.Common.Communicators;
using Kashkeshet.Common.Loaders;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BlazorClient
{
    public class ClientManager : ComponentBase 
    {
        public string Name { get; set; }
        public string Input { get; set; }

        public ICommunicator communicator;
        public BlazorMessageSender Sender { get; set; }

        public IClientRunnable receiver;
        public MessagesOutput Output { get; set; }

        private string _name;

        protected override void OnInitialized()
        {
            var endPointPort = 8080;
            var endPointIP = System.Net.IPAddress.Parse("127.0.0.1");

            var client = new System.Net.Sockets.TcpClient();
            client.Connect(endPointIP, endPointPort);

            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            communicator = new TcpCommunicator(client, formatter);

            Sender = new BlazorMessageSender();

            var loader = new FileLoader();
            Output = new MessagesOutput();
            receiver = new ClientReceiver(Output, loader);
            Task.Run(() => { receiver.Run(communicator); });
        }

        public void CreateUser()
        {
            _name = Name;
            Name = string.Empty;
        }

        public void ConnectToServer()
        {

        }

        public void SendMessage()
        {
            Sender.Input = Input;
            Input = String.Empty;
            Sender.Run(communicator);
        }
    }
}
