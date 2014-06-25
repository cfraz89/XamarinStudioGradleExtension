using System;
using MonoDevelop.Core.Serialization;
using MonoDevelop.Projects;

namespace XamarinStudioGradleExtension
{
	public class ConfigurationProperties
	{
		[ItemProperty]
		public String FetchDependenciesTarget { get; set; }

		public ConfigurationProperties ()
		{
		}

		public ConfigurationProperties WithDefaultSettings(ItemConfiguration configuration)
		{
			FetchDependenciesTarget = "fetchXamarinDependencies-" + configuration.Name;
			return this;
		}
	}
}

