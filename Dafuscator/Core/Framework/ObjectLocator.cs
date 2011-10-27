using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace WaveTech.Dafuscator.Framework
{
	public static class ObjectLocator
	{
		public static bool IsInitialized { get; set; }

		private static void Initialize()
		{
			if (!IsInitialized)
			{
				Bootstrapper.Configure();
				IsInitialized = true;
			}
		}

		public static T GetInstance<T>()
		{
			Initialize();
			return ObjectFactory.GetInstance<T>();
		}

		public static T GetInstance<T>(string name)
		{
			Initialize();
			return ObjectFactory.GetNamedInstance<T>(name);
		}

		public static object GetInstance(Type type)
		{
			Initialize();
			return ObjectFactory.GetInstance(type);
		}

		public static List<T> GetAllInstances<T>()
		{
			Initialize();
			return ObjectFactory.GetAllInstances<T>().ToList();
		}

		public static List<object> GetAllInstances(Type type)
		{
			Initialize();
			IList instances = ObjectFactory.GetAllInstances(type);

			var objects = new List<object>();
			foreach (object o in instances)
			{
				objects.Add(o);
			}

			return objects;
		}
	}
}