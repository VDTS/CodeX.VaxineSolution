using DataAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Services
{
    public interface IDbService
    {
        public string Post(string data, string Node);
        public Task<string> Get(string Node);
    }
}
