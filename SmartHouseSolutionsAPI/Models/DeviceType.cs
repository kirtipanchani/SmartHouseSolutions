﻿using System;
using System.Collections.Generic;

namespace SmartHouseSolutionsAPI.Models;

public partial class DeviceType
{
    public int DeviceTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
