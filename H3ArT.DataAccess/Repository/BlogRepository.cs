using H3ArT.DataAccess.Data;
using H3ArT.DataAccess.Repository.IRepository;
using H3ArT.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3ArT.DataAccess.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Blog blog)
        {
            var blogFromDb = _db.TblBlog.FirstOrDefault(u => u.blogID == blog.blogID);
            if (blogFromDb != null )
            {
                blogFromDb.creatorID = blog.creatorID;
                blogFromDb.title = blog.title;
                blogFromDb.description = blog.description;
                blogFromDb.createAt = blog.createAt;
                if(blogFromDb.imageUrl != null )
                {
                    blogFromDb.imageUrl = blog.imageUrl;

                }
            }
        }


    }
}
