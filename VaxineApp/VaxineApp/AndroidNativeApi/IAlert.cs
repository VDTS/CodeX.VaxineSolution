using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VaxineApp.AndroidNativeApi
{
    public interface IAlert
    {
        Task<string> Display(string title, string message, string accept, string reject, string cancel);
    }
}
