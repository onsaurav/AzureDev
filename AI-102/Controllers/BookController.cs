using AI_102.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AI_102.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return LoadInitialBooks();
        }

        private IEnumerable<Book> LoadInitialBooks()
        {
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin", Publication = "Prentice Hall", PublicationDate = new DateOnly(2008, 8, 1), Language = "English", Price = 45.99 },
                new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt & David Thomas", Publication = "Addison-Wesley", PublicationDate = new DateOnly(1999, 10, 20), Language = "English", Price = 39.95 },
                new Book { Id = 3, Title = "Introduction to Algorithms", Author = "Thomas H. Cormen", Publication = "MIT Press", PublicationDate = new DateOnly(2009, 7, 31), Language = "English", Price = 89.50 },
                new Book { Id = 4, Title = "You Don’t Know JS Yet", Author = "Kyle Simpson", Publication = "Independently published", PublicationDate = new DateOnly(2020, 1, 28), Language = "English", Price = 29.99 },
                new Book { Id = 5, Title = "Artificial Intelligence: A Modern Approach", Author = "Stuart Russell & Peter Norvig", Publication = "Pearson", PublicationDate = new DateOnly(2020, 4, 1), Language = "English", Price = 119.00 },
                new Book { Id = 6, Title = "Design Patterns: Elements of Reusable Object-Oriented Software", Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides", Publication = "Addison-Wesley", PublicationDate = new DateOnly(1994, 10, 31), Language = "English", Price = 54.99 },
                new Book { Id = 7, Title = "Code Complete", Author = "Steve McConnell", Publication = "Microsoft Press", PublicationDate = new DateOnly(2004, 6, 19), Language = "English", Price = 58.99 },
                new Book { Id = 8, Title = "Deep Learning", Author = "Ian Goodfellow, Yoshua Bengio, Aaron Courville", Publication = "MIT Press", PublicationDate = new DateOnly(2016, 11, 18), Language = "English", Price = 84.95 },
                new Book { Id = 9, Title = "Refactoring", Author = "Martin Fowler", Publication = "Addison-Wesley", PublicationDate = new DateOnly(2018, 11, 19), Language = "English", Price = 49.99 },
                new Book { Id = 10, Title = "Soft Skills: The software developer’s life manual", Author = "John Sonmez", Publication = "Manning", PublicationDate = new DateOnly(2014, 12, 28), Language = "English", Price = 39.99 }
            };

            return books;
        }
    }
}
