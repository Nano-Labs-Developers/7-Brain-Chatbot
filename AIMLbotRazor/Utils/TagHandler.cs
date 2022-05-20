using System.Collections.Generic;
using System.Reflection;
using AIMLbotRazor.Utils;

namespace AIMLbotRazor.Utils
{
	public class TagHandler
    {
		public string AssemblyName;

		public string ClassName;

		public string TagName;

		public AIMLTagHandler Instantiate(Dictionary<string, Assembly> Assemblies)
		{
			if (Assemblies.ContainsKey(AssemblyName))
			{
				Assembly assembly = Assemblies[AssemblyName];
				assembly.GetTypes();
				return (AIMLTagHandler)assembly.CreateInstance(ClassName);
			}
			return null;
		}
	}
}
