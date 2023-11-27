using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_Project.Models;

public partial class Incoming
{
    [Key]
    public int Id { get; set; }

    public int MedicineId { get; set; }

    public DateTime ArrivalDate { get; set; }

    public int Count { get; set; }

    public string Provider { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Medicine? Medicine { get; set; }
}
