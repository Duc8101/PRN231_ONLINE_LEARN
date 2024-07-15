using System.Net;

namespace Common.Base
{
    public class ResponseBase<T>
    {
        public int Code { get; }
        public string Message { get; } = string.Empty;
        public T? Data { get; }

        public ResponseBase()
        {

        }

        public ResponseBase(T data, string message, int code)
        {
            Data = data;
            Message = message;
            Code = code;
        }

        public ResponseBase(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public ResponseBase(T data, string message)
        {
            Data = data;
            Message = message;
            Code = (int)HttpStatusCode.OK;
        }

        public ResponseBase(T data)
        {
            Data = data;
            Code = (int)HttpStatusCode.OK;
        }
    }
}
