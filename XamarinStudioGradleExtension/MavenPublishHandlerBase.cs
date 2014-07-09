using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using System.Threading;
using MonoDevelop.Projects;


namespace XamarinStudioGradleExtension
{
	abstract class MavenPublishHandlerBase : CommandHandler
	{
		IProgressMonitor mProgressMonitor;
		ProcessWrapper mProcessWrapper;

		public MavenPublishHandlerBase ()
		{
		}

		protected abstract String PublishTarget (ProjectProperties sourceProperties);

		void KillProgressMonitor() {
			mProgressMonitor.EndTask();
			mProgressMonitor.Dispose();
		}

		void HandlePublishComplete (object sender, EventArgs e)
		{
			try {
				if (mProcessWrapper.ExitCode == 0) {
					mProgressMonitor.ReportSuccess ("Publish complete");
				} else
					mProgressMonitor.ReportError ("Publish error", new System.Exception("Publish error"));
			} catch (Exception ex) {
				Console.Out.WriteLine(ex);
			} finally {
				KillProgressMonitor ();
			}
		}

		protected override void Run ()
		{
			Publish (IdeApp.ProjectOperations.CurrentSelectedProject);
		}

		protected override void Update (CommandInfo info)
		{
			if (IdeApp.ProjectOperations.CurrentSelectedProject != null) {
				var gradleInterface = new ProjectGradleInterface (IdeApp.ProjectOperations.CurrentSelectedProject);
				info.Enabled = gradleInterface.PropertiesForProject ().UseGradle;
			} else
				info.Enabled = false;
		}

		public void Publish(Project project)
		{
			var projectInterface = new ProjectGradleInterface (project);
			var projectProperties = projectInterface.PropertiesForProject ();

			mProgressMonitor = IdeApp.Workbench.ProgressMonitors.GetBuildProgressMonitor ();
			mProgressMonitor.BeginTask ("Publishing to Maven", 1);
			mProcessWrapper = MonoDevelop.Core.Runtime.ProcessService.StartProcess ("/usr/local/bin/gradle", PublishTarget(projectProperties), project.BaseDirectory, mProgressMonitor.Log, mProgressMonitor.Log, HandlePublishComplete);
		}
	}
}

