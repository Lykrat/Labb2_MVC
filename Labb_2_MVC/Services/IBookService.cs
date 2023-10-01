using Labb2_CallAPI_ASP.Data;

namespace Labb_2_MVC.Services
{
	public interface IBookService
	{
		Task<T> GetAllBooks<T>();
		Task<T> GetBookById<T>(int id);
		Task<T> AddBookAsync<T>(Books books);
		Task<T> UpdateBookAsync<T>(Books books);
		Task<T> DeleteBookAsync<T>(int id);
	}
}
