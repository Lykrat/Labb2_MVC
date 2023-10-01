using Labb_2_MVC.Models;

namespace Labb_2_MVC.Services
{
	public interface IBaseService : IDisposable
	{
		ResponseDTO responseModel { get; set; }
		Task<T> SendAsync<T>(ApiRequest apiRequest);

	}
}
