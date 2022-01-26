using Kashkeshet.ServerSide.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerFactories.Abstractions
{
    public interface IChatFactory
    {
        IRoutable CreateBasicChat(string name);
    }
}
