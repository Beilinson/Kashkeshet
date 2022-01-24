﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.ServerSide.Core
{
    // Exists because it might have a bigger role in the future
    public interface IRoutable
    {
        IMessageHistory MessageHistory { get; }
        void UpdateHistory(object message);
    }
}
