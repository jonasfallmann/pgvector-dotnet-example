using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using PGVectorDemo.Database;

namespace PGVectorDemo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController(DemoDbContext dbContext) : ControllerBase
    {
        private DemoDbContext DatabaseContext { get; set; } = dbContext;

        [HttpPost("create-vector")]
        public async Task<IActionResult> CreateVector(float x, float y, float z)
        {
            DatabaseContext.DemoEntities.Add(new DemoEntity() { Name = "hallo", Embedding = new Vector(new float[] { x, y, z }) });
            await DatabaseContext.SaveChangesAsync();
            return Ok("hello there");
        }
        
        [HttpGet("demo")]
        public async Task<IActionResult> GetClosestVector(float x, float y, float z)
        {
            var embedding = new Vector(new float[] { x, y, z });
            var items = await DatabaseContext.DemoEntities
                .OrderBy(x => x.Embedding!.L2Distance(embedding))
                .Take(5)
                .ToListAsync();
            return Ok(items);
        }
    }
    
    
}
