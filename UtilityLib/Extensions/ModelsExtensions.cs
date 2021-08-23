using System;
using System.Collections.Generic;
using System.Text;

namespace UtilityLib.Extensions
{
    public static class ModelsExtensions
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
