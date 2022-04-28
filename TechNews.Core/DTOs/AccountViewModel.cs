using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TechNews.Core.DTOs
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "لطفا آدرس ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا از یک آدرس ایمیل معتبر استفاده نمایید")]
        [MaxLength(250, ErrorMessage = "آدرس ایمیل نمیتواند بیشتر از 250 کاراکتر باشد")]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا تلفن همراه را وارد کنید")]
        [MaxLength(50, ErrorMessage = "تلفن همراه نمیتواند بیشتر از 50 کاراکتر باشد")]
        public string PhoneNumber { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا نام را وارد کنید")]
        [MaxLength(250, ErrorMessage = "نام نمیتواند بیشتر از 250 کاراکتر باشد")]
        public string Username { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [MaxLength(250, ErrorMessage = "کلمه عبور نمیتواند بیشتر از 250 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        [MaxLength(250, ErrorMessage = "تکرار کلمه عبور نمیتواند بیشتر از 250 کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "کلمه عبور با تکرار کلمه عبور مغایرت دارد !")]
        public string ConfirmPassword { get; set; }
        public string? IpAddress { get; set; }


    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "لطفا آدرس ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا از یک آدرس ایمیل معتبر استفاده نمایید")]
        [MaxLength(250, ErrorMessage = "آدرس ایمیل نمیتواند بیشتر از 250 کاراکتر باشد")]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد کنید")]
        [MaxLength(250, ErrorMessage = "کلمه عبور نمیتواند بیشتر از 250 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "لطفا آدرس ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "لطفا از یک آدرس ایمیل معتبر استفاده نمایید")]
        [MaxLength(250, ErrorMessage = "آدرس ایمیل نمیتواند بیشتر از 250 کاراکتر باشد")]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا کلمه عبور فعلی خود را وارد کنید")]
        [MaxLength(250, ErrorMessage = "کلمه عبور نمیتواند بیشتر از 250 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا کلمه عبور جدید خود را وارد کنید")]
        [MaxLength(250, ErrorMessage = "کلمه عبور نمیتواند بیشتر از 250 کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا تکرار کلمه عبور را وارد کنید")]
        [MaxLength(250, ErrorMessage = "تکرار کلمه عبور نمیتواند بیشتر از 250 کاراکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "کلمه عبور با تکرار کلمه عبور مغایرت دارد !")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangeUserDetailsViewModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }
        public string IdentifierCode { get; set; }
        public int RoleId { get; set; }

    }
}
