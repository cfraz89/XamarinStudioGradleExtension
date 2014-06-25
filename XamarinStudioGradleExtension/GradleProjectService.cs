using System;
using MonoDevelop.Projects;

namespace XamarinStudioGradleExtension
{
	public class GradleProjectService : ProjectServiceExtension
	{
		public GradleProjectService ()
		{
		}


		protected override BuildResult Build (MonoDevelop.Core.IProgressMonitor monitor, SolutionEntityItem item, ConfigurationSelector configuration)
		{
			ItemConfiguration itemConfig = configuration.GetConfiguration (item);
			var projectGradleInterface = new ProjectGradleInterface (item as Project);

			var result = new BuildResult();
			if (projectGradleInterface.DetectedGradleFile && itemConfig != null)
				result.Append(new GradleBuilder (monitor, item, configuration).Build ());

			result.Append(base.Build (monitor, item, configuration));

			return result;
		}
	}
}

