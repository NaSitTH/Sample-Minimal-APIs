namespace ContosoUniversityApi.Wrapper
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool IsError { get; set; } = false;
        public string[] Errors { get; set; } = new string[] { };

        public Response()
        {
        }

        public Response(T data)
        {
            this.Data = data;
        }

        public Response(bool isError, string[] errors)
        {
            this.IsError = isError;
            this.Errors = errors;
        }

        public Response(T data, bool isError, string[] errors)
        {
            this.Data = data;
            this.IsError = isError;
            this.Errors = errors;
        }
    }
}