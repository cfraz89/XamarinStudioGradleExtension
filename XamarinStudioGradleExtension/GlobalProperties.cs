using System;
using MonoDevelop.Core.Serialization;

namespace XamarinStudioGradleExtension
{
	public class GlobalProperties
	{
		[ItemProperty]
		public string GradleCommand { get; set; }

		public GlobalProperties ()
		{
		}

		public GlobalProperties WithDefaultValues()
		{
			GradleCommand = "/usr/local/bin/gradle";
			return this;
		}
	}
}

