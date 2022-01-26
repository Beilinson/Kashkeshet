using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.UI
{
    public class MessagesOutput : IOutput
    {
        public List<string> OutputtedMessages { get; }

        public MessagesOutput()
        {
            OutputtedMessages = new List<string>();
        }

        public void Output(object toOutput)
        {
            OutputtedMessages.Add(toOutput.ToString());
        }
    }
}
