using System;
using System.Collections.Generic;

namespace SmartHouseSolutionsAPI.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public virtual ICollection<CustomerDeviceRelation> CustomerDeviceRelations { get; set; } = new List<CustomerDeviceRelation>();
}
