﻿using Microsoft.AspNetCore.Http;

namespace ETicaretServer.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles);
        
        Task<bool> CopyFileAsync(string path, IFormFile file);


    }
}
