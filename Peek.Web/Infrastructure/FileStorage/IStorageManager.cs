using Peek.Web.Areas.Administration.InputModels;

namespace Peek.Web.Infrastructure.FileStorage
{
    public interface IStorageManager
    {
        string UploadProductImages(ProductInputModel product);
    }
}
