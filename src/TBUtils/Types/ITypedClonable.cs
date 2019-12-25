using System;
using System.Collections.Generic;
using System.Text;

namespace TBUtils.Types
{
    public interface ITypedClonable<T>
    {
        T Clone();
    }
}
