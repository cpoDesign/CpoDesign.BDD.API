using System;
using System.Net.Http.Headers;

namespace CpoDesign.BDD.API.CpoDesign
{
    public class WebRequestResult
    {
        public bool Executed { get; set; }
        public string Content { get; set; }
        
        public Exception Exception { get; set; }
        public HttpResponseHeaders Response { get; internal set; }
    }
}
