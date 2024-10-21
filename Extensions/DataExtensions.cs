using System;
using UnityEngine;

namespace ProjectName.Extensions
{
    public static class DataExtensions
    {
        public static bool Exists(this object target)
            => target != null;

        public static bool Exists(this UnityEngine.Object target)
            => target;

        public static bool NotExists(this object target)
            => target == null;

        public static bool NotExists(this UnityEngine.Object target)
            => !target;

        public static T With<T>(this T self, Action<T> set)
        {
            set.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> set, bool when)
        {
            if (when)
                set.Invoke(self);
            return self;
        }
    }
}