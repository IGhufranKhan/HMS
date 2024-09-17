
using HMS.Abstractions;

namespace HMS.Services
{
    public class UploadPictureService : IUploadPictureService
    {
        public string UploadPicture(IFormFile profilePicture)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            // Generate a unique file name
            var pictureName = "pp-" + Guid.NewGuid() + Path.GetExtension(profilePicture.FileName);
            var filePath = Path.Combine("wwwroot/uploads", pictureName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                profilePicture.CopyTo(stream);
            }

            return pictureName;
        }
    }
}
