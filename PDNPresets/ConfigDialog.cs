using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using PaintDotNet.PropertySystem;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace PDNPresets
{
	internal partial class PDNPresetsConfigDialog : EffectConfigDialog
	{
		private List<string> names;
		private List<Effect> effects;
		private List<EffectConfigDialog> dialogs;
		private List<PropertyCollection> collections;

		public PDNPresetsConfigDialog()
		{
			InitializeComponent();
			this.names = new List<string>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			this.collections = new List<PropertyCollection>();
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
			token.collections = new List<PropertyCollection>(this.collections);
		}

		protected override void InitDialogFromToken(EffectConfigToken effectTokenCopy)
		{
			this.names = new List<string>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			this.collections = new List<PropertyCollection>();
			FinishTokenUpdate();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string name = this.cbEffect.Text;
			Effect effect = null;
			EffectConfigDialog dialog = null;
			PropertyCollection collection = null;

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
				collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;

				this.lbEffect.Items.Add(this.cbEffect.Text);
				this.names.Add(name);
				this.effects.Add(effect);
				this.dialogs.Add(dialog);
				this.collections.Add(collection);
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
				this.collections.RemoveAt(index);
				FinishTokenUpdate();
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "PDN Preset Files (.pst)|*.pst";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create));

				int effectCount = this.names.Count;

				writer.Write(effectCount);
				for (int i = 0; i < effectCount; i++)
				{
					writer.Write(this.names[i]);

					IEnumerator<Property> enumerator = this.collections[i].GetEnumerator();
					while (enumerator.MoveNext())
						writer.Write((int)enumerator.Current.Value);
				}

				writer.Close();
			}
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "PDN Preset Files (.pst)|*.pst";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				BinaryReader reader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open));

				this.lbEffect.Items.Clear();
				this.names = new List<string>();
				this.effects = new List<Effect>();
				this.dialogs = new List<EffectConfigDialog>();
				this.collections = new List<PropertyCollection>();

				string name = "";
				Effect effect = null;
				EffectConfigDialog dialog = null;
				PropertyCollection collection = null;
				PropertyBasedEffectConfigToken token = null;

				int effectCount = reader.ReadInt32();
				for (int i = 0; i < effectCount; i++)
				{
					name = reader.ReadString();

					if (name == "Auto-Level")
					{
						effect = new AutoLevelEffect();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);
					}
					else if (name == "Black and White")
					{
						effect = new DesaturateEffect();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);
					}
					else if (name == "Brightness / Contrast")
					{
						effect = new BrightnessAndContrastAdjustment();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);

						foreach (BrightnessAndContrastAdjustment.PropertyNames j in Enum.GetValues(typeof(BrightnessAndContrastAdjustment.PropertyNames)))
						{
							int propValue = reader.ReadInt32();
							token.SetPropertyValue(j, propValue);
						}
					}
					else if (name == "Curves")
					{
						effect = new CurvesEffect();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);
					}
					else if (name == "Hue / Saturation")
					{
						effect = new HueAndSaturationAdjustment();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);

						foreach (HueAndSaturationAdjustment.PropertyNames j in Enum.GetValues(typeof(HueAndSaturationAdjustment.PropertyNames)))
						{
							int propValue = reader.ReadInt32();
							token.SetPropertyValue(j, propValue);
						}
					}
					else if (name == "Invert Colors")
					{
						effect = new InvertColorsEffect();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);
					}
					else if (name == "Levels")
					{
						effect = new LevelsEffect();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);
					}
					else if (name == "Posterize")
					{
						effect = new PosterizeAdjustment();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);

						foreach (PosterizeAdjustment.PropertyNames j in Enum.GetValues(typeof(PosterizeAdjustment.PropertyNames)))
						{
							int propValue = reader.ReadInt32();
							token.SetPropertyValue(j, propValue);
						}
					}
					else if (name == "Sepia")
					{
						effect = new SepiaEffect();
						dialog = effect.CreateConfigDialog();
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);
					}

					dialog.EffectToken = token;
					collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;

					this.lbEffect.Items.Add(name);
					this.names.Add(name);
					this.effects.Add(effect);
					this.dialogs.Add(dialog);
					this.collections.Add(collection);
				}

				FinishTokenUpdate();
				reader.Close();
			}
		}
	}
}
