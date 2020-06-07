using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle.BookStore.Data;
using WebGentle.BookStore.Models;

namespace WebGentle.BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Description = model.Description,
                Title = model.Title,
                Language = model.Language,
                Category = model.Category,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if(allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        Language= book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if(book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                return bookDetails;
            }
            return null;
        }


        public async Task<BookModel> UpdateBook_Get(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                return bookDetails;
            }
            return null;
        }

        public async Task<Books> UpdateBook_Post(BookModel bookModel)
        {
            if(bookModel != null)
            {
                var bookDetails = await _context.Books.Where(x=>x.Id == bookModel.Id).FirstAsync();
                if(bookDetails != null)
                {
                    bookDetails.Id = bookModel.Id;
                    bookDetails.Author = bookModel.Author;
                    bookDetails.Description = bookModel.Description;
                    bookDetails.Category = bookModel.Category;
                    bookDetails.Title = bookModel.Title;
                    bookDetails.Language = bookModel.Language;
                    bookDetails.TotalPages = bookModel.TotalPages ?? 0;
                    bookDetails.UpdatedOn = DateTime.UtcNow;

                    await _context.SaveChangesAsync();
                }
                return bookDetails;
            }
            return null;
        }

        public async Task<BookModel> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return bookDetails;
            }
            return null;
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) || x.Author.Contains(authorName)).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>
            {
                new BookModel(){Id=1, Title="MVC", Author="Nouman Butt", Description="This is the description for MVC book", Category="Programming", Language="English", TotalPages=134},
                new BookModel(){Id=2, Title="C#", Author="Nouman M B", Description="This is the description for C# book", Category="Framework", Language="chinese", TotalPages=250},
                new BookModel(){Id=3, Title="JAVA", Author="Nouman M", Description="This is the description for JAVA book", Category="Developer", Language="Urdu", TotalPages=800},
                new BookModel(){Id=4, Title="Php", Author="Nouman B", Description="This is the description for Php book", Category="Programming", Language="English", TotalPages=600},
                new BookModel(){Id=5, Title="Angular",Author="Nouman Butt", Description="This is the description for Angular book", Category="Programming", Language="English", TotalPages=134},
                new BookModel(){Id=6, Title="React",Author="Nouman", Description="This is the description for React book", Category="Programming", Language="Japanese", TotalPages=456},
            };
        }
    }
}
