using System;
using System.Collections.Generic;

namespace WaveTech.Dafuscator.Model.Interfaces.Generators
{
	public interface IGeneratorBuilder
	{
		Guid GeneratorId { get; }
		List<string> BuildGenerator(object generator, object[] data, HashSet<string> existingColumnData);
	}
}