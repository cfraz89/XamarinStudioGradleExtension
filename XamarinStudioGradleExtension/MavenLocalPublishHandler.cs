using System;

namespace XamarinStudioGradleExtension
{
	class MavenLocalPublishHandler : MavenPublishHandlerBase
	{
		public MavenLocalPublishHandler ()
		{
		}

		protected override string PublishTarget(ProjectProperties sourceProperties)
		{
			return sourceProperties.PublishLocalTarget;
		}
	}
}

