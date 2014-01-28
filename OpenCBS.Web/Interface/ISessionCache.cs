
using System;
using OpenCBS.Web.Model;

namespace OpenCBS.Web.Interface
{
    public interface ISessionCache
    {
        void Set(Guid guid, User user);
        User Get(Guid guid);
        void Remove(Guid guid);
    }
}
