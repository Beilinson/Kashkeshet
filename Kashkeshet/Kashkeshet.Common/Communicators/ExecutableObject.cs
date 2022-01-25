using System;

namespace Kashkeshet.Common.Communicators
{
    public class ExecutableObject
    {
        private readonly Action<object> _executable;
        private object _data;

        public ExecutableObject(Action<object> executable)
        {
            _executable = executable;
        }

        public void SetData(object data)
        {
            _data = data;
        }

        public void Run()
        {
            _executable?.Invoke(_data);
        }
    }
}
