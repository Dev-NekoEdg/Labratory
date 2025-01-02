using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Dtos
{
    public class FilterEnvelop<T>
    {
        public int CurrentPage { get; set; }

        public int PageSize{ get; set; }

        public int Pages { get; set; }

        public int TotalRecords { get; set; }

        public T Data { get; set; }
    }

    public class FilterSearch {

        public string Field { get; set; }

        public string Value { get; set; }
    }
}
