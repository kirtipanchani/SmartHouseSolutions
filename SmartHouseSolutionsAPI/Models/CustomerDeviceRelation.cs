using System;
using System.Collections.Generic;

namespace SmartHouseSolutionsAPI.Models;

public partial class CustomerDeviceRelation
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int DeviceId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;
}
