using Labb_2_MVC.Models;
using Labb_2_MVC.Services;
using Labb2_CallAPI_ASP.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Labb_2_MVC.Controllers
{
	public class BooksController : Controller
	{
		private readonly IBookService _bookService;
		public BooksController(IBookService bookService)
		{
			this._bookService = bookService;
		}
		public async Task<IActionResult> BookIndex()
		{
			List<Books> list=new List<Books>();
			var response = await _bookService.GetAllBooks<ResponseDTO>();
			if(response != null && response.Success)
			{
				list=JsonConvert.DeserializeObject<List<Books>>(Convert.ToString(response.Result));
			}
			return View(list);
		}
		public async Task<IActionResult> Details(int id)
		{
			var response = await _bookService.GetBookById<ResponseDTO>(id);
			if(response != null && response.Success)
			{
				Books model = JsonConvert.DeserializeObject<Books>(Convert.ToString(response.Result));
				return View(model);
			}
			return NotFound();
		}
		public async Task<IActionResult> AddBook()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult>AddBook(Books book)
		{
			if (ModelState.IsValid)
			{
				var response=await _bookService.AddBookAsync<ResponseDTO>(book);
				if(response !=null && response.Success)
				{
					return RedirectToAction(nameof(BookIndex));
				}
			}
			return View(book);
		}
		public async Task<IActionResult> UpdateBook(int id)
		{
			var response = await _bookService.GetBookById<ResponseDTO>(id);
			if(response!=null && response.Success)
			{
				Books book = JsonConvert.DeserializeObject<Books>(Convert.ToString(response.Result));
				return View(book);
			}
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> UpdateBook(Books book)
		{
			if (ModelState.IsValid)
			{
				var response= await _bookService.UpdateBookAsync<ResponseDTO>(book);
				if(response!=null && response.Success)
				{
                    return RedirectToAction(nameof(BookIndex));
                }
			}
			return View(book);
		}
        public async Task<IActionResult> DeleteBook(int id)
        {

            var response = await _bookService.GetBookById<ResponseDTO>(id);

            if (response != null && response.Success)
            {
                Books book = JsonConvert.DeserializeObject<Books>(Convert.ToString(response.Result));
                return View(book);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(Books book)
        {
            var response = await _bookService.DeleteBookAsync<ResponseDTO>(book.Id);
			if (response != null && response.Success)
			{
				return RedirectToAction(nameof(BookIndex));
			}
            else
            {
                return NotFound();
            }
            //if (ModelState.IsValid)
            //{
            //    var response = await _bookService.DeleteBookAsync<ResponseDTO>(book.Id);
            //    if (response != null && response.Success)
            //    {
            //        return RedirectToAction(nameof(BookIndex));
            //    }
            //}
        }
    }
}
