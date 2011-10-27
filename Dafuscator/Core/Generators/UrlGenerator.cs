using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using WaveTech.Dafuscator.Framework;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class UrlGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("749DD4BD-69B5-4C72-93CA-D72D6188791B"); }
		}

		public string Name
		{
			get { return "Url Generator"; }
		}

		public Type Type
		{
			get { return typeof(IUrlGenerator); }
		}

		public List<OleDbType> CompatibleDataTypes
		{
			get
			{
				return new List<OleDbType>
				       	{
									OleDbType.LongVarChar,
									OleDbType.LongVarWChar,
									OleDbType.VarChar,
									OleDbType.VarWChar
				       	};
			}
		}
	}

	public class UrlGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("749DD4BD-69B5-4C72-93CA-D72D6188791B"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
			{
				bool data2 = false;

				try
				{
					data2 = (bool)data[1];
				}
				catch { }

				generatedData = ((IUrlGenerator)generator).GenerateUrls((double)data[0], data2, existingColumnData);
			}

			return generatedData;
		}
	}

	public class UrlGenerator : IUrlGenerator
	{
		private readonly IStringGenerator stringGenerator;
		private readonly ICompanyNameGenerator companyNameGenerator;

		public UrlGenerator(IStringGenerator stringGenerator, ICompanyNameGenerator companyNameGenerator)
		{
			this.stringGenerator = stringGenerator;
			this.companyNameGenerator = companyNameGenerator;
		}

		public string GenerateUrl(bool includePrefix)
		{
			StringBuilder url = new StringBuilder();

			if (includePrefix)
				url.Append("http://");

			if (DateTime.Now.Second % 3 != 0)
				url.Append("www.");

			//url.Append(stringGenerator.GenerateRandomString(3, 25, false, false));
			url.Append(companyNameGenerator.GenerateCompanyNameWithoutSuffix());

			Thread.Sleep(250);

			if (DateTime.Now.Second % 3 == 0)
			{
				url.Append(".org");
			}
			else if (DateTime.Now.Second % 2 == 0)
			{
				url.Append(".net");
			}
			else
			{
				url.Append(".com");
			}

			return url.ToString();
		}

		public List<string> GenerateUrls(double count, bool includePrefix, HashSet<string> existingColumnData)
		{
			HashSet<string> urls = new HashSet<string>();

			while (urls.Count < count)
			{
				string url = GenerateUrl(includePrefix);

				if (urls.Contains(url, new StringUtilities.IgnoreCaseStringComparer()) == false &&
						existingColumnData.Contains(url, new StringUtilities.IgnoreCaseStringComparer()) == false)
				{
					urls.Add(url);
				}
			}

			return urls.ToList();
		}
	}
}
