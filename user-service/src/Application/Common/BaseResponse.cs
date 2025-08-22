using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class BaseResponse
    {
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; } = "Success";
    }
    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
