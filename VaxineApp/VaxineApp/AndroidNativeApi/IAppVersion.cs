using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.AndroidNativeApi
{
    public interface IAppVersion
    {
        string GetVersion();
        int GetBuild();
    }
}
