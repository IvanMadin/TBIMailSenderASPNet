using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Providers
{
    public class EncryptingHelper
    {
        public string DecryptingData(string dataToDecrypt)
        {
            String codedBody = dataToDecrypt.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");
            byte[] data = Convert.FromBase64String(codedBody);
            var result = Encoding.UTF8.GetString(data);

            return result;
        }
    }
}
