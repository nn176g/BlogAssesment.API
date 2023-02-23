using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestBlog.Business.Interfaces;
using TestBlog.Data.Models;
using TestBlog.Repository.Interfaces;

namespace TestBlog.Business.BlogBusiness
{
    
    public class BlogBusiness: IBlogBusiness
    {
        private readonly IBlogRepository _blogRepository;

        public BlogBusiness(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IEnumerable<Blogs> GetBlogs()
        {
           return  _blogRepository.GetBlogs();
        }

        public Blogs GetBlogsById(int id)
        {
            return _blogRepository.GetBlog(id);
        }

        public IEnumerable<Blogs> GetBlogsBySearchData(string searchData,string id)
        {
            return _blogRepository.GetBlogs(searchData,id);
        }
        public IEnumerable<Blogs> GetBlogsByOwner(string applicationUserId)
        {
            return _blogRepository.GetBlogsByOwner(applicationUserId);
        }
        public Comments GetCommentById(int id)
        {
            return _blogRepository.GetComment(id);
        }

        public IEnumerable<Comments> GetCommentByUserId(string id)
        {
            return _blogRepository.GetCommentByUser(id);
        }

        public async Task<Blogs> Insert(Blogs model)
        {
            return await _blogRepository.Add(model);
        }
        public async Task<Comments> Insert(Comments model)
        {
            return await _blogRepository.Add(model);
        }

        public async Task<Blogs> Update(Blogs blog)
        {
            return await _blogRepository.Update(blog);
        }

        public IEnumerable<Blogs>GetUserBlogs(string id)
        {
            return _blogRepository.GetBlogsByOwner(id);
        }

        public async Task<Blogs> DeleteBlog(int id)
        {
            await _blogRepository.DeleteBlog(id);

            return new Blogs();
        }
        public async  Task<Comments> DeleteComment(int id)
        {
            await _blogRepository.DeleteComment(id);
            return new Comments();
        }
    }
}
