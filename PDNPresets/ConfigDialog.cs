using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Security.Cryptography;

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

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			Effect effect = null;
			EffectConfigDialog dialog = null;

			if (this.cbEffect.Text == "Auto-Level")
			{
				effect = new AutoLevelEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else if (this.cbEffect.Text == "Black and White")
			{
				effect = new DesaturateEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else if (this.cbEffect.Text == "Brightness and Contrast")
			{
				effect = new BrightnessAndContrastAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}

			this.effects.Add(new Pair<Effect, EffectConfigToken>(effect, dialog.EffectToken));
			FinishTokenUpdate();
		}
	}
}
