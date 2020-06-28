using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;

namespace PDNPresets
{
	public class PDNPresetsConfigToken : EffectConfigToken
	{
		internal List<Pair<Effect, EffectConfigToken>> effects { get; set; }

		internal PDNPresetsConfigToken()
		{
			this.effects = new List<Pair<Effect, EffectConfigToken>>();
		}

		private PDNPresetsConfigToken(PDNPresetsConfigToken copyMe)
		{
			this.effects = new List<Pair<Effect, EffectConfigToken>>(copyMe.effects);
		}

		public override object Clone()
		{
			return new PDNPresetsConfigToken(this);
		}
	}
}
