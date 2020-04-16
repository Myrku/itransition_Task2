using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task2_2.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Укажите логин")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Логин должен быть от 0 до 50 символов")]
        public string name { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Пароль должен быть от 0 до 50 символов")]
        public string password { get; set; }

    }
}