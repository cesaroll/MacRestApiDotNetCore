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

            if(quote != null)
            {
                return Ok(quote);
            }

            return NotFound();

        }


        // POST: api/quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote model)
        {
            if (model != null)
            {
                _quotesDbContext.Quotes.Add(model);
                _quotesDbContext.SaveChanges();

                return CreatedAtAction(nameof(Post), new { Id = model.Id, Message = "Created" });

            }

            return BadRequest();
        }


        // PUT: api/quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote model)
        {
            if (id > 0 && model != null)
            {
                var entity = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);

                if (entity != null)
                {
                    entity.Title = model.Title;
                    entity.Author = model.Author;
                    entity.Description = model.Description;
                    _quotesDbContext.SaveChanges();

                    return Ok(new { Id = id, Message = "Updated" });

                }

                return NotFound(new { Id = id, Message = "Not Found" });

            }

            return BadRequest();

        }


        // DELETE: api/quotes/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                var quote = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);

                if (quote != null)
                {
                    _quotesDbContext.Quotes.Remove(quote);
                    _quotesDbContext.SaveChanges();

                    return Ok(new { Id = id, Message = "Deleted" });
                }

                return NotFound(new { Id = id });
            }

            return BadRequest();
        }

    }

}
