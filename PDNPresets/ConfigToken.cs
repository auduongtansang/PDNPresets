using PaintDotNet.Effects;
using PaintDotNet.PropertySystem;
using System.Collections.Generic;

namespace PDNPresets
{
	public class PDNPresetsConfigToken : EffectConfigToken
	{
		internal List<int> types { get; set; }
		internal List<Effect> effects { get; set; }
		internal List<EffectConfigDialog> dialogs { get; set; }
		internal List<PropertyCollection> collections { get; set; }

		internal PDNPresetsConfigToken()
		{
			this.types = new List<int>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			this.collections = new List<PropertyCollection>();
		}

		private PDNPresetsConfigToken(PDNPresetsConfigToken copyMe)
		{
			this.types = new List<int>(copyMe.types);
			this.effects = new List<Effect>(copyMe.effects);
			this.dialogs = new List<EffectConfigDialog>(copyMe.dialogs);
			this.collections = new List<PropertyCollection>(copyMe.collections);
		}

		public override object Clone()
		{
			return new PDNPresetsConfigToken(this);
		}
	}
}
