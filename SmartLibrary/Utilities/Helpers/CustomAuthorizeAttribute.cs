using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Utilities.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Nếu đã đăng nhập nhưng không có quyền -> chuyển đến trang AccessDenied
                filterContext.Result = new RedirectResult("~/Home/AccessDenied");
            }
            else
            {
                // Nếu chưa đăng nhập -> chuyển đến trang đăng nhập
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}