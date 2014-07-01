using System;

namespace XamarinStudioGradleExtension
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ConfigurationOptionsWidget : Gtk.Bin
	{
		public ConfigurationOptionsWidget ()
		{
			this.Build ();
		}

		public string FetchTarget {
			get {
				return fetchTargetEntry.Text;
			} set {
				fetchTargetEntry.Text = value;
			}
		}
	}
}

