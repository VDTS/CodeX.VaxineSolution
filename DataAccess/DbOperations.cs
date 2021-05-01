using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface DbOperations
    {
        public void AddDoc();
        public List<Object> GetNodes();
    }
}
