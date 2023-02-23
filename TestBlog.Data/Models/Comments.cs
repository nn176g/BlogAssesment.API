using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBlog.Data.Models
{
    public partial class Comments
    {
        public Comments()
        {
            InverseParent = new HashSet<Comments>();
        }

        [Key]
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(AuthorId))]
        [InverseProperty(nameof(AspNetUsers.Comments))]
        public virtual AspNetUsers Author { get; set; }
        [ForeignKey(nameof(BlogId))]
        [InverseProperty(nameof(Blogs.Comments))]
        public virtual Blogs Blog { get; set; }
        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(Comments.InverseParent))]
        public virtual Comments Parent { get; set; }
        [InverseProperty(nameof(Comments.Parent))]
        public virtual ICollection<Comments> InverseParent { get; set; }
    }
}
