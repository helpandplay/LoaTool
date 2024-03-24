using System.Windows;
using LoaTool.Define.Common;
using LoaTool.Define.Interfaces;

namespace LoaTool.Util;
public class ResourceService : IResourceService
{
    private readonly Dictionary<object, object> _resources = new Dictionary<object, object>();
    public TValue GetResource<TKey, TValue>(TKey key) where TKey : Enum where TValue : IResource
    {
        foreach(var kvp in _resources)
        {
            if(kvp.Key.GetType() == typeof(TKey))
            {
                var _key = (TKey)kvp.Key;
                if(_key.Equals(key))
                {
                    return (TValue)kvp.Value;
                }
            }
        }

        throw new ResourceReferenceKeyNotFoundException("Not found key", key);
    }

    public bool AddResource<TValue>(TValue resource) where TValue : IResource
    {
        if(!_resources.ContainsKey(resource.FileType))
        {
            _resources.Add(resource.FileType, resource);
            return true;
        }
        return false;
    }
}
