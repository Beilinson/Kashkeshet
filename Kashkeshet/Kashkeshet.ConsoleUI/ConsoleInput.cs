using Kashkeshet.Common.UI;
using System;

namespace Kashkeshet.ConsoleUI
{
    public class ConsoleInput : IInput
    {
        public object Input()
        {
            return Console.ReadLine();
        }
    }
}
