using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Windows.Forms;
using System;

namespace PDNPresets
{
	internal partial class PDNPresetsConfigDialog : EffectConfigDialog
	{
		private List<string> names;
		private List<Effect> effects;
		private List<EffectConfigDialog> dialogs;

		public PDNPresetsConfigDialog()
		{
			InitializeComponent();
			this.names = new List<string>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			FinishTokenUpdate();
		}

		protected override void InitialInitToken()
		{
			theEffectToken = new PDNPresetsConfigToken();
		}

		protected override void InitTokenFromDialog()
		{
			PDNPresetsConfigToken token = (PDNPresetsConfigToken)EffectToken;
			token.names = new List<string>(this.names);
			token.effects = new List<Effect>(this.effects);
			token.dialogs = new List<EffectConfigDialog>(this.dialogs);
		}

		protected override void InitDialogFromToken(EffectConfigToken effectTokenCopy)
		{
			this.names = new List<string>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			FinishTokenUpdate();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string name = this.cbEffect.Text;
			Effect effect = null;
			EffectConfigDialog dialog = null;

			if (name == "Auto-Level")
			{
				effect = new AutoLevelEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = DialogResult.OK;
			}
			else if (name == "Black and White")
			{
				effect = new DesaturateEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = DialogResult.OK;
			}
			else if (name == "Brightness / Contrast")
			{
				effect = new BrightnessAndContrastAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (name == "Curves")
			{
				effect = new CurvesEffect();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (name == "Hue / Saturation")
			{
				effect = new HueAndSaturationAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (name == "Invert Colors")
			{
				effect = new InvertColorsEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = DialogResult.OK;
			}
			else if (name == "Levels")
			{
				effect = new LevelsEffect();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (name == "Posterize")
			{
				effect = new PosterizeAdjustment();
				dialog = effect.CreateConfigDialog();
				dialog.ShowDialog();
			}
			else if (name == "Sepia")
			{
				effect = new SepiaEffect();
				dialog = effect.CreateConfigDialog();
				dialog.DialogResult = DialogResult.OK;
			}

			if (dialog.DialogResult == DialogResult.OK)
			{
				this.lbEffect.Items.Add(this.cbEffect.Text);
				this.names.Add(name);
				this.effects.Add(effect);
				this.dialogs.Add(dialog);
				FinishTokenUpdate();
			}
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index >= 0)
			{
				this.lbEffect.Items.RemoveAt(index);
				this.names.RemoveAt(index);
				this.effects.RemoveAt(index);
				this.dialogs.RemoveAt(index);
				FinishTokenUpdate();
			}
		}
	}
}
