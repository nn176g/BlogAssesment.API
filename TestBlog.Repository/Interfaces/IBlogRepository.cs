using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Repository.Interfaces
{
    public interface IBlogRepository
    {
        Blogs GetBlog(int id);
        IEnumerable<Blogs> GetBlogs();
        IEnumerable<Blogs> GetBlogs(string searchString, string id);
        Task<Blogs> Add(Blogs blog);
        Comments GetComment(int commentId);
        Task<Comments> Add(Comments comment);
        Task<Blogs> Update(Blogs blog);
        IEnumerable<Blogs> GetBlogsByOwner(string id);
        Task DeleteComment(int commentId);
        Task DeleteBlog(int id);
        IEnumerable<Comments> GetCommentByUser(string userId);
    }
}
