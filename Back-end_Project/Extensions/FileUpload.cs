namespace Back_end_Project.Extensions
{
    public static class FileUpload
    {
        public static string CreateImage(this IFormFile file, string root, string path)
        {

            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string FullPath = Path.Combine(root, path, fileName);
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}
