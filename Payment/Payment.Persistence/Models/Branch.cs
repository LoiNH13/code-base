using System;
using System.Collections.Generic;

namespace Payment.Persistence.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string BranchName { get; set; } = null!;
}
