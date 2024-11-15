
﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CRMAPI3.Class
{
    public interface IRoomS3
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<byte[]> DownloadFileAsync(string fileKey);
    }
}