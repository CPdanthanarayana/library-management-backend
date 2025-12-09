using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<BooksController> _logger;

        public BooksController(AppDbContext db, ILogger<BooksController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAll()
        {
            var books = await _db.Books.AsNoTracking().ToListAsync();
            return Ok(books);
        }

        // GET: api/books/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetById(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();     
            return Ok(book);                          
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] Book input)
        {
            try
            {
                _db.Books.Add(input);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = input.Id }, input); // 201
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DB update error while creating book");
                return StatusCode(500, "An error occurred while saving the book.");
            }
        }

        // PUT: api/books/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Book input)
        {
            if (id != input.Id)
                return BadRequest("ID in URL and body must match.");

            var existing = await _db.Books.FindAsync(id);
            if (existing == null) return NotFound(); 

            existing.Title = input.Title;
            existing.Author = input.Author;
            existing.Description = input.Description;

            try
            {
                await _db.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error while updating book id {Id}", id);
                return StatusCode(500, "An error occurred while updating the book.");
            }
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Books.FindAsync(id);
            if (existing == null) return NotFound();

            try
            {
                _db.Books.Remove(existing);
                await _db.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "DB update error while deleting book id {Id}", id);
                return StatusCode(500, "An error occurred while deleting the book.");
            }
        }
    }
}
