using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace langfvn.Models
{
    public class RegisterModel
    {
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Bạn phải nhập Email để đăng ký")]
        public String Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Bạn phải nhập Mật khẩu để đăng ký")]
        [StringLength(20, MinimumLength =6 ,ErrorMessage ="Mật khẩu phải có ít nhất 6 kí tự")]
        public String Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Bạn phải Xác nhật mật khẩu để đăng ký")]
        [Compare("Password",ErrorMessage ="Xác nhận mật khẩu không khớp.")]
        public String ConfirmPassword { get; set; }

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public String FullName { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public String Phone { get; set; }
    }
}