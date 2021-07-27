using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace VaxineApp.AndroidNativeApi
{
    public interface ISave
    {
        //Method to save document as a file and view the saved document
        Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}
