using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_Project.Models;

public partial class Disease
{
    [Key]
    public int Id { get; set; }

    public int InternationalCode { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MedicinesForDisease> MedicinesForDiseases { get; set; } = new List<MedicinesForDisease>();
}
