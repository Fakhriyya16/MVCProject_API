namespace MVCProject_API.Helpers.Extensions
{
    public static class FileExtensions
    {
        public static string FileNameGenerator(this string fileName)
        {
            return Guid.NewGuid().ToString() + fileName;
        }

        public static string GenerateFilePath(this IWebHostEnvironment env, string folder, string fileName)
        {
            return Path.Combine(env.WebRootPath, folder, fileName);
        }

        public static async Task SaveToFileAsync(this IFormFile file, string path)
        {
            using FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public static void DeleteImage(this string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
