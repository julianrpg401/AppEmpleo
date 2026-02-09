using AppEmpleo.Interfaces.Services.Storage;

namespace AppEmpleo.Class.Utilities.Storage
{
    public class FileSystemResumeStorage : IResumeStorage
    {
        private const string ResumeFolderName = "curriculums";
        private readonly IWebHostEnvironment _environment;

        public FileSystemResumeStorage(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var folder = Path.Combine(_environment.WebRootPath, ResumeFolderName);
            Directory.CreateDirectory(folder);

            var extension = Path.GetExtension(file.FileName);
            var storedFileName = $"{Guid.NewGuid()}{extension}";
            var savePath = Path.Combine(folder, storedFileName);

            await using var stream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
            await file.CopyToAsync(stream, cancellationToken);

            return storedFileName;
        }

        public string GetAbsolutePath(string storedFileName)
        {
            var safeName = Path.GetFileName(storedFileName);
            return Path.Combine(_environment.WebRootPath, ResumeFolderName, safeName);
        }

        public bool Exists(string storedFileName)
        {
            var path = GetAbsolutePath(storedFileName);
            return File.Exists(path);
        }
    }
}
