using System;
using System.Collections.Generic;

namespace SmartHouseSolutionsAPI.Models;

public partial class Device
{
    public int DeviceId { get; set; }

    public string DeviceName { get; set; } = null!;

    public int DeviceTypeId { get; set; }

    public virtual ICollection<CustomerDeviceRelation> CustomerDeviceRelations { get; set; } = new List<CustomerDeviceRelation>();

    public virtual DeviceType DeviceType { get; set; } = null!;
}
