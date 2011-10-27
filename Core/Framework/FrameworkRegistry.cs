using StructureMap.Configuration.DSL;
using WaveTech.Dafuscator.Model.Interfaces.Framework;

namespace WaveTech.Dafuscator.Framework
{
	internal class FrameworkRegistry : Registry
	{
		public FrameworkRegistry()
		{
			For<IEventAggregator>().Singleton().Use<EventAggregator>();
		}
	}
}