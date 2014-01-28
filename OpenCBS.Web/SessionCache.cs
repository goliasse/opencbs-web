using System;
using System.Collections.Generic;
using OpenCBS.Web.Interface;
using OpenCBS.Web.Model;

namespace OpenCBS.Web
{
    public class SessionCache : ISessionCache
    {
        private readonly Dictionary<Guid, User> _cache = new Dictionary<Guid, User>();

        public void Set(Guid guid, User user)
        {
            _cache[guid] = user;
        }

        public User Get(Guid guid)
        {
            return _cache.ContainsKey(guid) ? _cache[guid] : null;
        }

        public void Remove(Guid guid)
        {
            _cache.Remove(guid);
        }
    }
}
