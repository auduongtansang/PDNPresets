using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;

namespace PDNPresets
{
	internal partial class PDNPresetsConfigDialog : EffectConfigDialog
	{
		private List<Pair<Effect, EffectConfigToken>> effects;

		public PDNPresetsConfigDialog()
		{
			InitializeComponent();
			this.effects = new List<Pair<Effect, EffectConfigToken>>();
			FinishTokenUpdate();
		}

		protected override void InitialInitToken()
		{
			theEffectToken = new PDNPresetsConfigToken();
		}

		protected override void InitTokenFromDialog()
		{
			PDNPresetsConfigToken token = (PDNPresetsConfigToken)EffectToken;
			token.effects = new List<PaintDotNet.Pair<Effect, EffectConfigToken>>(this.effects);
		}

		protected override void InitDialogFromToken(EffectConfigToken effectTokenCopy)
		{
			this.effects = new List<Pair<Effect, EffectConfigToken>>();
			FinishTokenUpdate();
		}
	}
}
