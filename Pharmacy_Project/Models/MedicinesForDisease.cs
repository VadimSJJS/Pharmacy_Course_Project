using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_Project.Models;

public partial class MedicinesForDisease
{
    [Key]
    public int Id { get; set; }

    public int MidicineId { get; set; }

    public int DiseaseId { get; set; }

    public virtual Disease Disease { get; set; } = null!;

    public virtual Medicine Midicine { get; set; } = null!;
}
