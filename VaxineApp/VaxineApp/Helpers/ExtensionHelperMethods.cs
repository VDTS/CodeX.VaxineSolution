using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Helpers
{
    public static class ExtensionHelperMethods
    {
        public static bool AreEmpty<T>(this T modelClass)
        {
            if (modelClass is object)
            {
                Type modelClassObj = modelClass.GetType();
                return (bool)modelClassObj.GetMethod("IsEmpty").Invoke(modelClass, null);
            }
            return true;
        }
    }
}
