using UserServiceManagement.Contracts.Configurations;

namespace UserServiceManagement.Data.Configurations
{
    public class CloudinarySettings : ICloudinarySettings
    {
        public string CloudName { get ; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }
}
