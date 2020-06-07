using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WebGentle.BookStore.Models;

namespace WebGentle.BookStore.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty{ get; set; }

        [ViewData]
        public string Title{ get; set; }

        [ViewData]

        public BookModel Book { get; set; }

        public ViewResult Index()
        {
            //ViewData["property1"] = "Mahmood Butt";

            //ViewData["book"] = new BookModel() { Author = "anc", Id = 3 };

            CustomProperty = "Custom Value";

            Title = "Home page from controller";

            Book = new BookModel() { Id = 4, Title = "abcd", Author = "hhh" };

            return View();
        }

        public ViewResult AboutUs()
        {
            return View();
        }

        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
