using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.BlogDTOs
{
    public class AddBlogDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageURL { get; set; }

        public List<string> Tags { get; set; }
    }
}
