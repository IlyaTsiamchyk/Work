using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LangDetector.Domain;

namespace LangDetector.Models
{
    public class RequestRepository : IRequestRepository
    {
        private ApplicationDbContext _databaseContext;

        public RequestRepository(ApplicationDbContext context)
        {
            _databaseContext = context;
        }

        public void InsertOrUpdate(RequestInfo requestInfo, bool isInsert)
        {
            if (isInsert) //TODO: try.
            {
                _databaseContext.RequestsInfo.Add(requestInfo);
            }
            else
            {
                _databaseContext.Entry(requestInfo).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void InsertOrUpdate(Request request, bool isInsert)
        {
            if (isInsert) //TODO: try.
            {
                _databaseContext.Requests.Add(request);
            }
            else
            {
                _databaseContext.Entry(request).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}