namespace Peek.Web.Infrastructure.FileStorage
{
    using Peek.Web.Areas.Administration.InputModels;

    public interface IStorageManager
    {
        string UploadProductImages(ProductInputModel product);
    }
}
