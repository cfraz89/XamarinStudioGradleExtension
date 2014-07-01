using System;
using MonoDevelop.Ide.Gui.Dialogs;
using Gtk;

namespace XamarinStudioGradleExtension
{
	public class ConfigurationOptionsPanel : MultiConfigItemOptionsPanel
	{
		ConfigurationOptionsWidget optionsWidget;
		ProjectGradleInterface projectInterface;

		public ConfigurationOptionsPanel ()
		{
		}

		public override Widget CreatePanelWidget ()
		{
			projectInterface = new ProjectGradleInterface (this.ConfiguredProject);

			optionsWidget = new ConfigurationOptionsWidget ();
			optionsWidget.Show ();
			return optionsWidget;
		}

		public override bool ValidateChanges()
		{
			return true;
		}

		public override void ApplyChanges()
		{
			var configSettings = projectInterface.PropertiesForConfiguration (this.CurrentConfiguration);
			configSettings.FetchDependenciesTarget = optionsWidget.FetchTarget;
			projectInterface.SaveProject ();
		}

		public override void LoadConfigData ()
		{
			var configSettings = projectInterface.PropertiesForConfiguration (this.CurrentConfiguration);
			optionsWidget.FetchTarget = configSettings.FetchDependenciesTarget;
		}
	}
}

