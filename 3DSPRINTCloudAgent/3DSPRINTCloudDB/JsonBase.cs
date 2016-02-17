using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Security.Permissions;
using Newtonsoft.Json.Linq;

namespace _3DSPRINTCloudDB
{
    abstract class JsonBase : IDisposable
    {
        protected JObject _json;
        protected bool _read;
        protected bool _write;
        static readonly string VIKey = "@1C2c3D4e8F7h7H8";

        public JObject json
        {
            get { return _json; }
        }

        public JsonBase(bool read = false, bool write = false)
        {
            _read = read;
            _write = write;
            _json = new JObject();
            Load();
        }

        public void Dispose()
        {
            Save();
        }

        ~JsonBase()
        {
            Dispose();
        }

        protected abstract string GetFileName();
        protected virtual bool Encription()
        {
            return false;
        }

        protected string GetFilePath()
        {
            return GetFolder() + '\\' + GetFileName();
        }

        static public void SetupDBLocation(string filepath)
        {
            try
            {
                DirectoryInfo dInfo = new DirectoryInfo(filepath);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                SecurityIdentifier sri = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                dSecurity.AddAccessRule(new FileSystemAccessRule(sri, FileSystemRights.FullControl, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
            }
            catch (Exception e)
            {
                Logger.Instance.error(e);
            }
        }

        public void SetupDB()
        {
            if (GetFileName() == null)
                return;
            if (!File.Exists(GetFilePath()))
            {
                bool write = _write;
                _write = true;
                Save();
                _write = write;
                SetupDBLocation(GetFilePath());
            }
        }

        static public string GetFolder()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string folder = appData + @"\3DSPRINTCloudAgent";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        public void Set(JObject new_json)
        {
            _json = new_json;
        }
        
        private string Encrypt(string plainText)
		{
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			byte[] keyBytes = new Rfc2898DeriveBytes(GetFilePath(), Encoding.ASCII.GetBytes(GetFileName())).GetBytes(256/8);
			var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
			
			byte[] cipherTextBytes;

			using (var memoryStream = new MemoryStream())
			{
				using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
					cryptoStream.FlushFinalBlock();
					cipherTextBytes = memoryStream.ToArray();
					cryptoStream.Close();
				}
				memoryStream.Close();
			}
			return Convert.ToBase64String(cipherTextBytes);
		}

        private string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(GetFilePath(), Encoding.ASCII.GetBytes(GetFileName())).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public void Save(double timeout = 20.0/*sec*/)
        {
            if (!_write)
                return;
            bool success = false;
            DateTime _timeout = DateTime.Now.AddSeconds(timeout);
            while (_timeout > DateTime.Now && !success)
            {
                try
                {
                    string data = _json.ToString(Newtonsoft.Json.Formatting.None);
                    data = Encription() ? Encrypt(data) : data;
                    File.WriteAllText(GetFilePath(), data);
                    
                    if (_json.Count == 0)
                    {
                        Logger.Instance.log("{0}: saved empty.", GetFilePath());
                    }
                    success = true;
                }
                catch (Exception e)
                {
                    Logger.Instance.error(e.Message);
                }
            }
        }

        public JObject Load(double timeout = 20.0/*sec*/)
        {
            if (!_read)
                return null;
            DateTime _timeout = DateTime.Now.AddSeconds(timeout);
            while (_timeout > DateTime.Now)
            {
                try
                {
                    if (!File.Exists(GetFilePath()))
                        return null;
                    string data = File.ReadAllText(GetFilePath());
                    data = Encription() ? Decrypt(data) : data;
                    _json = JObject.Parse(data);
                    if (_json.Count == 0)
                    {
                        Logger.Instance.log("{0}: loaded empty.", GetFilePath());
                    }
                    return _json;
                }
                catch (Exception e)
                {
                    Logger.Instance.error(e.Message);
                }
            }

            return null;
        }
    }
}
