using System;
using MonoDevelop.Ide.Gui.Dialogs;
using Gtk;

namespace XamarinStudioGradleExtension
{
	public class GradleOptionsPanel : ItemOptionsPanel
	{
		GradleOptionsWidget optionsWidget;
		ProjectGradleInterface projectInterface;

		public GradleOptionsPanel ()
		{
		}

		public override Widget CreatePanelWidget ()
		{
			optionsWidget = new GradleOptionsWidget ();
			optionsWidget.Show ();

			projectInterface = new ProjectGradleInterface (this.ConfiguredProject);
			optionsWidget.UseGradle = projectInterface.PropertiesForProject ().UseGradle;

			return optionsWidget;
		}

		public override bool ValidateChanges()
		{
			return true;
		}

		public override void ApplyChanges()
		{
			projectInterface.PropertiesForProject ().UseGradle = optionsWidget.UseGradle;
			projectInterface.SaveProject ();
		}
	}
}

