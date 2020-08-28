using System.Collections.Generic;
using System.Linq;
using ComposableCollections.Dictionary.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComposableCollections.AspNetCore
{
    public abstract class ComposableReadOnlyDictionaryControllerBase<TKey, TValue> : ControllerBase
    {
        private IComposableReadOnlyDictionary<TKey, TValue> _state;

        protected ComposableReadOnlyDictionaryControllerBase(IComposableReadOnlyDictionary<TKey, TValue> state)
        {
            _state = state;
        }

        protected ComposableReadOnlyDictionaryControllerBase()
        {
            
        }

        protected void Initialize(IComposableReadOnlyDictionary<TKey, TValue> state)
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
    }
}