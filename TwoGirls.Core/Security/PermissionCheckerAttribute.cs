﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TwoGirls.Core.Services.Interfaces;

namespace TwoGirls.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly int _permissionId = 0;
        private IPermissionService _permissionService;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
                if (!_permissionService.PermissionCheker(userId, _permissionId))
                {
                    context.Result = new RedirectResult("/account/login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/account/login");
            }
        }
    }
}
