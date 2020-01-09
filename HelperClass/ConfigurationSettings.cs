using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using AngularApp.HelperClass;

namespace AngularApp.HelperClass
{
    public static class ConfigurationSettings
    {
         /// <summary>
        /// initialize HttpContext session instance
        /// </summary>
        public static HttpContext HttpContext => new HttpContextAccessor().HttpContext;

        /// <summary>
        /// Getter for DBConnection value mentioned in appsetings.json file in ConnectionStrings section with key = "strDBConn"
        /// </summary>
        public static string DBConnection
        {
            get
            {
                IOptions<ConnectionStrings> _connectionstrings = (IOptions<ConnectionStrings>)HttpContext.RequestServices.GetService(typeof(IOptions<ConnectionStrings>));
                return _connectionstrings.Value.StrDBConn;
            }
        }

        /// <summary>
        /// Getter for DBConnectionLog value mentioned in appsetings.json file in ConnectionStrings section with key = "strDBConnLog"
        /// </summary>
        public static string DBConnectionLog
        {
            get
            {
                IOptions<ConnectionStrings> _connectionstrings = (IOptions<ConnectionStrings>)HttpContext.RequestServices.GetService(typeof(IOptions<ConnectionStrings>));
                return _connectionstrings.Value.StrDBConnLog;
            }
        }

        /// <summary>
        /// Getter for FileUploadPath value mentioned in appsetings.json file in FolderPaths section with key = "FileUploadPath"
        /// </summary>
        public static string FileUploadPath
        {
            get
            {
                IOptions<FolderPaths> _folderPaths = (IOptions<FolderPaths>)HttpContext.RequestServices.GetService(typeof(IOptions<FolderPaths>));
                return _folderPaths.Value.FileUploadPath;
            }
        }

        /// <summary>
        /// Getter for FileCMSPhotosPath value mentioned in appsetings.json file in FolderPaths section with key = "FileCMSPhotosPath"
        /// </summary>
        public static string FileCMSPhotosPath
        {
            get
            {
                IOptions<FolderPaths> _folderPaths = (IOptions<FolderPaths>)HttpContext.RequestServices.GetService(typeof(IOptions<FolderPaths>));
                return _folderPaths.Value.FileCMSPhotosPath;
            }
        }

        /// <summary>
        /// Getter for DBName value mentioned in appsetings.json file  in FolderPaths section with key = "DBName"
        /// </summary>
         public static string DBName
        {
            get
            {
                IOptions<FolderPaths> _folderPaths = (IOptions<FolderPaths>)HttpContext.RequestServices.GetService(typeof(IOptions<FolderPaths>));
                return _folderPaths.Value.DBName;
            }
        }

        /// <summary>
        /// Getter for DBBackUpPath value mentioned in appsetings.json file  in FolderPaths section with key = "DBBackUpPath"
        /// </summary>
        public static string DBBackUpPath
        {
            get
            {
                IOptions<FolderPaths> _folderPaths = (IOptions<FolderPaths>)HttpContext.RequestServices.GetService(typeof(IOptions<FolderPaths>));
                return _folderPaths.Value.DBBackUpPath;
            }
        }

        /// <summary>
        /// Getter for SecretKey value mentioned in appsetings.json file  in PasswordEncrypt section with key = "SecretKey"
        /// </summary>
        public static string SecretKey
        {
            get
            {
                IOptions<PasswordEncrypt> _passwordencrypt = (IOptions<PasswordEncrypt>)HttpContext.RequestServices.GetService(typeof(IOptions<PasswordEncrypt>));
                return _passwordencrypt.Value.SecretKey;
            }
        }

        /// <summary>
        /// Getter for EncryptionDecryptionKey value mentioned in appsetings.json file  in PasswordEncrypt section with key = "EncryptionDecryptionKey"
        /// </summary>
        public static string EncryptionDecryptionKey
        {
            get
            {
                IOptions<PasswordEncrypt> _passwordencrypt = (IOptions<PasswordEncrypt>)HttpContext.RequestServices.GetService(typeof(IOptions<PasswordEncrypt>));
                return _passwordencrypt.Value.SecretKey;
            }
        }

        public static object SmtpCredentials { get; internal set; }
    }

    /// <summary>
    /// used for PasswordEncrypt section of appsettings.json
    /// </summary>
    public class PasswordEncrypt
    {
        public string SecretKey { get; set; }
    }

    /// <summary>
    /// used for FolderPaths section of appsettings.json
    /// </summary>
    public class FolderPaths
    {
        public string FileUploadPath { get; set; }
        public string FileCMSPhotosPath { get; set; }
        public string DBBackUpPath { get; set; }
        public string DBName { get; set; }
    }
}
