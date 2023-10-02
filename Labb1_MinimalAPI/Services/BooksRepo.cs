using Labb2_CallAPI_ASP.Data;
using Microsoft.EntityFrameworkCore;

namespace Labb2_CallAPI_ASP.Services
{
    public class BooksRepo : IBooksRepo
	{
		private DataContext _dbContext;

		public BooksRepo(DataContext dbContext)
		{
			this._dbContext = dbContext;
		}
		public async Task<Books> CreateBookAsync(Books book)
		{
			await _dbContext.Books.AddAsync(book);
			await _dbContext.SaveChangesAsync();
			return book;
		}

		public async Task<Books> DeleteBookAsync(int id)
		{
            var bookToDelete = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (bookToDelete == null)
            {
                return null;
            }
            _dbContext.Books.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();
            return bookToDelete;
        }

		public async Task<IEnumerable<Books>> GetAllAsync()
		{
			return await _dbContext.Books.ToListAsync();
		}

		public async Task<Books> GetBookAsync(int id)
		{
			return await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<Books> UpdateBook(Books book)
		{
			var newBook=await _dbContext.Books.FindAsync(book.Id);
			if(newBook != null)
			{
				newBook.Title = book.Title;
				newBook.Author = book.Author;
				newBook.Year = book.Year;
				newBook.Description = book.Description;
				newBook.InStock = book.InStock;
				await _dbContext.SaveChangesAsync();
				return newBook;
			}
			return null;
		}
	}
}
