using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoaTool.Define.Common;

namespace LoaTool.Define.Interfaces
{
    public interface IResourceService
    {
        TValue GetResource<TKey, TValue>(TKey key) where TKey : Enum where TValue : IResource;
        bool AddResource<TValue>(TValue resource) where TValue : IResource;
    }
}
