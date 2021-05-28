using System.Collections.Generic;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints.Extensions;

namespace ServiceStack.WebHost.Endpoints.Support
{
    public class ForbiddenHttpHandler
        : IServiceStackHttpHandler, IHttpHandler
    {
		public bool? IsIntegratedPipeline { get; set; }
		public string WebHostPhysicalPath { get; set; }
		public List<string> WebHostRootFileNames { get; set; }
		public string ApplicationBaseUrl { get; set; }
		public string DefaultRootFileName { get; set; }
		public string DefaultHandler { get; set; }

        public void ProcessRequest(IHttpRequest request, IHttpResponse response, string operationName)
        {
            response.ContentType = "text/plain";
            response.StatusCode = 403;

		    response.EndHttpHandlerRequest(skipClose: true, afterBody: r => {
                r.Write("Forbidden\n\n");

            });
		}

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            response.ContentType = "text/plain";
            response.StatusCode = 403;

            response.EndHttpHandlerRequest(skipClose:true, afterBody: r=> {
                r.Write("Forbidden\n\n");
            });
		}

        public bool IsReusable
        {
            get { return true; }
        }
    }
}