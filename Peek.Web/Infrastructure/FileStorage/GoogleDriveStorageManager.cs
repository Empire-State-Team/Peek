namespace Peek.Web.Infrastructure.FileStorage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Drive.v2;
    using Google.Apis.Drive.v2.Data;
    using Google.Apis.Services;
    using Peek.Web.Areas.Administration.InputModels;

    public class GoogleDriveStorageManager : IStorageManager
    {
        private readonly DriveService service;

        public GoogleDriveStorageManager()
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = "342979829511-hbo1ldurpr0dje7u5r9dvqv4vefn79hv.apps.googleusercontent.com",
                ClientSecret = "0F4Mn5fNMEsVvPi6blFlwRjf"
            };

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                clientSecrets, new[] { DriveService.Scope.Drive }, "peekUser", CancellationToken.None).Result;

            this.service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "Peek"
            });
        }

        public string UploadProductImages(ProductInputModel product)
        {
            var folderBody = new Google.Apis.Drive.v2.Data.File
            {
                Title = product.Name,
                MimeType = "application/vnd.google-apps.folder"
            };

            var folder = this.service.Files.Insert(folderBody).Execute();
            var publicPermission = new Permission { Role = "reader", Type = "anyone" };
            this.service.Permissions.Insert(publicPermission, folder.Id).Execute();

            foreach (var image in product.Images)
            {
                var fileBody = new Google.Apis.Drive.v2.Data.File
                {
                    Title = Guid.NewGuid().ToString("N"),
                    MimeType = "image/jpeg",
                    Parents = new[] { new ParentReference { Id = folder.Id } }
                };

                this.service.Files.Insert(fileBody, image.InputStream, "image/jpeg").Upload();
            }

            return folder.Id;
        }

        public IEnumerable<string> GetFileUrls(string folderId)
        {
            var children = this.service.Children.List(folderId).Execute();
            var urls = children.Items.Select(c => "https://drive.google.com/uc?id=" + c.Id);

            return urls;
        }
    }
}
