using Labb2_CallAPI_ASP.Data;
using Labb2_CallAPI_ASP.Services;
using Microsoft.EntityFrameworkCore;

namespace Labb2_CallAPI_ASP
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddAuthorization();

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<DataContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
			builder.Services.AddScoped<IBooksRepo, BooksRepo>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapPost("/api/book", async (IBooksRepo bookRepo, Books book) =>
			{
				var result = await bookRepo.CreateBookAsync(book);
				return Results.Ok(result);
			}).WithName("AddBook").Accepts<Books>("application/json").Produces(201).Produces(400);

			app.MapGet("/api/books",async (IBooksRepo bookRepo) =>
			{
				APIResponse response = new APIResponse
				{
					Result = await bookRepo.GetAllAsync(),
					Success = true,
					StatusCode = System.Net.HttpStatusCode.OK
				};
				return Results.Ok(response);
				//var result=await bookRepo.GetAllAsync();
				//return Results.Ok(result);
			}).WithName("GetAllBooks").Produces(200);

			app.MapGet("/api/books/{id:int}", async (IBooksRepo bookRepo,int id) =>
			{
				//var result = await bookRepo.GetBookAsync(id);
				//if(result==null)
				//{
				//	return Results.BadRequest(result);
				//}
				//return Results.Ok(result);
				APIResponse response = new APIResponse();
				var book=await bookRepo.GetBookAsync(id);

				if (book != null)
				{
					response.Result = book;
					response.Success = true;
					response.StatusCode= System.Net.HttpStatusCode.OK;
					return Results.Ok(response);
				};
				return Results.NotFound("Could not find book");
			}).WithName("GetBookById").Produces(200).Produces(400);

			app.MapPut("/api/books/{id:int}", async (IBooksRepo bookRepo,Books book) =>
			{
				var result = await bookRepo.UpdateBook(book);
				if (result == null)
				{
					return Results.BadRequest(result);
				}
				return Results.Ok(result);
				//APIResponse response = new APIResponse
				//{
				//	Success = false,
				//	StatusCode = System.Net.HttpStatusCode.BadRequest
				//};
				//var bookUpdate = await bookRepo.UpdateBook(book);
				//if (bookUpdate != null)
				//{
				//	response.Result = bookUpdate;
				//	response.Success = true;
				//	response.StatusCode = System.Net.HttpStatusCode.OK;
				//	return Results.Ok();
				//}
				//else
				//{
				//	return Results.NotFound("Book was not found");
				//}
			}).WithName("UpdateBook").Accepts<Books>("application/json").Produces<APIResponse>(200).Produces(400);

			app.MapDelete("/api/books/{id:int}", async (IBooksRepo bookRepo, int id) =>
			{
				var result = await bookRepo.DeleteBookAsync(id);
				if (result == null)
				{
					return Results.BadRequest(result);
				}
				return Results.Ok(result);
				//APIResponse response = new APIResponse() { Success = false, StatusCode = System.Net.HttpStatusCode.BadRequest };

				//var bookToDelete = await bookRepo.DeleteBookAsync(id);
				//if (bookToDelete != null)
				//{
				//    response.Result = bookToDelete;
				//    response.Success = true;
				//    response.StatusCode = System.Net.HttpStatusCode.NoContent;
				//    return Results.Ok(response);
				//}
				//return Results.NotFound("Unable to find the book");
			}).WithName("DeleteBook").Produces<APIResponse>(200).Produces(400);

			app.Run();
		}
	}
}