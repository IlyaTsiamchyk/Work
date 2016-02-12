using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangDetector.Domain
{
    public interface IRequestRepository
    {
        void InsertOrUpdate(Request request, bool isInsert);
        void InsertOrUpdate(RequestInfo requestInfo, bool isInsert);
        void Save();
    }
}
