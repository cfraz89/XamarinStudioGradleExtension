using System;
using MonoDevelop.Ide.Gui.Dialogs;
using MonoDevelop.Ide;
using MonoDevelop.Core;

namespace XamarinStudioGradleExtension
{
	public class GlobalGradleOptionsPanel : OptionsPanel
	{
		GlobalGradleOptionsWidget widget;
		public GlobalGradleOptionsPanel ()
		{
		}

		public override Gtk.Widget CreatePanelWidget ()
		{
			var properties = FetchProperties ();
			widget = new GlobalGradleOptionsWidget ();
			widget.GradleCommand = properties.GradleCommand;
			widget.ConfigureOnDemand = properties.ConfigureOnDemand;
			return widget;
		}

		public GlobalProperties FetchProperties()
		{
			return PropertyService.Get (GlobalProperties.GLOBAL_PROPERTIES_PATH, new GlobalProperties ().WithDefaultValues ());
		}

		public override void ApplyChanges ()
		{
			var properties = FetchProperties ();
			properties.GradleCommand = widget.GradleCommand;
			properties.ConfigureOnDemand = widget.ConfigureOnDemand;
		}
	}
}

