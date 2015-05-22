namespace Peek.Web.Infrastructure.FileStorage
{
    using System.Collections.Generic;
    using Peek.Web.Areas.Administration.InputModels;

    public interface IStorageManager
    {
        string UploadProductImages(ProductInputModel product);

        IEnumerable<string> GetFileUrls(string folderId);
    }
}
