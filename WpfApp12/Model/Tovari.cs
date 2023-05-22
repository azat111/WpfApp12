using System;
using System.Collections.Generic;

namespace WpfApp12.Model;

public partial class Tovari
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Quantity { get; set; }

    public int? Price { get; set; }

	public string? Photo { get; set; }

	public virtual ICollection<CorzinaOfTovari> CorzinaOfTovaris { get; set; } = new List<CorzinaOfTovari>();

    public string? phot => "/Resources/" + Photo + ".jpg";
}
