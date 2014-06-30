using System;
using MonoDevelop.Ide.Gui.Dialogs;
using Gtk;

namespace XamarinStudioGradleExtension
{
	public class ProjectOptionsPanel : ItemOptionsPanel
	{
		ProjectOptionsWidget optionsWidget;
		ProjectGradleInterface projectInterface;

		public ProjectOptionsPanel ()
		{
		}

		public override Widget CreatePanelWidget ()
		{
			optionsWidget = new ProjectOptionsWidget ();
			optionsWidget.Show ();

			projectInterface = new ProjectGradleInterface (this.ConfiguredProject);
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
			var properties = projectInterface.PropertiesForProject ();
			properties.PublishLocalTarget = optionsWidget.PublishLocalTarget;
			properties.PublishTarget = optionsWidget.PublishTarget;
			projectInterface.SaveProject ();
		}
	}
}

