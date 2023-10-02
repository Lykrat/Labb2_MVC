using Labb2_CallAPI_ASP.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace Labb_2_MVC.Services
{
	public class BookService : BaseService, IBookService
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public BookService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<T> AddBookAsync<T>(Books books)
		{
			return await this.SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.POST,
				Data = books,
				Url = StaticDetails.BookAPIBase + "/api/book",
				AccessToken = ""
			});
		}

		public async Task<T> DeleteBookAsync<T>(int id)
		{
			return await this.SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.DELETE,
				Url = StaticDetails.BookAPIBase + "/api/books/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> GetAllBooks<T>()
		{
			return await this.SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.GET,
				Url = StaticDetails.BookAPIBase + "/api/books",
				AccessToken = ""
			});
		}

		public async Task<T> GetBookById<T>(int id)
		{
			return await this.SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.GET,
				Url = StaticDetails.BookAPIBase + "/api/books/" + id,
				AccessToken = ""
			});
		}

		public async Task<T> UpdateBookAsync<T>(Books books)
		{
			return await this.SendAsync<T>(new Models.ApiRequest
			{
				ApiType = StaticDetails.ApiType.PUT,
				Data = books,
				Url = StaticDetails.BookAPIBase + "/api/books/" + books.Id,
				AccessToken = ""
			});
		}
	}
}
