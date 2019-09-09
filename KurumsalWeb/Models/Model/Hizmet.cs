using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWeb.Models.Model
{
    [Table("Hizmet")]
    public class Hizmet
    {
        [Key]
        public int HizmetId { get; set; }

        [Required,StringLength(150,ErrorMessage ="150 simvoldan ibaret olmalidir")]
        [DisplayName("Hizmet Baslik")]
        public string Baslik { get; set; }

        [DisplayName("Hizmet Aciklama")]
        [StringLength(400,ErrorMessage ="400 simvoldan ibaret olmalidir")]
        public string Aciklama { get; set; }

        public string ResimURL { get; set; }











    }
}