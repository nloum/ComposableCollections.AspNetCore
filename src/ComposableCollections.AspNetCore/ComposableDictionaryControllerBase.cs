using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComposableCollections.AspNetCore
{
    public abstract class ComposableDictionaryControllerBase<TKey, TValue> : ControllerBase
    {
        private IComposableDictionary<TKey, TValue> _state;

        protected ComposableDictionaryControllerBase(IComposableDictionary<TKey, TValue> state)
        {
            _state = state;
        }

        protected ComposableDictionaryControllerBase()
        {
            
        }

        protected void Initialize(IComposableDictionary<TKey, TValue> state)
        {
            _state = state;
        }

        [HttpGet]
        public IEnumerable<TValue> GetMany([FromQuery] int? skip = null, [FromQuery] int? limit = null)
        {
            var result = _state.Values;
            if (skip != null)
            {
                result = result.Skip(skip.Value);
            }

            if (limit != null)
            {
                result = result.Take(limit.Value);
            }

            return result.AsEnumerable();
        }
        
        [HttpGet("{key}")]
        public TValue GetOne(TKey key)
        {
            return _state[key];
        }
        
        [HttpDelete("{key}")]
        public TValue RemoveOne(TKey key)
        {
            _state.Remove(key, out var value);
            return value;
        }

        [HttpPut("{key}")]
        public void AddOrUpdate(TKey key, [FromBody] TValue value)
        {
            _state.AddOrUpdate(key, value);
        }
    }
}