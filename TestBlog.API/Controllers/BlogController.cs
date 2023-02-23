using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestBlog.Business.Interfaces;
using TestBlog.Data.Models;

namespace TestBlog.API.Controllers
{
    
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IBlogBusiness _blogBusiness;
        public BlogController(ILogger<BlogController> logger, IBlogBusiness blogBusiness)
        {
            _logger = logger;
            _blogBusiness = blogBusiness;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_blogBusiness.GetBlogs());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
            
        }

        [HttpGet]
        [Route("api/[controller]/search")]
        public IActionResult GetBlogsBySearchData(string data, string id)
        {
            try
            {
                return Ok(_blogBusiness.GetBlogsBySearchData(data,id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }

        [HttpGet]
        [Route("api/[controller]/comment")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                return Ok(_blogBusiness.GetCommentById( id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }

        [HttpGet]
        [Route("api/[controller]/commentsbyuser")]
        public IActionResult GetCommentsByUserId(string userId)
        {
            try
            {
                return Ok(_blogBusiness.GetCommentByUserId(userId));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }

        [HttpGet]
        [Route("api/[controller]/byuser")]
        public IActionResult GetBlogsByAppUser(string appId)
        {
            try
            {
                return Ok(_blogBusiness.GetBlogsByOwner(appId));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public Blogs GetById(int id)
        {
            try
            {
                var blog = _blogBusiness.GetBlogsById(id);
                Response.StatusCode = 200;
                return blog;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }
        [HttpPost]
        [Route("api/[controller]/insertblog")]
        public async Task<IActionResult> InsertBlog(Blogs model)
        {
            try
            {
                return Ok(await _blogBusiness.Insert(model));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }
            
        }
        [HttpPost]
        [Route("api/[controller]/insertcomment")]
        public async Task<IActionResult> InsertComment(Comments model)
        {
            try
            {
                return Ok(await _blogBusiness.Insert(model));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }
        [HttpPut]
        [Route("api/[controller]/update")]
        public async Task<IActionResult> Update(Blogs model)
        {
            try
            {
                return Ok(await _blogBusiness.Update(model));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }
        [HttpDelete]
        [Route("api/[controller]/deleteblog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            try
            {
                return Ok(await _blogBusiness.DeleteBlog(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }
        [HttpDelete]
        [Route("api/[controller]/deletecomment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                return Ok(await _blogBusiness.DeleteComment(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw e;
            }

        }
    }
}