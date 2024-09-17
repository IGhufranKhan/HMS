namespace HMS.Abstractions
{
    public interface IUploadPictureService
    {
        string UploadPicture(IFormFile profilePicture);
    }
}
