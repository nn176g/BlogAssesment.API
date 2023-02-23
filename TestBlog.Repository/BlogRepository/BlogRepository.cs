using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBlog.Data.Models;
using TestBlog.Repository.Interfaces;

namespace TestBlog.Repository.BlogRepository
{
    
    public class BlogRepository: IBlogRepository
    {
        private readonly DBContext _dbContext;
        private readonly ILogger<BlogRepository> _logger;

        public BlogRepository(DBContext dBContext , ILogger<BlogRepository> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }

        public IEnumerable<Blogs> GetBlogs()
        {
            _logger.LogInformation("Attempting to get all Database Blogs");
            var result= _dbContext.Blogs.ToList();
            if (result!=null)
            {
                _logger.LogInformation("GetBlog successful");
                return result;
            }
            _logger.LogWarning("No Blogs found in Database");
            return result;
            
        }

        public IEnumerable<Blogs> GetBlogs(string searchString, string id)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                _logger.LogInformation("Attempting to get all Database Blogs by searchString");
                   var result = _dbContext.Blogs
                    .OrderByDescending(blog => blog.UpdatedOn)
                    .Include(blog => blog.Creator)
                    .Include(blog => blog.Comments)
                    .Where(blog => blog.Title.Contains(searchString) || blog.Content.Contains(searchString));
                if (result != null)
                {
                    _logger.LogInformation("GetBlog by searchString successful");
                    return result;
                }
                _logger.LogWarning("No records matches with searchString");
                return result;
            }else
            {
                _logger.LogInformation("Attempting to get all Database Blogs by searchString");
                var result = _dbContext.Blogs
                   .OrderByDescending(blog => blog.UpdatedOn)
                   .Include(blog => blog.Creator)
                   .Include(blog => blog.Comments);
                if (result != null)
                {
                    _logger.LogInformation("GetBlog by searchString successful");
                    return result;
                }
                _logger.LogWarning("No records matches with searchString");
                return result;
            }

            

        }

        public async Task<Blogs> Add(Blogs blog)
        {
            _logger.LogInformation("Attempting Insert a Record in the Blog Table");
            var tempBlog= NormalizeEntity( blog);
            _dbContext.Add(tempBlog);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Insert Blog method successful");
            return tempBlog;
        }

        public Blogs GetBlog(int blogId)
        {
            _logger.LogInformation("Attempting to get Blog by Id");
            var result= _dbContext.Blogs
                .Include(blog => blog.Creator)
                .Include(blog => blog.Comments)
                    .ThenInclude(comment => comment.Author)
                .Include(blog => blog.Comments)
                    .ThenInclude(comment => comment.Blog.Comments)
                        .ThenInclude(reply => reply.Parent)
                .FirstOrDefault(blog => blog.Id == blogId);
            
            if (result != null)
            {
                _logger.LogInformation("GetBlog by Id successful");
                return result;
            }
            _logger.LogWarning("No records matches with Id");
            return result;

        }

        public Comments GetComment(int commentId)
        {
            _logger.LogInformation("Attempting to get Comments of a Blog");
            var result= _dbContext.Comments
                .Include(comment => comment.Author)
                .Include(comment => comment.Blog)
                .Include(comment => comment.Parent)
                .FirstOrDefault(comment => comment.Id == commentId);
            if (result != null)
            {
                _logger.LogInformation("GetComment successful");
                return result;
            }
            _logger.LogWarning("No records matches with Id");
            return result;
        }

        public IEnumerable<Comments> GetCommentByUser(string userId)
        {
            _logger.LogInformation("Attempting to get Comments of a Blog");
            var result = _dbContext.Comments
                .Include(comment => comment.Author)
                .Include(comment => comment.Blog)
                .Include(comment => comment.Parent)
                .Where(comment => comment.AuthorId == userId);

            if (result != null)
            {
                _logger.LogInformation("GetComment successful");
                return result;
            }
            _logger.LogWarning("No records matches with Id");
            return result;
        }

        public async Task<Comments> Add(Comments comment)
        {
            _logger.LogInformation("Attempting Insert a Record in the Blog Table");
           Comments tempComment= NormalizeEntity(comment);
            _dbContext.Add(tempComment);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Insert Comment method successful");
            return comment;
        }
        public async Task<Blogs> Update(Blogs blog)
        {
            _logger.LogInformation("Attempting Update a Record in Blog Table");
            var tempBlog = NormalizeEntity(blog);
            tempBlog.Id = blog.Id;
            _dbContext.Update(tempBlog);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Update method successful");
            return tempBlog;
        }

        public async  Task DeleteBlog(int id)
        {
            _logger.LogInformation("Attempting to Delete a Record in Blog Table");
            var blog = GetBlog(id);
            if (blog!= null)
            {
                _dbContext.Blogs.Remove(blog);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Delete method successful");
            }else
            {
                _logger.LogInformation("Blog Id was not found");
            }

        }
        public async Task DeleteComment(int commentId)
        {
            _logger.LogInformation("Attempting to Delete a Record in Blog Table");
            var comment = GetComment(commentId);
            if (comment != null)
            {
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Delete method successful");
            }
            else
            {
                _logger.LogInformation("Comment Id was not found");
            }
        }

        public IEnumerable<Blogs> GetBlogsByOwner(string id)
        {
            _logger.LogInformation("Attempting to Blogs by Owner");
            var result= _dbContext.Blogs
                .Include(blog => blog.Creator)
                .Include(blog => blog.Approver)
                .Include(blog => blog.Comments)
                    .Where(blog=>blog.Creator.Id==id);
            if (result != null)
            {
                _logger.LogInformation("GetBlogsByOwner successful");
                return result;
            }
            _logger.LogWarning("No records matches with Id");
            return result;
        }
        private Blogs NormalizeEntity(Blogs blog)
        {
            Blogs tempBlog = new Blogs
            {
                Approved = blog.Approved,
                ApproverId = blog.Approver.Id,
                Content = blog.Content,
                Comments = blog.Comments,
                Published = blog.Published,
                Title = blog.Title,
                UpdatedOn = blog.UpdatedOn,
                CreatorId = blog.Creator.Id,
                CreatedOn = blog.CreatedOn

            };

            return tempBlog;
        }
        private Comments NormalizeEntity(Comments comment)
        {

            Comments tempComment = new Comments
            {
                BlogId= comment.Blog.Id,
                AuthorId=comment.Author.Id,
                Content=comment.Content,
                ParentId= comment.Parent?.Id,
                CreatedOn=comment.CreatedOn
            };

            return tempComment;
        }

    }
}
