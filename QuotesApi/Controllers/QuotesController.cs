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
        public IEnumerable<Quote> Get()
        {
            return _quotesDbContext.Quotes;
        }

        // GET: api/quotes/5
        [HttpGet("{id}")]
        public Quote Get(int id)
        {
            return _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] Quote model)
        //{
        //    if(model != null)
        //    {
        //        Quotes.Add(model);

        //        return CreatedAtAction(nameof(Post), new { Id = model.Id, Message = "Created" });

        //    }

        //    return BadRequest();
        //}

        //[HttpPut("{id}")]
        //public IActionResult Put(int id, [FromBody] Quote model)
        //{
        //    if( id > 0 && model != null)
        //    {
        //        var idx = Quotes.FindIndex(x => x.Id == id);

        //        if(idx >= 0)
        //        {
        //            Quotes[idx] = model;

        //            return Ok(new { Id = id, Message = "Updated" });

        //        }

        //        return NotFound(new { Id = id, Message = "Not Found" });

        //    }

        //    return BadRequest();

        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    if(id > 0)
        //    {
        //        var idx = Quotes.FindIndex(x => x.Id == id);

        //        if (idx >= 0)
        //        {
        //            Quotes.RemoveAt(idx);

        //            return Ok(new { Id = id, Message = "Deleted" });
        //        }

        //        return NotFound(new { Id = id });
        //    }

        //    return BadRequest();
        //}

    }

}
