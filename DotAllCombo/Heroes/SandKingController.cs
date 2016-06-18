namespace DotAllCombo.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Ensage;
    using Ensage.Common;
    using Ensage.Common.Extensions;
    using Ensage.Common.Menu;
	using SharpDX;
	using Extensions;

	internal class SandKingController : HeroController
	{

		public override void OnInject()
		{
			DebugExtensions.Chat.PrintError("This Hero Not Supported");

		}
	}
}