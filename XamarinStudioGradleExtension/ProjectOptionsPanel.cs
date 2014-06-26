using System;
using MonoDevelop.Ide.Gui.Dialogs;
using Gtk;

namespace XamarinStudioGradleExtension
{
	public class ProjectOptionsPanel : ItemOptionsPanel
	{
		ProjectOptionsWidget optionsWidget;

		public ProjectOptionsPanel ()
		{
		}

		public override Widget CreatePanelWidget ()
		{
			optionsWidget = new ProjectOptionsWidget ();
			optionsWidget.Show ();

			var projectInterface = new ProjectGradleInterface (this.ConfiguredProject);
			var properties = projectInterface.PropertiesForProject ();
			optionsWidget.PublishLocalTarget = properties.PublishLocalTarget;
			optionsWidget.PublishTarget = properties.PublishTarget;

			return optionsWidget;
		}

		public override bool ValidateChanges()
		{
			return true;
		}

		public override void ApplyChanges()
		{

		}

//		public override void LoadConfigData ()
//		{
//			var projectInterface = new ProjectGradleInterface (this.ConfiguredProject);
//			var projectSettings = projectInterface.PropertiesForProject ();
//			optionsWidget.UseGradle = projectSettings.UseGradle;
//			optionsWidget.PublishLocalTarget = projectSettings.PublishLocalTarget;
//			optionsWidget.PublishTarget = projectSettings.PublishTarget;

//			var configs = this.CurrentConfigurations;
//			if (configs.Length == 1) {
//				var configSettings = projectInterface.PropertiesForConfiguration (configs [0]);
//				optionsWidget.FetchTarget = configSettings.FetchDependenciesTarget;
//			} else {
//			}
		//}
	}
}

