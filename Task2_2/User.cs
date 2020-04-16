//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Task2_2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Укажите логин")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Логин должен быть от 0 до 50 символов")]
        public string name { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Пароль должен быть от 0 до 50 символов")]
        public string password { get; set; }

        [Required(ErrorMessage = "Укажите email адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string email { get; set; }
        public System.DateTime registerDate { get; set; }
        public Nullable<System.DateTime> lastLoginDate { get; set; }
        public string status { get; set; }
    }
}