namespace TestProject2;

public class DeviceManagerTests
    {
        private const string TestFilePath = "test_devices.txt";

        [Fact]
        public void ReadDevices_ShouldLoadCorrectNumberOfDevices()
        {
            // Arrange
            File.WriteAllLines(TestFilePath, new[]
            {
                "SW-1,Apple Watch SE2,true,50%",
                "P-2,LinuxPC,false,Linux Mint"
            });

            var deviceManager = new DeviceManager(TestFilePath);

            // Act & Assert
            Assert.Equal(2, deviceManager.Devices.Count);
        }

        [Fact]
        public void AddDevice_ShouldIncreaseDeviceCount()
        {
            // Arrange
            var deviceManager = new DeviceManager(TestFilePath);
            var newDevice = new Smartwatch(3, "Fitbit Versa", 80);

            // Act
            deviceManager.add(newDevice);

            // Assert
            Assert.Contains(newDevice, deviceManager.Devices);
            Assert.Equal(1, deviceManager.Devices.Count); // No devices were loaded initially
        }

        [Fact]
        public void RemoveDevice_ShouldDecreaseDeviceCount()
        {
            // Arrange
            File.WriteAllLines(TestFilePath, new[] { "SW-1,Apple Watch SE2,true,50%" });
            var deviceManager = new DeviceManager(TestFilePath);

            // Act
            deviceManager.RemoveDevice(1);

            // Assert
            Assert.Empty(deviceManager.Devices);
        }

        [Fact]
        public void EditDevice_ShouldModifyCorrectly()
        {
            // Arrange
            File.WriteAllLines(TestFilePath, new[] { "SW-1,Apple Watch SE2,true,50%" });
            var deviceManager = new DeviceManager(TestFilePath);

            // Act
            deviceManager.edit(1, 90); // Change battery level

            // Assert
            var device = deviceManager.Devices.First() as Smartwatch;
            Assert.NotNull(device);
            Assert.Equal(90, device.battery);
        }

        [Fact]
        public void TurnOnDevice_ShouldChangeState()
        {
            // Arrange
            File.WriteAllLines(TestFilePath, new[] { "P-2,LinuxPC,false,Linux Mint" });
            var deviceManager = new DeviceManager(TestFilePath);

            // Act
            deviceManager.turnOn(2);

            // Assert
            var device = deviceManager.Devices.First();
            Assert.True(device.IsTurnedOn);
        }
    }
}