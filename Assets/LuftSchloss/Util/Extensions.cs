using System;
using System.Collections.Generic;

namespace LuftSchloss {
    public static class Extensions {
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB) {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        public static void SafeCall(this Action action) {
            if (action != null) {
                action();
            }
        }

        public static void SafeCall<T>(this Action<T> action, T arg) {
            if (action != null) {
                action(arg);
            }
        }

        public static void SafeCall<T, K>(this Action<T, K> action, T arg1, K arg2) {
            if (action != null) {
                action(arg1, arg2);
            }
        }
    }
}
