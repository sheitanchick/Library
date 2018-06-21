using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ApiController : Controller
    {
        private LibraryContext _db;

        public ApiController(Library.Models.LibraryContext libraryContext)
        {
            _db = libraryContext;
        }

        [HttpGet("[action]")]
        public JsonResult GetUsers()
        {
            var users = _db.Users.ToList();
            return Json(users);
        }

        [HttpGet("[action]/{id}")]
        public JsonResult GetUser(int id)
        {
            var users = _db.Users.ToList();

            User user = users.Where(o => o.ID == id).FirstOrDefault();
            return Json(new { Name = user.Name, Email = user.Email, ID = user.ID });
        }

        [HttpPost("[action]")]
        public JsonResult EditUser(User user)
        {
            var storeUser = _db.Users.Where(o => o.ID == user.ID).FirstOrDefault();
            List<string> errors = new List<string>();
            if (storeUser != null)
            {

                if (user.Name.Trim() == "" && user.Name == null)
                {
                    errors.Add("Name is empty!");
                }
                if (user.Email == null && user.Email.Trim() == "")
                {
                    errors.Add("Email is empty");
                }

                if (errors.Count > 0)
                {
                    return Json(new { result = "false", errors = errors });
                }

                else
                {
                    if (_db.Users.Where(o => o.Email == user.Email & o.ID != user.ID).FirstOrDefault() == null)
                    {
                        storeUser.Name = user.Name;
                        storeUser.Email = user.Email;

                        _db.SaveChanges();

                        return Json(new { result = "true" });
                    }
                    else
                    {
                        errors.Add("User with this email already exist!");
                        return Json(new { result = "false", errors = errors });
                    }
                }
            }

            else
            {
                return Json(new { result = "false"});
            }
        }

        [HttpGet("[action]/{id}")]
        public JsonResult DeleteUser(int id)
        {
            var storeUser = _db.Users.Where(o => o.ID == id).FirstOrDefault();

            if (storeUser != null)
            {
                _db.Users.Remove(storeUser);
                _db.SaveChanges();

                return Json(new { result = "true" });
            }
            else
            {
                return Json(new { result = "false" });
            }
        }

        [HttpPost("[action]")]
        public JsonResult AddUser(User user)
        {
            List<string> errors = new List<string>();
            User existingUser = _db.Users.Where(o => o.Email == user.Email).FirstOrDefault();

            if (user.Name == null || user.Name.Trim() == "") errors.Add("Type a name, please!");
            if (existingUser != null)
            {
                errors.Add("User with this email already exist!");
                return Json(new { result = "false", errors });
            }
            else
            {
                _db.Users.Add(user);
                _db.SaveChanges();

                return Json(true);
            }
        }

        [HttpGet("[action]")]
        public JsonResult GetBooks()
        {
            return Json(_db.Books.ToList());
        }

        [HttpGet("[action]/{id}")]
        public JsonResult GetBook(int id)
        {
            Book book = _db.Books.Where(o => o.ID == id).FirstOrDefault();
            if (book != null)
            {
                return Json(book);
            }
            else
            {
                return Json(new {result = "false", errors = "Book not found" });
            }
        }

        [HttpGet("[action]/{id}")]
        public JsonResult DeleteBook(int id)
        {
            Book book = _db.Books.Where(o => o.ID == id).FirstOrDefault();
            if (book != null)
            {
                _db.Books.Remove(book);
                _db.SaveChanges();
                return Json(new { result = "true"});
            }
            else
            {
                return Json(new {result = "false", errors = "Book not found" });
            }
        }

        [HttpPost("[action]")]
        public JsonResult AddBook(Book book)
        {
            List<string> errors = new List<string>();
            Book existingBook = _db.Books.Where(o => o.Name == book.Name & o.Author == book.Author).FirstOrDefault();

            if (existingBook != null)
            {
                errors.Add("This book already exist, you can edit it at edting page!");
                return Json(new { result = "false", errors });
            }
            else
            {
                if (book.Name == null || book.Name.Trim() == "") errors.Add("Specify book name, please!");
                if (book.Author == null || book.Author.Trim() == "") errors.Add("You forgot about author :(");
                if (book.AvailableQuantity < 0) errors.Add("Quantity can't be less than 0!");

                if (errors.Count > 0)
                {
                    return Json(new { result = "false", errors });
                }
                else
                {
                    _db.Books.Add(book);
                    _db.SaveChanges();

                    return Json(new { result = "true" });
                }
            }
        }

        [HttpPost("[action]")]
        public JsonResult EditBook(Book book)
        {
            var storeBook = _db.Books.Where(o => o.ID == book.ID).FirstOrDefault();
            List<string> errors = new List<string>();
            if (storeBook != null)
            {

                if (book.Name.Trim() == "" && book.Name == null)
                {
                    errors.Add("Book name is empty!");
                }
                if (book.Author == null && book.Author.Trim() == "")
                {
                    errors.Add("Author is necessery!");
                }
                if (book.AvailableQuantity < 0)
                {
                    errors.Add("Quantity can't be less than 0!");
                }

                if (errors.Count > 0)
                {
                    return Json(new { result = "false", errors = errors });
                }

                else
                {
                    storeBook.Name = book.Name;
                    storeBook.Author = book.Author;
                    storeBook.AvailableQuantity = book.AvailableQuantity;

                    _db.SaveChanges();

                    return Json(new { result = "true" });
                }
            }

            else
            {
                return Json(new { result = "false" });
            }
        }


        [HttpPost("[action]")]
        public JsonResult AddOrder(Order order)
        {

            List<string> errors = new List<string>();

            Book book = _db.Books.Where(o => o.ID == order.BookID).FirstOrDefault();
            User user = _db.Users.Where(o => o.ID == order.UserID).FirstOrDefault();

            if (book != null && book.AvailableQuantity > 0 && user != null)
            {
                order.BookingDate = DateTime.Now.Date;

                if (order.ExpectedReturnDate >= order.BookingDate)
                {
                    _db.Add(order);
                    book.AvailableQuantity -= 1;
                    _db.SaveChanges();

                    return Json(new { result = "true"});
                }
                else
                {
                    errors.Add("Dates messed up");
                    return Json(new { result = "false", errors });
                }

            }
            else
            {
                //a bit awkward msg
                errors.Add("We don't have this book :(");
                return Json(new { result = "false", errors });
            }
        }

        [HttpGet("[action]")]
        public JsonResult GetOrders()
        {
            var orders = _db.Orders.ToList();
            var book1 = orders.FirstOrDefault().Book;
            List<object> result = new List<object>();
            foreach (Order ord in orders)
            {
                var user = _db.Users.Where(o => o.ID == ord.UserID).FirstOrDefault();
                var book = _db.Books.Where(o => o.ID == ord.BookID).FirstOrDefault();
                result.Add(new { id = ord.ID, book = new { name = book.Name, author = book.Author, id = book.ID }, user = new { id = user.ID, name = user.Name }, bookingDate = ord.BookingDate, expectedReturnDate = ord.ExpectedReturnDate, actualReturnDate = ord.ActualReturnDate, daysAfterDue = (DateTime.Now - ord.ExpectedReturnDate).Days });
            }
            return Json(result);
        }

        [HttpGet("[action]/{id}")]
        public JsonResult BookReturned(int id)
        {
            var order = _db.Orders.Where(o => o.ID == id).FirstOrDefault();
            var book = _db.Books.Where(o => o.ID == order.BookID).FirstOrDefault();
            if (order != null && order.ActualReturnDate == null)
            {
                order.ActualReturnDate = DateTime.Now.Date;
                book.AvailableQuantity ++;
                _db.SaveChanges();
                return Json(new { result = "true" });
            }
            else
            {
                return Json(new { result = "false", errors = "Order closed or doesn't exist" });
            }
        }

        [HttpGet("[action]")]
        public ActionResult GetExcel()
        {
            var fileDownloadName = "Guys-" + DateTime.Now.Date + ".xlsx";
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var data = new List<string[]>();
            var headers = new string[] { "Customer", "Book name", "Author", "Booking day", "Expected return date", "Days after duedate"};

            var overdueOrders = _db.Orders.Where(o => o.ActualReturnDate == null & (DateTime.Now.Date - o.ExpectedReturnDate.Date).Days > 0).ToList();

            foreach (Order ord in overdueOrders)
            {
                var user = _db.Users.Where(o => o.ID == ord.UserID).FirstOrDefault();
                var book = _db.Books.Where(o => o.ID == ord.BookID).FirstOrDefault();

                data.Add(new string[] {
                    user.Name,
                    book.Name,
                    book.Author,
                    ord.BookingDate.Date.ToString().Substring(0, 10),
                    ord.ExpectedReturnDate.ToString().Substring(0, 10),
                    (DateTime.Now.Date - ord.ExpectedReturnDate.Date).Days.ToString()
                });

            }


            Excel.ExcelExporter excel = new Excel.ExcelExporter();

            return new FileStreamResult(excel.GetExcel(data, headers), contentType) { FileDownloadName = fileDownloadName };
        }
    }
}