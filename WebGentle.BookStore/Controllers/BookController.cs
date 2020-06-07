using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebGentle.BookStore.Models;
using WebGentle.BookStore.Repository;

namespace WebGentle.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ViewResult> GetAllBooks(bool isDeleted = false, bool IsUpdated = false, string bookName = "", int bookId = 0)
        {
            ViewBag.isDeleted = isDeleted;
            ViewBag.IsUpdated = IsUpdated;
            ViewBag.bookName = bookName;
            ViewBag.bookId = bookId;
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        public async Task<ViewResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);

            return View(data);
        }

        public async Task<ViewResult> UpdateBook(int id)
        {
            var data = await _bookRepository.UpdateBook_Get(id);
            var list = new SelectList(new List<string>() { "Urdu", "Punjabi", "English" });
            ViewBag.language = list;
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBook(BookModel bookModel)
        {
            var data = await _bookRepository.UpdateBook_Post(bookModel);

            if (data != null)
            {
                return RedirectToAction(nameof(GetAllBooks), new { isDeleted = false, IsUpdated = true, bookName = "", bookId = data.Id });
            }

            return View(data);
        }

        public async Task<ActionResult> DeleteBook(int id)
        {
            var data = await _bookRepository.DeleteBook(id);
            if(data != null)
            {
                return RedirectToAction(nameof(GetAllBooks), new { isDeleted = true, IsUpdated = false, bookName = data.Description , bookId = data.Id });
            }

            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            // == For paasing value into dropdown
            var model = new BookModel()
            {
                Language = "English"
            };

            // == Creating a list and pass it from controller
            //var list = new List<string>() { "Urdu", "Punjabi", "English" };
            //ViewBag.language = list;

            // == Creating Select List and pass it direct from controller 
            var list = new SelectList(new List<string>() { "Urdu", "Punjabi", "English" });
            ViewBag.language = list;

            //== Creating Groups for SlectListItems
            //var group1 = new SelectListGroup() { Name = "Group1" };
            //var group2 = new SelectListGroup() { Name = "Group2" };
            //var group3 = new SelectListGroup() { Name = "Group3" };

            // == Creating SelectListGroup and pass it from controller
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text = "hindi", Value = "1" , Group = group1},
            //    new SelectListItem(){ Text = "English", Value = "2" , Group = group1},
            //    new SelectListItem(){ Text = "Urdu", Value = "3" , Group = group2},
            //    new SelectListItem(){ Text = "Tamil", Value = "4" , Group = group2},
            //    new SelectListItem(){ Text = "Punjabi", Value = "5" , Group = group3},
            //    new SelectListItem(){ Text = "Chinese", Value = "6" , Group = group3},
            //};

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            // == Creating a list and pass it from controller
            //ViewBag.language = new List<string>() { "Urdu", "Punjabi", "English" };

            // == Creating Select List and pass it direct from controller 
            ViewBag.language = new SelectList(new List<string>() { "Urdu", "Punjabi", "English" });

            //== Creating Groups for SlectListItems
            //var group1 = new SelectListGroup() { Name = "Group1" };
            //var group2 = new SelectListGroup() { Name = "Group2" };
            //var group3 = new SelectListGroup() { Name = "Group3" };

            // == Creating SelectListGroup and pass it from controller
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new SelectListItem(){ Text = "hindi", Value = "1" , Group = group1},
            //    new SelectListItem(){ Text = "English", Value = "2" , Group = group1},
            //    new SelectListItem(){ Text = "Urdu", Value = "3" , Group = group2},
            //    new SelectListItem(){ Text = "Tamil", Value = "4" , Group = group2},
            //    new SelectListItem(){ Text = "Punjabi", Value = "5" , Group = group3},
            //    new SelectListItem(){ Text = "Chinese", Value = "6" , Group = group3},
            //};

            //ModelState.AddModelError("", "This is the custom error message");
            //ModelState.AddModelError("", "This is the second custom error message");

            return View();
        }
    }
}