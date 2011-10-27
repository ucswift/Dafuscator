using System;
using System.Collections.Generic;
using System.Data.OleDb;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	public class GuidGeneratorInfo : IGeneratorInfo
	{
		public Guid Id
		{
			get { return new Guid("2A30ED0A-B1C3-4BFF-87B3-DE51161995D3"); }
		}

		public string Name
		{
			get { return "Guid Generator"; }
		}

		public Type Type
		{
			get { return typeof(IGuidGenerator); }
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
									OleDbType.VarWChar,
									OleDbType.Guid
				       	};
			}
		}
	}

	public class GuidGeneratorBuilder : IGeneratorBuilder
	{
		public Guid GeneratorId
		{
			get { return new Guid("2A30ED0A-B1C3-4BFF-87B3-DE51161995D3"); }
		}

		public List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData)
		{
			List<string> generatedData = null;

			if (generator != null && data.Length >= 1)
				generatedData = ((IGuidGenerator)generator).GenerateGuids((double)data[0]);

			return generatedData;
		}
	}

	public class GuidGenerator : IGuidGenerator
	{
		public string GenerateGuid()
		{
			return Guid.NewGuid().ToString();
		}

		public List<string> GenerateGuids(double count)
		{
			List<string> guids = new List<string>();

			for (double i = 0; i < count; i++)
			{
				guids.Add(GenerateGuid());
			}

			return guids;
		}
	}
}