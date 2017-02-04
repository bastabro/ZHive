using System;

namespace ZHive.Utilities
{
    public class Helpers
    {
        public static bool ContainsNullOrEmpty(params object[] Items)
        {
            if (Items == null || Items.Length == 0)
                return true;

            bool result = false;
            foreach (var item in Items)
            {
                if (item == null)
                    result = true;

                if (item is string)
                    if (string.IsNullOrWhiteSpace(item as string))
                        result = true;
            }
            return result;
        }
    }
}
