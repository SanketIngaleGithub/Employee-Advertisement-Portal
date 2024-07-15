using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System.IO;
namespace EmployeeAdvertisementPortal.Helpers
{
    public static class FileHelper
    {
        public static void SaveAdvertisementFile(AdvertisementViewModel model, string serverPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(model.AdvertisementFile.FileName);
            string extension = Path.GetExtension(model.AdvertisementFile.FileName);
            fileName = fileName + extension;
            model.MediaPath = $"/Content/Advertisement/{fileName}";
            fileName = Path.Combine(serverPath, fileName);
            model.AdvertisementFile.SaveAs(fileName);
        }
    }
}