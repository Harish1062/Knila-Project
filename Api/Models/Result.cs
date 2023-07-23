using System.Net;

namespace KnilaProject.Models
{
    public class Result<T>
    {
        public Result()
        {
            StatusCode = (int)HttpStatusCode.OK;
        }

        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

    }
}
