using System;
using MonoDevelop.Projects;
using MonoDevelop.Ide;
using System.CodeDom.Compiler;

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
			if (IdeApp.IsInitialized) {
				ItemConfiguration itemConfig = configuration.GetConfiguration (item);
				var projectGradleInterface = new ProjectGradleInterface (item as Project);
				try {
					if (projectGradleInterface.PropertiesForProject ().UseGradle && itemConfig != null) {
						//Now gradle will skip the build command itself for if it's an mdtool project
						result.Append (new GradleBuilder (monitor, item, configuration).Build (true, null));
						//so we can do it here
						if (item.UseMSBuildEngine == null) 
							result.Append (base.Build (monitor, item, configuration));
					} else
						result.Append (base.Build (monitor, item, configuration));
				} catch (Exception e) {
				}
			} else
				result.Append(base.Build (monitor, item, configuration));

			return result;
		}
	}
}

