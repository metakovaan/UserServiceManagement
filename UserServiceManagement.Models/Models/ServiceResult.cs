using System.ComponentModel;

namespace UserServiceManagement.Models.Models
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public Error Error { get; set; }

        public ServiceResult()
        {
            IsSuccess = false;
        }

        public ServiceResult(bool isSuccess, string message, object data, Error error)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            Error = error;
        }
    }
}
