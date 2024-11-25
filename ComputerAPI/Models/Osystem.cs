using System;
using System.Collections.Generic;

namespace ComputerAPI.Models;

public partial class Osystem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Comp> Comps { get; set; } = new List<Comp>();
}
