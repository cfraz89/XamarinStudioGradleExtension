using System;
using MonoDevelop.Projects;
using System.IO;
using MonoDevelop.Ide;

namespace XamarinStudioGradleExtension
{
	public class ProjectGradleInterface
	{
		Project mProject;
		String mGradleBaseDir;

		public ProjectGradleInterface (Project project)
		{
			mProject = project;
		}

		string DetectBaseDir ()
		{
			string baseDir = null;
			if (File.Exists (Path.Combine (mProject.BaseDirectory, "build.gradle")))
				baseDir = mProject.BaseDirectory;
			else if (mProject.ParentSolution != null) {
				if (File.Exists (Path.Combine (mProject.ParentSolution.BaseDirectory, "build.gradle")))
					baseDir = mProject.ParentSolution.BaseDirectory;
			}
			return baseDir;
		}

		public String GradleBaseDir {
			get {
				if (mGradleBaseDir == null) {
					mGradleBaseDir = DetectBaseDir ();
				}
				return mGradleBaseDir;
			}
		}

		public bool DetectedGradleFile {
			get {
				return GradleBaseDir != null;
			}
		}

		public ProjectProperties PropertiesForProject()
		{
			var properties = mProject.ExtendedProperties ["XamarinStudioGradleExtension.ProjectProperties"] as ProjectProperties;
			if (properties == null) {
				properties = new ProjectProperties ().WithDefaultSettings();
				properties.UseGradle = DetectedGradleFile;
				mProject.ExtendedProperties ["XamarinStudioGradleExtension.ProjectProperties"] = properties;
				SaveProject ();
			}
			return properties;
		}

		public ConfigurationProperties PropertiesForConfiguration(ItemConfiguration configuration)
		{
			var properties = configuration.ExtendedProperties ["XamarinStudioGradleExtension.ConfigurationProperties"] as ConfigurationProperties;
			if (properties == null) {
				properties = new ConfigurationProperties ().WithDefaultSettings(configuration);
				configuration.ExtendedProperties ["XamarinStudioGradleExtension.ConfigurationProperties"] = properties;
				SaveProject ();
			}
			return properties;
		}

		public void SaveProject() {
			var saveMonitor = IdeApp.Workbench.ProgressMonitors.GetSaveProgressMonitor (false);
			mProject.Save (saveMonitor);
			saveMonitor.EndTask ();
			saveMonitor.Dispose ();
		}
	}
}

