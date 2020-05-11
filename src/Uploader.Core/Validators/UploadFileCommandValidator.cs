using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Uploader.Core.Validators
{
    public class UploadFileCommandValidator 
    {
        public static void FileIsValid(IFormFile file)
        {
            if (file == null)
            {
                throw new Exception("File is required");
            }
            
            var allowableExtFiles = new string []{"csv", "xml"};

            if (!allowableExtFiles.Contains(file.FileName.Split(".")[1]))
            {
                throw new Exception("Unknown format");
            }
        }
    }
}