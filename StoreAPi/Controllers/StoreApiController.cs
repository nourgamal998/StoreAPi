using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersistenceLayer.Data.PersistenceLayer.Data;


namespace PersistenceLayer.Data

{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreApiController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public StoreApiController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Products.ToList());
        }

    }
}
