using Kashkeshet.Common.UI;
using System;

namespace Kashkeshet.ConsoleUI
{
    public class ConsoleOutput : IOutput
    {
        public void Output(object toOutput)
        {
            Console.WriteLine(toOutput);
        }
    }
}
