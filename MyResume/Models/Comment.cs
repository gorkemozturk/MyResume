using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Post Post { get; set; }
    }
}
