using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Dtos
{
    public class DefaultDto<T>
    {
        public DefaultDto(T value)
        {
            this.Result = value;
        }

        public T Result { get; set; }
    }
}
