namespace JobOffers.Utilities
{
    public static class Helper
    {

        // This Method will create a file path ("wwwroot\Uploads/Categories) and return a photo in the Edit view.
        public static async Task<string> UploadImage(List<IFormFile> Files , string folderName)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".jpg";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads/" + folderName, ImageName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return ImageName;
                    }
                }
            }
            return string.Empty;
        }




        public static async Task<string> UploadFile(List<IFormFile> Files, string folderName)
        {
            foreach (var file in Files)
            {
                if (file.Length > 0)
                {
                    string FileName = Guid.NewGuid().ToString() + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".pdf";
                    var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Downloads/" + folderName, FileName);
                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await file.CopyToAsync(stream);
                        return FileName;
                    }
                }
            }
            return string.Empty;
        }



        public static MemoryStream DownloadSinghFile(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }

    }
}
