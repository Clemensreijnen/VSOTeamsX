using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VSOTeams.Helpers
{
    internal static class FileHelper
    {
        internal static async Task<ImageSource> DownloadImage(Uri Url, string fileName)
        {
            ImageSource source;
            var file = await GetFileFromLocalFolder(fileName);
            if(file != null)
            {
                source = file.Path;
                return source;
            }

            IFolder storageFolder = FileSystem.Current.LocalStorage;
            byte[] byteArray;

            var _credentials = await LoginInfo.GetCredentials();
            var username = _credentials.UserName;
            var password = _credentials.Password;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        System.Text.Encoding.UTF8.GetBytes(
                            string.Format("{0}:{1}", username, password))));

                
                byteArray = await client.GetByteArrayAsync(Url.ToString());
            }

            IFile img = GetOrCreateFileFromLocalFolder(fileName);

            using (var fs = await img.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(byteArray);
                    writer.Flush();
                    writer.Close();
                }
                
            }

            source = img.Path;
            return source;
            
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

        internal static async Task<IFile> GetFileFromLocalFolder(string fileName)
        {
            try
            {

                 if (await CheckIfFileExsistsInLocalFolder(fileName))
                 {
                     return await FileSystem.Current.LocalStorage.GetFileAsync(fileName);
                 }
                 else
                 {
                     return null;
                 }
            }
            catch (Exception)
            {
                return null;
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


        internal static IFile GetOrCreateFileFromLocalFolder(string fileName)
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
