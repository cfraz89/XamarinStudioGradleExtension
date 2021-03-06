﻿using System;

namespace XamarinStudioGradleExtension
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class GlobalGradleOptionsWidget : Gtk.Bin
	{
		public GlobalGradleOptionsWidget ()
		{
			this.Build ();
		}

		public string GradleCommand {
			get {
				return commandEntry.Text;
			}
			set {
				commandEntry.Text = value;
			}
		}

		public bool ConfigureOnDemand {
			get {
				return configureOnDemandCheckbox.Active;
			}
			set {
				configureOnDemandCheckbox.Active = value;
			}
		}
	}
}

