using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModel
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage ="mật khẩu mới là bắt buộc ahaha!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="Mật khẩu mới và mật khẩu nhập lại không trùng nhau! ")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}
