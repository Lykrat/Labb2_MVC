using static Labb_2_MVC.StaticDetails;

namespace Labb_2_MVC.Models
{
	public class ApiRequest
	{
		public ApiType ApiType { get; set; }
		public string Url { get;set; }
		public object Data { get; set; }
		public string AccessToken { get; set; }
	}
}
