using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WpfApp_NTP_ders
{
    [Table("Sinif")]
    public partial class Sinif
    {
        public Sinif()
        {
            Ogrencis = new HashSet<Ogrenci>();
        }

        [Key]
        public int Id { get; set; }
        [Column("seviye")]
        public int Seviye { get; set; }
        [Column("sube")]
        [StringLength(3)]
        [Unicode(false)]
        public string Sube { get; set; } = null!;
        [Column("okul")]
        public int Okul { get; set; }

        //SINIF SUBE BİLGİSİNİ GÖSTERMEK İÇİN
        public string SinifAd { get{ return Seviye + "/" + Sube; } }

        [ForeignKey(nameof(Okul))]
        [InverseProperty("Sinifs")]
        public virtual Okul OkulNavigation { get; set; } = null!;
        [InverseProperty(nameof(Ogrenci.SinifNavigation))]
        public virtual ICollection<Ogrenci> Ogrencis { get; set; }
    }
}
