using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyResume.Data;
using MyResume.Models;

namespace MyResume.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public IQueryable<Comment> Get(int id)
        {
            return _context.Comments.Where(c => c.PostId == id);
        }

        // POST: api/Comments
        [HttpPost]
        public Comment Post(int id, [FromBody] Comment comment)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return null;

            comment.Post = post;
            comment.CreatedAt = DateTime.Now;

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }
    }
}
