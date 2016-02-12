using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LangDetector.Domain;

namespace LangDetector.Models
{
    public class DALContext : IDALContext
    {
        private ApplicationDbContext _databaseContext;
        private IRequestRepository _requests;

        public DALContext()
        {
            _databaseContext = new ApplicationDbContext();
        }

        public IRequestRepository Requests
        {
            get
            {
                if (_requests == null)
                {
                    _requests = new RequestRepository(_databaseContext);
                }
                return _requests;
            }
        }
    }
}