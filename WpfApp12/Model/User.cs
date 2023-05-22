using System;
using System.Collections.Generic;

namespace WpfApp12.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int RoleId { get; set; }

    public int CorzinaId { get; set; }

    public virtual Corzina Corzina { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
