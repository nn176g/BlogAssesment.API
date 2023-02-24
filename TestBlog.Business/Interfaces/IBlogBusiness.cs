using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Business.Interfaces
{
    public interface IBlogBusiness
    {
        IEnumerable<Blogs> GetBlogs();
        IEnumerable<Blogs> GetBlogsByOwner(string applicationUserId);
        IEnumerable<Blogs> GetBlogsBySearchData(string searchData, string id);
        IEnumerable<Blogs> GetBlogsById(int id);
        Task<Blogs> Insert(Blogs model);
        Task<Comments> Insert(Comments model);
        Comments GetCommentById(int id);
        Task<Blogs> Update(Blogs blog);
        IEnumerable<Blogs> GetUserBlogs(string id);
        Task<Blogs> DeleteBlog(int id);
        Task<Comments> DeleteComment(int id);
        IEnumerable<Comments> GetCommentByUserId(string id);
    }
}
