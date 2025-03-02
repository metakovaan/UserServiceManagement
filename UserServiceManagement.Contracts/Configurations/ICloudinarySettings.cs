namespace UserServiceManagement.Contracts.Configurations
{
    public interface ICloudinarySettings
    {
        string CloudName { get; set; }
        string ApiKey { get; set; }
        string ApiSecret { get; set; }
    }
}
