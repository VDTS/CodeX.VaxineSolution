using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.MVVMHelper
{
    public interface IDataCrud
    {
        public void Get();
        public void Put();
        public void Post();
        public void Delete();
    }
}
