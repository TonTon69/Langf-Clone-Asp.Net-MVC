using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace langfvn.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Bạn vui lòng nhập tài khoản")]
        public String UserName { set; get; }
        [Required(ErrorMessage = "Bạn vui lòng nhập mật khẩu")]
        public String Password { set; get; }
       
        public bool RememberMe { set; get; }

    }
}