namespace WaveTech.Scutex.Manager
{
	internal static class MasterHooker
	{

		//#if CE
		internal static void HookDemoGenerators()
		{
			Dafuscator.Generators.HookerDemo.HookDemoGenerators();
		}
		//#else
		internal static void HookGenerators()
		{
			Dafuscator.Generators.Hooker.HookGenerators();
		}
		//#endif
	}
}