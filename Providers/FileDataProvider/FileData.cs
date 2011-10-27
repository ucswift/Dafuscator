using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WaveTech.Dafuscator.Model.Interfaces.Providers;

namespace WaveTech.Dafuscator.Providers.FileDataProvider
{
	public class FileData : IFileDataProvider
	{
		public List<string> GetDataFromEmbededFile(Assembly assembly, string name)
		{
			if (assembly == null)
				throw new ArgumentNullException("assembly");

			if (String.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

// ReSharper disable AssignNullToNotNullAttribute
			StreamReader textStreamReader = new StreamReader(assembly.GetManifestResourceStream(name));
// ReSharper restore AssignNullToNotNullAttribute

			string allText = textStreamReader.ReadToEnd().Replace("\r\n", "\n").Replace("\n\r", "\n");
			string[] lines = allText.Split(new char[] { '\n' });
			textStreamReader.Close();

			return lines.ToList();
		}
	}
}