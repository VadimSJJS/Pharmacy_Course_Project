using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_Project.Models;

public partial class Outgoing
{
    [Key]
    public int Id { get; set; }

    public int MedicineId { get; set; }

    public DateTime ImplementationDate { get; set; }

    public int Count { get; set; }

    public decimal SellingPrice { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
