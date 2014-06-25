using System;

namespace XamarinStudioGradleExtension
{
	class MavenPublishHandler : MavenPublishHandlerBase
	{
		public MavenPublishHandler ()
		{
		}

		protected override string PublishTarget(ProjectProperties sourceProperties)
		{
			return sourceProperties.PublishTarget;
		}
	}
}

