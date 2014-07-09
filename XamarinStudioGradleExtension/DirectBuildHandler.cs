using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using System.Threading;
using MonoDevelop.Projects;


namespace XamarinStudioGradleExtension
{
	class DirectBuildHandler : CommandHandler
	{
		IProgressMonitor mProgressMonitor;
		GradleBuilder builder;

		public DirectBuildHandler ()
		{
		}

		void HandleProcessComplete (object sender, EventArgs args)
		{
			try {
				var errorsPad = IdeApp.Workbench.Pads.ErrorsPad;
				//var content = errorsPad.Content as MonoDevelop.Ide.Gui.Pads.ErrorListPad;
				//foreach (var error in builder.BuildResult.Errors)
				//	content.AddTask(new MonoDevelop.Ide.Tasks.Task(error));

				if (builder.BuildResult.Errors.Count == 0)
					mProgressMonitor.ReportSuccess ("Build complete");
				else
					mProgressMonitor.ReportError ("Build error", new System.Exception("Build error"));
			} catch (Exception ex) {
				Console.Out.WriteLine(ex);
			} finally {
				mProgressMonitor.EndTask();
				mProgressMonitor.Dispose();
			}
		}

		protected override void Update (CommandInfo info)
		{
			if (IdeApp.ProjectOperations.CurrentSelectedProject != null) {
				var gradleInterface = new ProjectGradleInterface (IdeApp.ProjectOperations.CurrentSelectedProject);
				info.Enabled = gradleInterface.PropertiesForProject ().UseGradle;
			} else
				info.Enabled = false;
		}

		protected override void Run ()
		{
			var project = IdeApp.ProjectOperations.CurrentSelectedProject;
			Build (project, IdeApp.Workspace.ActiveConfiguration.GetConfiguration(project));
		}

		public GlobalProperties FetchProperties()
		{
			return PropertyService.Get (GlobalProperties.GLOBAL_PROPERTIES_PATH, new GlobalProperties ().WithDefaultValues ());
		}

		public void Build(Project project, ItemConfiguration configuration)
		{
			var projectInterface = new ProjectGradleInterface (project);
			var projectProperties = projectInterface.PropertiesForProject ();
			var configProperties = projectInterface.PropertiesForConfiguration (configuration);

			mProgressMonitor = IdeApp.Workbench.ProgressMonitors.GetBuildProgressMonitor ();
			//mProgressMonitor.BeginTask ("Gradle build", 1);
			builder = new GradleBuilder (mProgressMonitor, project, configuration.Selector);
			builder.Build (false, HandleProcessComplete);
			//HandleProcessComplete (this, "");
		}
	}
}

