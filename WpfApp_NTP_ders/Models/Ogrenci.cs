using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WpfApp_NTP_ders
{
    [Table("Ogrenci")]
    public partial class Ogrenci
    {
        [Key]
        public int Id { get; set; }
        [Column("okulno")]
        public int? Okulno { get; set; }
        [Column("ad")]
        [StringLength(50)]
        [Unicode(false)]
        public string Ad { get; set; } = null!;
        [Column("soyad")]
        [StringLength(50)]
        [Unicode(false)]
        public string Soyad { get; set; } = null!;
        [Column("sinif")]
        public int? Sinif { get; set; }

        [ForeignKey(nameof(Sinif))]
        [InverseProperty("Ogrencis")]
        public virtual Sinif? SinifNavigation { get; set; }
    }
}
