namespace Labb_2_MVC.Models
{
	public class ResponseDTO
	{
		public bool Success { get; set; } = true;
		public object Result { get;set; }
		public string Displaymessage { get; set; } = "";
		public List<string> Error { get; set; }
	}
}
