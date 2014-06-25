using System;
using MonoDevelop.Core.Serialization;
using MonoDevelop.Projects;
using MonoDevelop.Ide;

namespace XamarinStudioGradleExtension
{
	[DataItem(Name="Props")]
	public class ProjectProperties
	{
		[ItemProperty]
		public bool UseGradle { get; set;}
		[ItemProperty]
		public String PublishLocalTarget { get; set; }
		[ItemProperty]
		public String PublishTarget { get; set; }

		public ProjectProperties()
		{
		}

		public ProjectProperties WithDefaultSettings()
		{
			PublishLocalTarget = "publishToMavenLocal";
			PublishTarget = "publish";
			return this;
		}
	}
}

