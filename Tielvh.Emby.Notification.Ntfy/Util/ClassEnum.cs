using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tielvh.Emby.Notification.Ntfy.Exceptions;

namespace Tielvh.Emby.Notification.Ntfy.Util
{
    public abstract class ClassEnum<TEnum, TValue> where TEnum : ClassEnum<TEnum, TValue>
    {
        public string Name { get; }
        public TValue Value { get; }

        protected ClassEnum(string name, TValue value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name cannot be null or empty", nameof(name));

            Name = name;
            Value = value;
        }

        private static IEnumerable<TEnum> GetAllFromAssembly()
        {
            var type = typeof(TEnum);
            return Assembly
                .GetAssembly(type)
                .GetTypes()
                .Where(t => t.IsAssignableFrom(type))
                .SelectMany(t =>
                    t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                        .Where(f => t.IsAssignableFrom(f.FieldType))
                        .Select(fi => (TEnum)fi.GetValue(null)));
        }

        private static IDictionary<string, TEnum> GetAllByName() => GetAllFromAssembly().ToDictionary(i => i.Name);

        public static TEnum FromName(string name) => GetAllByName().TryGetValue(name, out var @enum)
            ? @enum
            : throw new ClassEnumNotDefinedException();
    }
}