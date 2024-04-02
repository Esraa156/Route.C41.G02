using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Route.C41.G02.PL.Helpers
{
    public static class DocumentSettings
    {

        public static string UploadFile(IFormFile file, string FolderName)
        {
            //  string FolderPath=$"Directory.GetCurrentDirectory()\\wwwroot\\files\\{FolderName}";
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", FolderName);
            if (!Directory.Exists(FolderPath))
            
                Directory.CreateDirectory(FolderPath);

                //Uniqe
                string filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                string filepath = Path.Combine(FolderPath, filename);

                //savefile

                var filestram = new FileStream(filepath, FileMode.Create);

                file.CopyTo(filestram);

                return filename;
            }
        



            public static void Delete(string filename,string foldername)
            {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldername);

            if(File.Exists(filepath)) { 
            
                File.Delete(filepath);

            }
        }

    }
    }


