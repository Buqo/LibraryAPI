using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly DataContext context;
        public BookController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            return Ok(await this.context.Books.ToListAsync());
        }
        [HttpGet("{id}")]

        public async Task<ActionResult<Book>> GetBookById(string id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return BadRequest("Book not found.");

            }
            return Ok(book);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            var isbook = await context.Books.FindAsync(book.Id);
            if (isbook != null)
            {
                return BadRequest("Book with this id already exists.");

            }

            this.context.Books.Add(book);
            await this.context.SaveChangesAsync();
            return Ok(book);
        }

        [HttpPut]
        public async Task<ActionResult<Book>> UpdateBook(Book insbook)
        {
            var book = await context.Books.FindAsync(insbook.Id);
            if (book == null)
            {
                return BadRequest("Book with this id doesnt exists.");

            }
            book.Author = insbook.Author;
            book.Date = insbook.Date;
            book.Title = insbook.Title;
            await this.context.SaveChangesAsync();
            return Ok(book);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(string id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
            {
                return BadRequest("Book with this id doesnt exists.");

            }
            else
            {
                this.context.Books.Remove(book);
                await this.context.SaveChangesAsync();
                return Ok(book);
            }
        }

    }
}
