using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fina.Core.Requests.Categories
{
    public class DeleteCategoryRequest : Request
    {
        public long Id{ get; set; }
    }
}