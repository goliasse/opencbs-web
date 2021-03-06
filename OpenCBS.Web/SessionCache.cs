﻿using System;
using System.Collections.Generic;
using OpenCBS.Web.Interface;
using OpenCBS.Web.Model;

namespace OpenCBS.Web
{
    public class SessionCache : ISessionCache
    {
        private readonly Dictionary<Guid, Session> _cache = new Dictionary<Guid, Session>();

        public void Set(Guid guid, Session session)
        {
            _cache[guid] = session;
        }

        public Session Get(Guid guid)
        {
            return _cache.ContainsKey(guid) ? _cache[guid] : null;
        }

        public void Remove(Guid guid)
        {
            _cache.Remove(guid);
        }
    }
}
