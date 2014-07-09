using System;
using MonoDevelop.Core.Serialization;

namespace XamarinStudioGradleExtension
{
	public class GlobalProperties
	{
		public static String GLOBAL_PROPERTIES_PATH = "XamarinStudioGradleExtension.GlobalProperties";

		[ItemProperty]
		public string GradleCommand { get; set; }

		[ItemProperty]
		public bool ConfigureOnDemand { get; set; }

		public GlobalProperties ()
		{
		}

		public GlobalProperties WithDefaultValues()
		{
			GradleCommand = "/usr/local/bin/gradle";
			ConfigureOnDemand = false;
			return this;
		}
	}
}

