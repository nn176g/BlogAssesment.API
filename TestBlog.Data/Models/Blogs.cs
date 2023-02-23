using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestBlog.Data.Models
{
    public partial class Blogs
    {
        public Blogs()
        {
            Comments = new HashSet<Comments>();
        }

        [Key]
        public int Id { get; set; }
        public string CreatorId { get; set; }
        public string ApproverId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Approved { get; set; }
        public bool Published { get; set; }
        public DateTime UpdatedOn { get; set; }

        [ForeignKey(nameof(ApproverId))]
        [InverseProperty(nameof(AspNetUsers.BlogsApprover))]
        public virtual AspNetUsers Approver { get; set; }
        [ForeignKey(nameof(CreatorId))]
        [InverseProperty(nameof(AspNetUsers.BlogsCreator))]
        public virtual AspNetUsers Creator { get; set; }
        [InverseProperty("Blog")]
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
