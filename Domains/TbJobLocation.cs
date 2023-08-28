using System;
using System.Collections.Generic;

namespace Domains;

public partial class TbJobLocation
{
    public int JobLocationId { get; set; }

    public string JobLocationName { get; set; } = null!;

    public int CurrentState { get; set; }

    public virtual ICollection<TbJob> TbJobs { get; set; } = new List<TbJob>();
}
