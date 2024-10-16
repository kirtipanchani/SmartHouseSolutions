using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartHouseSolutionsAPI.Controllers;
using SmartHouseSolutionsAPI.Models;



namespace SmartHouseSolutionsAPITest
{
    public class DevicesControllerTest
    {
        private readonly SmartHouseSolutionsDbContext _context;
        private readonly DevicesController _controller;

        public DevicesControllerTest()
        {
            // Set up the InMemory database
            var options = new DbContextOptionsBuilder<SmartHouseSolutionsDbContext>()
                .UseInMemoryDatabase(databaseName: "SmartHouseSolutionsTestDb")
                .Options;
            _context = new SmartHouseSolutionsDbContext(options);

            SeedDatabase();

            _controller = new DevicesController(_context);
        }

        private void SeedDatabase()
        {
            var customer1 = new Customer { CustomerId = 1, CustomerName = "Rahul", CustomerEmail = "rahul@example.com" };
            var customer2 = new Customer { CustomerId = 2, CustomerName = "Priya", CustomerEmail = "priya@example.com" };

            var deviceTypeCamera = new DeviceType { TypeName = "Camera" };
            var deviceTypeAlarm = new DeviceType { TypeName = "Alarm" };

            var device1 = new Device { DeviceId = 1, DeviceName = "Outdoor Camera", DeviceType = deviceTypeCamera };
            var device2 = new Device { DeviceId = 2, DeviceName = "Indoor Alarm", DeviceType = deviceTypeAlarm };

            var customerDeviceRelation1 = new CustomerDeviceRelation { CustomerId = 1, DeviceId = 1, Customer = customer1, Device = device1 };
            var customerDeviceRelation2 = new CustomerDeviceRelation { CustomerId = 2, DeviceId = 2, Customer = customer2, Device = device2 };

            _context.Customers.AddRange(customer1, customer2);
            _context.DeviceTypes.AddRange(deviceTypeCamera, deviceTypeAlarm);
            _context.Devices.AddRange(device1, device2);
            _context.CustomerDeviceRelations.AddRange(customerDeviceRelation1, customerDeviceRelation2);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetAllDevices_ReturnsAllDevices()
        {
            
            var result = await _controller.GetAllDevices();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var devices = Assert.IsAssignableFrom<IEnumerable<dynamic>>(okResult.Value);
            Assert.Equal(2, devices.Count());
        }

        [Fact]
        public async Task GetDeviceById_ReturnsDevice_WhenDeviceExists()
        {
            
            var result = await _controller.GetDeviceById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var device = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("Outdoor Camera", device.DeviceName);
        }

        [Fact]
        public async Task GetDeviceById_ReturnsNotFound_WhenDeviceDoesNotExist()
        {
            var result = await _controller.GetDeviceById(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetDevicesByType_ReturnsDevices_WhenTypeExists()
        {
            var result = await _controller.GetDevicesByType("Camera");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var devices = Assert.IsAssignableFrom<IEnumerable<dynamic>>(okResult.Value);
            Assert.Single(devices);  
        }

        [Fact]
        public async Task GetDevicesByType_ReturnsNotFound_WhenTypeDoesNotExist()
        {
            // Act
            var result = await _controller.GetDevicesByType("NonExistentType");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetDevicesByCustomerId_ReturnsDevicesForCustomer_WhenCustomerExists()
        {
            // Act
            var result = await _controller.GetDevicesByCustomerId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customerDevices = Assert.IsType<dynamic>(okResult.Value);
            Assert.Equal("Rahul", customerDevices.CustomerName);
            Assert.Single(customerDevices.Devices);
        }

        [Fact]
        public async Task GetDevicesByCustomerId_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Act
            var result = await _controller.GetDevicesByCustomerId(99);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
