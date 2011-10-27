using System.Collections.Generic;
using System.IO;
using WaveTech.Dafuscator.Model;
using WaveTech.Dafuscator.Model.Interfaces.Providers;
using WaveTech.Dafuscator.Model.Interfaces.Repositories;
using WaveTech.Dafuscator.Repositories.DatabaseProjectRepository.Properties;

namespace WaveTech.Dafuscator.Repositories.DatabaseProjectRepository
{
	public class DatabaseProjectRepository : IDatabaseProjectRepository
	{
		#region Private Readonly Members
		private readonly IObjectSerializationProvider objectSerializationProvider;
		private readonly ISymmetricEncryptionProvider encryptionProvider;
		private readonly EncryptionInfo encryptionInfo;
		#endregion Private Readonly Members

		#region Constructor
		public DatabaseProjectRepository(IObjectSerializationProvider objectSerializationProvider, ISymmetricEncryptionProvider encryptionProvider)
		{
			this.objectSerializationProvider = objectSerializationProvider;
			this.encryptionProvider = encryptionProvider;

			encryptionInfo = new EncryptionInfo();
			encryptionInfo.KeySize = 256;
			encryptionInfo.HashAlgorithm = Resources.EncryptionHashValue;
			encryptionInfo.PassPhrase = Resources.EncryptionPassPhrase;
			encryptionInfo.SaltValue = Resources.EncryptionSaltValue;
			encryptionInfo.Iterations = 5;
			encryptionInfo.InitVector = Resources.EncryptionInitVector;
		}
		#endregion Constructor

		#region Get Database Project
		public Database GetDatabaseProject(string path)
		{
			if (File.Exists(path) == false)
				return null;

			string rawFileData;
			using (StreamReader reader = new StreamReader(path))
			{
				rawFileData = reader.ReadToEnd();
			}

			string plainTextObjectData;
			plainTextObjectData = encryptionProvider.Decrypt(rawFileData, encryptionInfo);

			Database db = objectSerializationProvider.Deserialize<Database>(plainTextObjectData);

			return db;
		}
		#endregion Get Database Project

		#region Save Database Project
		public Database SaveDatabaseProject(Database databaseProject, string path)
		{
			if (File.Exists(path))
				File.Delete(path);

			// Clear out all the generated data, so we don't serialize that out
			foreach (Table t in databaseProject.Tables)
				foreach (Column c in t.Columns)
					c.Data = new List<string>();

			string plainTextObjectData;
			plainTextObjectData = objectSerializationProvider.Serialize(databaseProject);

			string encryptedObjectData;
			encryptedObjectData = encryptionProvider.Encrypt(plainTextObjectData, encryptionInfo);

			using (StreamWriter writer = new StreamWriter(path))
			{
				writer.Write(encryptedObjectData);
			}

			return GetDatabaseProject(path);
		}
		#endregion Save Client License
	}
}