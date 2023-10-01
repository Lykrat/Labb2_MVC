using System.Net;

namespace Labb2_CallAPI_ASP.Data
{
	public class APIResponse
	{
		public APIResponse()
		{
			Error=new List<string>();
		}
		public bool Success { get; set; }
		public Object Result { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public List<string> Error { get; set; }
	}
}
