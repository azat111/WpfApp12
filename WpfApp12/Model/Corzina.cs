using System;
using System.Collections.Generic;

namespace WpfApp12.Model;

public partial class Corzina
{
    public int Id { get; set; }

    public virtual ICollection<CorzinaOfTovari> CorzinaOfTovaris { get; set; } = new List<CorzinaOfTovari>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
