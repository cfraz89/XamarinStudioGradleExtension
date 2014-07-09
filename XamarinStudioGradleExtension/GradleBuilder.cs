using System;
using MonoDevelop.Projects;
using MonoDevelop.Core;
using MonoDevelop.Core.Execution;
using MonoDevelop.IPhone;

namespace XamarinStudioGradleExtension
{
	public class GradleBuilder
	{
		IProgressMonitor mProgressMonitor;
		SolutionEntityItem mItem;
		ConfigurationSelector mConfiguration;
		BuildResult mBuildResult;

		public GradleBuilder (IProgressMonitor monitor, SolutionEntityItem item, ConfigurationSelector configuration)
		{
			mProgressMonitor = monitor;
			mItem = item;
			mConfiguration = configuration;
		}

		void HandleBuildComplete(object sender, EventArgs args)
		{
		}

		void BuildOutputChanged(object sender, string buildOutput)
		{
			mProgressMonitor.Log.Write (buildOutput);
			var outputLines = buildOutput.Split ('\n');
			foreach (var line in outputLines) {
				var error = BuildError.FromMSBuildErrorFormat (line);
				if (error != null) {
					error.FileName = mItem.BaseDirectory.Combine (error.FileName);
					mBuildResult.Append (error);
				}
			}
		}

		void ErrorOutputChanged(object sender, string buildOutput)
		{
			mProgressMonitor.Log.Write (buildOutput);
			mBuildResult.AddError (buildOutput);
		}



		public GlobalProperties FetchProperties()
		{
			return PropertyService.Get (GlobalProperties.GLOBAL_PROPERTIES_PATH, new GlobalProperties ().WithDefaultValues ());
		}

		public BuildResult Build(bool fromIde, EventHandler exited)
		{
			var configName = GetConfigurationString ();
			mBuildResult = new BuildResult ();
			mBuildResult.SourceTarget = mItem;

			var properties = FetchProperties ();
			var flags = properties.ConfigureOnDemand ? "--configure-on-demand " : "";
			flags += fromIde ? "-Pide" : "";
			var processWrapper = Runtime.ProcessService.StartProcess (properties.GradleCommand, flags + " build" + configName, mItem.BaseDirectory, BuildOutputChanged, ErrorOutputChanged, exited);
			processWrapper.WaitForExit ();
			//var errorOutput = processWrapper.StandardError.ReadToEnd();
			//if (errorOutput.Length > 0) {
			//	mBuildResult.AddError (errorOutput);
			//}
			return mBuildResult;
		}

		String GetConfigurationString ()
		{
			var configuration = mConfiguration.GetConfiguration (mItem);
			String configurationName = configuration.Name;
			var iPhoneProject = mItem as IPhoneProject;
			if (iPhoneProject != null) {
				var iPhoneConfiguration = iPhoneProject.GetConfiguration (mConfiguration) as IPhoneProjectConfiguration;
				if (iPhoneConfiguration != null) {
					if (iPhoneConfiguration.IsSimPlatform)
						configurationName += "iPhoneSimulator";
					else if (iPhoneConfiguration.IsDevicePlatform)
						configurationName += "iPhone";
				}
			}
			return configurationName;
		}

		public BuildResult BuildResult {
			get {
				return mBuildResult;
			}
		}
	}
}

