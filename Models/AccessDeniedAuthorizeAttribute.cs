using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace langfvn.Models
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            // Khi người dùng không được xác thực, nó sẽ trả về HTTP 401, gửi chúng đến trang đăng nhập.
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/admin/accounts/login");
                return;
            }
            // Khi người dùng đăng nhập, nhưng không ở vai trò bắt buộc, nó sẽ tạo một NotAuthorizedResult thay thế. 
            // Hiện tại điều này chuyển hướng đến một trang lỗi.
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/admin/accounts/denied");
            }
        }
    }
}