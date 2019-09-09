using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Slider")]
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }

        [DisplayName("Slider Başliq"),StringLength(30,ErrorMessage ="30 simvoldan ibarət olmalıdır")]
        public string Baslik { get; set; }

        [DisplayName("Slider Açıklama"), StringLength(150, ErrorMessage = "150 simvoldan ibarət olmalıdır")]
        public string Aciklama { get; set; }

        [DisplayName("Slider Rəsim")]
        public string ResimURL { get; set; }

    }
}