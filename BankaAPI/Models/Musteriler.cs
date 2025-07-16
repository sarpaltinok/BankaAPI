using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankaAPI.Models;

public partial class Musteri
{
    [Key]
    public int MusteriNo { get; set; }

    public string? Ad { get; set; }

    public string? Soyad { get; set; }

    public string? Telefon { get; set; }

    public string? Sube { get; set; }

    public int? KrediNotu { get; set; }

    public string? Cinsiyet { get; set; }

    public DateOnly? DogumTarihi { get; set; }

    public DateOnly? KayitTarihi { get; set; }

    public decimal? KrediTutari { get; set; }

    public virtual ICollection<OdemeLog> OdemeLogs { get; set; } = new List<OdemeLog>();

    public virtual ICollection<Odemeler> Odemelers { get; set; } = new List<Odemeler>();
}
