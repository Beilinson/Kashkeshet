using System;
using System.Collections.Generic;
using System.Text;

namespace Kashkeshet.Common.Communicators
{
    [Serializable]
    public class TestClass
    {
        public int MyProperty { get; set; }
        public TestClass(int a)
        {
            MyProperty = a;
        }
    }
}
