using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Generators;

namespace WaveTech.Dafuscator.Generators
{
	internal class DemoGeneratorRegistry : Registry
	{
		public DemoGeneratorRegistry()
		{
			For<INumberGenerator>().Use<NumberGenerator>();
			For<IStringGenerator>().Use<StringGenerator>();
			For<IClearGenerator>().Use<ClearGenerator>();
			For<INoneGenerator>().Use<NoneGenerator>();

			For<IGeneratorInfo>().Add<NumberGeneratorInfo>();
			For<IGeneratorInfo>().Add<StringGeneratorInfo>();
			For<IGeneratorInfo>().Add<ClearGeneratorInfo>();
			For<IGeneratorInfo>().Add<NoneGeneratorInfo>();

			//For<IGeneratorInfo>()
			//      .AddInstances(x =>
			//      {
			//        x.Is.OfConcreteType<NumberGeneratorInfo>();
			//        x.Is.OfConcreteType<StringGeneratorInfo>();
			//        x.Is.OfConcreteType<ClearGeneratorInfo>();
			//      });

			For<IGeneratorBuilder>().Add<NumberGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<StringGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<ClearGeneratorBuilder>();
			For<IGeneratorBuilder>().Add<NoneGeneratorBuilder>();

			//For<IGeneratorBuilder>()
			//      .AddInstances(x =>
			//      {
			//        x.Is.OfConcreteType<ClearGeneratorBuilder>();
			//        x.Is.OfConcreteType<NumberGeneratorBuilder>();
			//        x.Is.OfConcreteType<StringGeneratorBuilder>();
			//      });
		}
	}
}