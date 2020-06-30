using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;

namespace PDNPresets
{
	public class PDNPresetsConfigToken : EffectConfigToken
	{
		internal List<string> names { get; set; }
		internal List<Effect> effects { get; set; }
		internal List<EffectConfigDialog> dialogs { get; set; }

		internal PDNPresetsConfigToken()
		{
			this.names = new List<string>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
		}

		private PDNPresetsConfigToken(PDNPresetsConfigToken copyMe)
		{
			this.names = new List<string>(copyMe.names);
			this.effects = new List<Effect>(copyMe.effects);
			this.dialogs = new List<EffectConfigDialog>(copyMe.dialogs);
		}

		public override object Clone()
		{
			return new PDNPresetsConfigToken(this);
		}
	}
}
