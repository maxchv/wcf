using System.Data.Entity;

namespace FileTransferService
{
    class FileTransferContext : DbContext
    {
        public FileTransferContext() : base("FilesTransferDBCS")
        {
            
        }

        public DbSet<FileInfoEx> Files { get; set; }
    }
}