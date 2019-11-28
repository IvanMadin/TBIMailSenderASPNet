namespace EmailManager.Service.Contracts
{
    public interface IEncryptingHelper
    {
        string Decrypt(string cipherText);
        string DecryptingBase64Data(string dataToDecrypt);
        string Encrypt(string clearText);
    }
}