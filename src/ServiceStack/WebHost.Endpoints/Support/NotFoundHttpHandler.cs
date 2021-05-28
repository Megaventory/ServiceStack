using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints.Extensions;
using ServiceStack.Logging;
using HttpRequestWrapper = ServiceStack.WebHost.Endpoints.Extensions.HttpRequestWrapper;
using HttpResponseWrapper = ServiceStack.WebHost.Endpoints.Extensions.HttpResponseWrapper;

namespace ServiceStack.WebHost.Endpoints.Support
{
	public class NotFoundHttpHandler
		: IServiceStackHttpHandler, IHttpHandler
	{
        private static readonly ILog Log = LogManager.GetLogger(typeof(NotFoundHttpHandler));

		public bool? IsIntegratedPipeline { get; set; }
		public string WebHostPhysicalPath { get; set; }
		public List<string> WebHostRootFileNames { get; set; }
		public string ApplicationBaseUrl { get; set; }
		public string DefaultRootFileName { get; set; }
		public string DefaultHandler { get; set; }

		public void ProcessRequest(IHttpRequest request, IHttpResponse response, string operationName)
		{
            Log.ErrorFormat("{0} Request not found: {1}", request.UserHostAddress, request.RawUrl);

		    var text = new StringBuilder();

            if (EndpointHost.DebugMode)
            {
				text.AppendLine("Handler for Request not found.");
            }
            else
            {
                text.Append("404");
            }

		    response.ContentType = "text/plain";
			response.StatusCode = 404;
            response.EndHttpHandlerRequest(skipClose: true, afterBody: r => r.Write(text.ToString()));
		}

		public void ProcessRequest(HttpContext context)
		{
			var request = context.Request;
			var response = context.Response;

			var httpReq = new HttpRequestWrapper("NotFoundHttpHandler", request);
			if (!request.IsLocal)
			{
				ProcessRequest(httpReq, new HttpResponseWrapper(response), null);
				return;
			}

            Log.ErrorFormat("{0} Request not found: {1}", request.UserHostAddress, request.RawUrl);

			var sb = new StringBuilder();
			sb.AppendLine("Handler for Request not found.");

			response.ContentType = "text/plain";
			response.StatusCode = 404;
            response.EndHttpHandlerRequest(skipClose:true, afterBody: r => r.Write(sb.ToString()));
		}

		public bool IsReusable
		{
			get { return true; }
		}
	}
}