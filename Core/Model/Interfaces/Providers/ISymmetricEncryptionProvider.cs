namespace WaveTech.Dafuscator.Model.Interfaces.Providers
{
	public interface ISymmetricEncryptionProvider
	{
		string Encrypt(string plainText, EncryptionInfo info);
		string Decrypt(string cipherText, EncryptionInfo info);
	}
}