using System;

namespace XamarinStudioGradleExtension
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class ProjectOptionsWidget : Gtk.Bin
	{
		public ProjectOptionsWidget ()
		{
			this.Build ();
		}

		public string PublishLocalTarget {
			get {
				return publishLocalTargetEntry.Text;
			} set {
				publishLocalTargetEntry.Text = value;
			}
		}

		public string PublishTarget {
			get {
				return publishTargetEntry.Text;
			} set {
				publishTargetEntry.Text = value;
			}
		}
	}
}

