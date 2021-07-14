using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace langfvn.Models
{
    public class ChangePasswordViewModel
    {
        [StringLength(100)]
        public string Email { get; set; }

        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [StringLength(100)]
        [MinLength(6, ErrorMessage = "Mật khẩu phải ít nhất 6 kí tự")]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        //[DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
}