using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IGeneratorInfo
	{
		Guid Id { get; }
		string Name { get; }
		Type Type { get; }
		List<OleDbType> CompatibleDataTypes { get; }
	}
}