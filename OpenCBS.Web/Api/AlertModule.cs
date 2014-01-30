﻿using Nancy;
using OpenCBS.Web.Interface.Repository;
using OpenCBS.Web.Model;
using System.Linq;

namespace OpenCBS.Web.Api
{
    public class AlertModule : SecureModule
    {
        public AlertModule(IAlertRepository alertRepository)
        {
            Get["/api/alerts"] = parameters =>
            {
                var user = (User) Context.CurrentUser;
                return new
                {
                    alerts = alertRepository
                        .FindAll(user.Id)
                        .Select(x => new
                        {
                            contractCode = x.ContractCode,
                            lateDays = x.LateDays
                        })
                };
            };
        }
    }
}