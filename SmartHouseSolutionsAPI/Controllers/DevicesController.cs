using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartHouseSolutionsAPI.Models;

namespace SmartHouseSolutionsAPI.Controllers
{
    public class DevicesController : ControllerBase
    {
        private readonly SmartHouseSolutionsDbContext _context;

        public DevicesController(SmartHouseSolutionsDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllDevices")]
        public async Task<ActionResult> GetAllDevices()
        {
            var devices = await _context.Devices
                .Include(d => d.CustomerDeviceRelations)
                .ThenInclude(c => c.Customer)
                .Select(x => new
                {
                    DeviceId = x.DeviceId,
                    DeviceName = x.DeviceName,
                    DeviceType = x.DeviceType.TypeName,
                    CustomerId = x.CustomerDeviceRelations.FirstOrDefault().CustomerId,
                    CustomerName = x.CustomerDeviceRelations.FirstOrDefault().Customer.CustomerName,
                    CustomerEmail = x.CustomerDeviceRelations.FirstOrDefault().Customer.CustomerEmail

                })
                .ToListAsync();

            return Ok(devices);
        }

        [HttpGet("GetDeviceById/{deviceId}")]
        public async Task<ActionResult> GetDeviceById(int deviceId)
        {
            var device = await _context.Devices
                .Include(d => d.CustomerDeviceRelations)
                .ThenInclude(c => c.Customer)
                .Where(x => x.DeviceId == deviceId)
                .Select(x => new
                {
                    DeviceId = x.DeviceId,
                    DeviceName = x.DeviceName,
                    DeviceType = x.DeviceType.TypeName,
                    CustomerId = x.CustomerDeviceRelations.FirstOrDefault().CustomerId,
                    CustomerName = x.CustomerDeviceRelations.FirstOrDefault().Customer.CustomerName,
                    CustomerEmail = x.CustomerDeviceRelations.FirstOrDefault().Customer.CustomerEmail

                }).FirstOrDefaultAsync();


            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        [HttpGet("GetDevicesByType/{typeName}")]
        public async Task<ActionResult> GetDevicesByType(string typeName)
        {
            var devices = await _context.Devices
                .Include(d => d.CustomerDeviceRelations)
                .ThenInclude(c => c.Customer)
                .Where(d => d.DeviceType.TypeName == typeName)
                .Select(x => new
                {
                    DeviceId = x.DeviceId,
                    DeviceName = x.DeviceName,
                    DeviceType = x.DeviceType.TypeName,
                    CustomerId = x.CustomerDeviceRelations.FirstOrDefault().CustomerId,
                    CustomerName = x.CustomerDeviceRelations.FirstOrDefault().Customer.CustomerName,
                    CustomerEmail = x.CustomerDeviceRelations.FirstOrDefault().Customer.CustomerEmail

                })
                .ToListAsync();

            if (devices.Count == 0)
            {
                return NotFound("No devices found for the given type.");
            }

            return Ok(devices);
        }

        [HttpGet("GetDevicesByCustomerId/{customerId}")]
        public async Task<ActionResult> GetDevicesByCustomerId(int customerId)
        {
            var devices = await _context.CustomerDeviceRelations
                .Include(cdr => cdr.Device)
                .Where(cdr => cdr.CustomerId == customerId)
                .Select(cdr => new
                {
                    CustomerId = cdr.CustomerId,    
                    CustomerName = cdr.Customer.CustomerName,
                    CustomerEmail = cdr.Customer.CustomerEmail,
                    Devices = cdr.Customer.CustomerDeviceRelations.Select(x => new {
                        DeviceId = x.DeviceId,
                        DeviceName = x.Device.DeviceName,
                        DeviceType = x.Device.DeviceType.TypeName
                    })
                    
                })
                .FirstOrDefaultAsync();

            if (devices == null)
            {
                return NotFound("No devices found for the given customer.");
            }

            return Ok(devices);
        }

    }
}
