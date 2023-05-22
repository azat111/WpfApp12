using System;
using System.Collections.Generic;

namespace WpfApp12.Model;

public partial class CorzinaOfTovari
{
    public int IdCorzina { get; set; }

    public int IdTovari { get; set; }

    public int? Quntity { get; set; }

    public int? Price { get; set; }

    public virtual Corzina IdCorzinaNavigation { get; set; } = null!;

    public virtual Tovari IdTovariNavigation { get; set; } = null!;
}
