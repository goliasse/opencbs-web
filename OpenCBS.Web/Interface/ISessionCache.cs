
using System;
using OpenCBS.Web.Model;

namespace OpenCBS.Web.Interface
{
    public interface ISessionCache
    {
        void Set(Guid guid, Session session);
        Session Get(Guid guid);
        void Remove(Guid guid);
    }
}
