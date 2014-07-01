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
			var result = new BuildResult ();
			//mdtool is derp, and it will still apply this plugin. That will cause a recursive compile. We don't want that.
			if (monitor.GetType() != typeof(MonoDevelop.Core.ProgressMonitoring.ConsoleProgressMonitor)) {
				ItemConfiguration itemConfig = configuration.GetConfiguration (item);
				var projectGradleInterface = new ProjectGradleInterface (item as Project);
				try {
					if (projectGradleInterface.PropertiesForProject ().UseGradle && itemConfig != null)
						result.Append (new GradleBuilder (monitor, item, configuration).Build ());
				} catch (Exception e) {
				} finally {
					result.Append (base.Build (monitor, item, configuration));
				}
			} else
				result = base.Build (monitor, item, configuration);

			return result;
		}
	}
}

