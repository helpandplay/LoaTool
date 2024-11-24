using LoaTool.Define.Common;

namespace LoaTool.Define.Interfaces
{
    public interface IResourceService
    {
        TValue GetResource<TKey, TValue>(TKey key) where TKey : Enum where TValue : IResource;
        bool AddResource<TValue>(TValue resource) where TValue : IResource;
    }
}
