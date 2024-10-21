using System;
using System.Collections.Generic;

namespace ProjectName.Collections.Generic
{
    public interface IProvider<TValue> where TValue : class
    {
        public IEnumerable<TValue> Values { get; }

        public event Action<TValue> Added;

        public event Action<TValue> Removed;

        public void Add(TValue target);

        public void Remove(TValue target);
    }
}