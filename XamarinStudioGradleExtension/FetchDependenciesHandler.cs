﻿using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using System.Threading;
using MonoDevelop.Projects;


namespace XamarinStudioGradleExtension
{
	class FetchDependenciesHandler : CommandHandler
	{
		IProgressMonitor mProgressMonitor;
		ProcessWrapper mProcessWrapper;

		public FetchDependenciesHandler ()
		{
		}

		void HandleProcessComplete (object sender, EventArgs e)
		{
			try {
				if (mProcessWrapper.ExitCode == 0)
					mProgressMonitor.ReportSuccess ("Fetch complete");
				else
					mProgressMonitor.ReportError ("Fetch error", new System.Exception("Fetch error"));
			} catch (Exception ex) {
				Console.Out.WriteLine(ex);
			} finally {
				mProgressMonitor.EndTask();
				mProgressMonitor.Dispose();
			}
		}

		protected override void Update (CommandInfo info)
		{
			var gradleInterface = new ProjectGradleInterface (IdeApp.ProjectOperations.CurrentSelectedProject);
			info.Enabled = gradleInterface.DetectedGradleFile;
		}

		protected override void Run ()
		{
			var project = IdeApp.ProjectOperations.CurrentSelectedProject;
			FetchDependencies (project, IdeApp.Workspace.ActiveConfiguration.GetConfiguration(project));
		}

		public void FetchDependencies(Project project, ItemConfiguration configuration)
		{
			var projectInterface = new ProjectGradleInterface (project);
			var projectProperties = projectInterface.PropertiesForProject ();
			var configProperties = projectInterface.PropertiesForConfiguration (configuration);

			mProgressMonitor = IdeApp.Workbench.ProgressMonitors.GetBuildProgressMonitor ();
			mProgressMonitor.BeginTask ("Fetch Dependencies", 1);
			mProcessWrapper = MonoDevelop.Core.Runtime.ProcessService.StartProcess ("/usr/local/bin/gradle", configProperties.FetchDependenciesTarget, project.BaseDirectory, mProgressMonitor.Log, mProgressMonitor.Log, HandleProcessComplete);
		}
	}
}

