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
			else if (this.cbEffect.Text == "Brightness / Contrast")
			{
				effect = new BrightnessAndContrastAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (this.cbEffect.Text == "Curves")
			{
				effect = new CurvesEffect();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (this.cbEffect.Text == "Hue / Saturation")
			{
				effect = new HueAndSaturationAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (this.cbEffect.Text == "Invert Colors")
			{
				effect = new InvertColorsEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else if (this.cbEffect.Text == "Levels")
			{
				effect = new LevelsEffect();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (this.cbEffect.Text == "Posterize")
			{
				effect = new PosterizeAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (this.cbEffect.Text == "Sepia")
			{
				effect = new SepiaEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = System.Windows.Forms.DialogResult.OK;
			}

			if (dialog.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				this.lbEffect.Items.Add(this.cbEffect.Text);
				this.effects.Add(new Pair<Effect, EffectConfigToken>(effect, dialog.EffectToken));
				FinishTokenUpdate();
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index >= 0)
			{
				this.lbEffect.Items.RemoveAt(index);
				this.effects.RemoveAt(index);
				FinishTokenUpdate();
			}
		}
	}
}
