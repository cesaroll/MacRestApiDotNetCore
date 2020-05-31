using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Models;
using QuotesApi.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {

        private QuotesDbContext _quotesDbContext;


        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }


        // GET: api/quotes
        [HttpGet]
        public IActionResult Get()
        {
            var quotes = _quotesDbContext.Quotes;
            return Ok(quotes);
        }


        // GET: api/quotes/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quote = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);

            if(quote == null)
            {
                return NotFound(new { Id = id, Message = "Record Not Found", HasError = true });
            }

            return Ok(quote);

        }


        // POST: api/quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            _quotesDbContext.Quotes.Add(model);
            _quotesDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { Id = model.Id, Message = "Record Created", HasError = false });

        }

        // POST: api/quotes
        [HttpPost]
        [Route("[action]")]
        public IActionResult PostMany([FromBody] List<Quote> quotes) // See Custom input formatters: http://scottclewell.com/post/multiple-or-single-aspnetcore/
        {
            if (quotes == null || quotes.Count() == 0)
            {
                return BadRequest();
            }

            _quotesDbContext.Quotes.AddRange(quotes);
            _quotesDbContext.SaveChanges();

            return CreatedAtAction(nameof(Post), new { Ids = quotes.Select(x => x.Id) , Message = "Records Created", HasError = false });

        }


        // PUT: api/quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote model)
        {
            if (id <= 0 || model == null)
            {
                return BadRequest();
            }

            var quote = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);

            if (quote == null)
            {
                return NotFound(new { Id = id, Message = "Record Not Found", HasError = true });
            }

            quote.Title = model.Title;
            quote.Author = model.Author;
            quote.Description = model.Description;
            quote.Type = model.Type;
            quote.CreatedAt = model.CreatedAt;

            _quotesDbContext.SaveChanges();

            return Ok(new { Id = id, Message = "Record Updated Succesfully", HasError = false });


        }


        // DELETE: api/quotes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var quote = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);

            if (quote == null)
            {
                return NotFound(new { Id = id, Message = "Record Not Found", HasError = true });
            }

            _quotesDbContext.Quotes.Remove(quote);
            _quotesDbContext.SaveChanges();

            return Ok(new { Id = id, Message = "Deleted", HasError = false });

        }

    }

}
