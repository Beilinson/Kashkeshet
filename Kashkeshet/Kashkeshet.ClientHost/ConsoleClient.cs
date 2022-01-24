using Kashkeshet.ClientSide.Abstraction;
using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ClientHost
{
    public class ConsoleClient
    {
        private readonly IClient _client;

        private readonly IInput _input;

        public ConsoleClient(IClient client, IInput input)
        {
            _client = client;
            _input = input;
        }

        public void Run()
        {
            while (true)
            {
                var input = _input.Input();
                _client.Send(input);
            }
        }
    }
}
