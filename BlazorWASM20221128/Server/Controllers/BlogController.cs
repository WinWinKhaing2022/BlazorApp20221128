using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using BlazorWASM20221128.Shared.Models;

namespace BlazorWASM20221128.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BlogController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{pageNo}/{rowCount}")]
        public async Task<IEnumerable<BlogModel>> GetListAsync(int pageNo = 1, int rowCount = 10)
        {
            string query = $@"
declare @rowCount int = {rowCount}
declare @pageNo int = {pageNo}

declare @skipRowCount int 
set @skipRowCount=  ((@pageNo - 1) * @rowCount)
select * from Tbl_Blog
ORDER BY Blog_Id DESC
OFFSET @skipRowCount ROWS
FETCH NEXT @rowCount ROWS ONLY;
";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                return await db.QueryAsync<BlogModel>(query);
            }

            //db.TblBlog.AsNoTracking().Skip(pageNo - 1 * rowCount).Take(rowCount).ToList();
        }

        [HttpGet("{id}")]
        public async Task<BlogModel> Get(int id)
        {
            string query = $"select * from tbl_blog where blog_id={id}";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                var response = await db.QueryAsync<BlogModel>(query);
                return response.FirstOrDefault();
            }
        }

        [HttpPost]
        public async Task<int> AddBlog([FromBody] BlogModel reqModel)
        {
            string query = $@"insert into Tbl_Blog (Blog_Title, Blog_Author, Blog_Content)
values('{reqModel.Blog_Title}', '{reqModel.Blog_Author}', '{reqModel.Blog_Content}')
";
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                return await db.ExecuteAsync(query);
            }
        }
    }
}
