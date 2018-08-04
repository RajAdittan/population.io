using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Population.IO
{
    public interface ICommandReader<T>
    {
        T Current { get; }
        bool MoveNext();
    }
}
