using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangDetector.Domain
{
    public interface IDALContext
    {
        IRequestRepository Requests { get; }
    }
}
