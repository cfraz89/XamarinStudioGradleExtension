using System;

namespace XamarinStudioGradleExtension
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class GradleOptionsWidget : Gtk.Bin
	{
		public GradleOptionsWidget ()
		{
			this.Build ();
		}

		public bool UseGradle {
			get {
				return useGradleCheckBox.Active;
			} set {
				useGradleCheckBox.Active = value;
			}
		}
	}
}

