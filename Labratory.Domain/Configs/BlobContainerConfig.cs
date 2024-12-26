using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Domain.Configs
{
    public class BlobContainerConfig
    {
        public string ContainerName { get; set; }

        public string Url { get; set; }

        public Dictionary<string, string> CommonMIMETypes { get; set; }

    }
}
