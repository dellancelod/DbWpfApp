using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DbWpfApp.Models
{
    internal class AppItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Назва застосунку")]
        public string AppName { get; set; }
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }
        [Display(Name = "Коментар")]
        public string Comment { get; set; }
    }
}
