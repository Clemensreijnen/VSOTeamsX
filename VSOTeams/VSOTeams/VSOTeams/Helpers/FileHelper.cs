using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VSOTeams.Helpers
{
    internal static class FileHelper
    {
        internal static async void DownloadImage(Uri Url, string fileName)
        {
            IFolder storageFolder = FileSystem.Current.LocalStorage;

            HttpClientHelper helper = new HttpClientHelper();
            var _credentials =  await LoginInfo.GetCredentials();

            HttpClient _httpClient = helper.CreateHttpClient(_credentials);

            var byteArray = await _httpClient.GetByteArrayAsync(Url.ToString());
            IFile img = await GetOrCreateFileFromLocalFolder(fileName);

            using (var fs = await img.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(byteArray);
                    writer.Flush();
                    writer.Close();
                }
                
            }
        }

        internal static async Task<IFolder> GetStorageFolder(string foldername)
        {
            bool bestaatie = false;
            IFolder storageFolder = FileSystem.Current.LocalStorage;
            IFolder returnFolder = null;
            try
            {
                returnFolder = await storageFolder.GetFolderAsync(foldername);
                bestaatie = true;
            }
            catch (Exception)
            {
                // hij bestaat niet
            }

            if (bestaatie != true)
            {
                returnFolder = await storageFolder.CreateFolderAsync(foldername, 
                    CreationCollisionOption.OpenIfExists);
            }

            return returnFolder;

        }

        internal static async Task<bool> DeleteStorageFile(string fileName)
        {
            try
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        internal static async Task<bool> CheckIfFileExsistsInLocalFolder(string fileName)
        {
            try
            {
               
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static async Task<bool> CheckIfFileExsistsInFolder(string fileName, string folderName)
        {
            try
            {
                IFolder folder = await GetStorageFolder(folderName);
                var file = await folder.GetFileAsync(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static async Task<IFile> GetOrCreateFileInFolder(string fileName, string folderName)
        {
            try
            {
                IFolder folder = await GetStorageFolder(folderName);
                var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                return file;
            }
            catch (Exception)
            {
                return null;
            }
        }


        internal static async Task<IFile> GetOrCreateFileFromLocalFolder(string fileName)
        {
            try
            {
               return FileSystem.Current.LocalStorage.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists).Result;
             }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
