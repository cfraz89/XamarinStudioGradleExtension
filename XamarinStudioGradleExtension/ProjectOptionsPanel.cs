using System;
using MonoDevelop.Ide.Gui.Dialogs;
using Gtk;

namespace XamarinStudioGradleExtension
{
	public class ProjectOptionsPanel : MultiConfigItemOptionsPanel
	{
		Gtk.Entry entry;

		public ProjectOptionsPanel ()
		{
		}

		public override Widget CreatePanelWidget ()
		{
			// Create the widget and initialize it
			entry = new Gtk.Entry ();
			entry.ShowAll ();
			entry.Text = ConfiguredProject.Name;
			return entry;
		}

		public override bool ValidateChanges()
		{
			// Don't allow empty project names
			return entry.Text.Length > 0;
		}

		public override void ApplyChanges()
		{
			// Save the changes
			ConfiguredProject.Name = entry.Text;
		}

		public override void LoadConfigData ()
		{

		}
	}
}

