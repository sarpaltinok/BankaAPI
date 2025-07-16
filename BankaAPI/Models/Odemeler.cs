﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankaAPI.Models;

public partial class Odemeler
{
    [Key]
    public int OdemeId { get; set; }

    public int? MusteriNo { get; set; }

    public decimal? GuncelOdemeTutari { get; set; }

    public decimal? GuncelBorcTutari { get; set; }

    public DateOnly? SonOdemeTarihi { get; set; }

    public decimal? GecikmisBorcTutari { get; set; }

    public decimal? OdenmisBorcTutari { get; set; }

    public virtual Musteriler? MusteriNoNavigation { get; set; }
}
