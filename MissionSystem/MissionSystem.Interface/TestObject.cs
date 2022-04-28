using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionSystem.Interface
{
    public struct Obj<T>
    {
        public T Value { get; set; }

        public Obj(T value)
        {
            Value = value;
        }

        public T Get()
        {
            return Value;
        }
    }
}
