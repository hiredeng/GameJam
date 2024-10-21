using System;
using System.Collections.Generic;

namespace ProjectName.Collections.Generic
{
    public abstract class ProviderBase<TValue> : IProvider<TValue> where TValue : class
    {
        public IEnumerable<TValue> Values { get; }

        public event Action<TValue> Added;
        public event Action<TValue> Removed;

        protected List<TValue> _values = new List<TValue>();

        public void Add(TValue target)
        {
            _values.Add(target);
            OnAdd(target);
        }

        public void Remove(TValue target)
        {
            _values.Remove(target);
            OnRemove(target);
        }

        protected virtual void OnAdd(TValue target) 
            => Added?.Invoke(target);

        protected virtual void OnRemove(TValue target)
            => Removed?.Invoke(target);
    }
}