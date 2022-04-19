using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WpfApp_NTP_ders
{
    [Table("Okul")]
    public partial class Okul
    {
        public Okul()
        {
            Sinifs = new HashSet<Sinif>();
        }

        [Key]
        public int Id { get; set; }
        [Column("kisa_ad")]
        [StringLength(20)]
        [Unicode(false)]
        public string? KisaAd { get; set; }
        [Column("tam_ad")]
        [StringLength(50)]
        [Unicode(false)]
        public string? TamAd { get; set; }
        [Column("mevcut")]
        public int? Mevcut { get; set; }

        [InverseProperty(nameof(Sinif.OkulNavigation))]
        public virtual ICollection<Sinif> Sinifs { get; set; }
    }
}
