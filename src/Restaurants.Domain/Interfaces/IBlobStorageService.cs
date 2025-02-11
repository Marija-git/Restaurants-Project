using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Interfaces
{
    public interface IBlobStorageService    {
        Task<string> UploadToBlobStorage(Stream data, string fileName);
    }
}
