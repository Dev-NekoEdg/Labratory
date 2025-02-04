using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labratory.Service.BlobStorage
{
    public interface IBlobStorageService
    {
        Task<string> SaveImageIntoBlobStorage(string extention, string newName, Stream stream);

        Task DeleteImageIntoBlobStorage(string completeName);
    }
}
