using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorWASM20221128.Shared.Models
{
    public class BlogModel
    {
        public int Blog_Id { get; set; }
        public string Blog_Title { get; set; }
        public string Blog_Author { get; set; }
        public string Blog_Content { get; set; }
        public string CreatedUser { get; set; } = "";
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
