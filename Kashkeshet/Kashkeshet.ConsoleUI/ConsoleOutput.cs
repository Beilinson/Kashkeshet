using Kashkeshet.Common.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ConsoleUI
{
    public class ConsoleOutput : IOutput
    {
        public void Output(object toOutput)
        {
            Console.Write(toOutput);
        }
    }
}
