using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Configs
{
    public class CommonConfig
    {
        public string[] AllowedImageExt { get; set; }


        public CommonConfig()
        {
                this.AllowedImageExt = new string[0];
        }

    }
}
