using System;

namespace VchyORMCommon
{
    public static class Common
    {
        public static bool IsEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }

        public static bool IsNull<T>(this T model)
            where T : class
        => model == null;

        public static bool IsNotNull<T>(this T model)
            where T : class
            => !model.IsNull();
    }
}
