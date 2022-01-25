using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.Common.User
{
    [Serializable]
    public class UserData
    {
        public string Name { get; }
        public int ID { get; }

        public UserData(string name, int iD)
        {
            Name = name;
            ID = iD;
        }
    }
}
