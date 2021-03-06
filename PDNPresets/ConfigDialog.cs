﻿using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using PaintDotNet.PropertySystem;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace PDNPresets
{
	internal partial class PDNPresetsConfigDialog : EffectConfigDialog
	{
		private static List<Type> available = new List<Type>()
		{
			new DesaturateEffect().GetType(),
			new BrightnessAndContrastAdjustment().GetType(),
			new CurvesEffect().GetType(),
			new HueAndSaturationAdjustment().GetType(),
			new InvertColorsEffect().GetType(),
			new PosterizeAdjustment().GetType(),
			new SepiaEffect().GetType(),
			new InkSketchEffect().GetType(),
			new OilPaintingEffect().GetType(),
			new PencilSketchEffect().GetType(),
			new FragmentEffect().GetType(),
			new GaussianBlurEffect().GetType(),
			new MotionBlurEffect().GetType(),
			new SurfaceBlurEffect().GetType(),
			new UnfocusEffect().GetType(),
			new CrystalizeEffect().GetType(),
			new FrostedGlassEffect().GetType(),
			new PixelateEffect().GetType(),
			new TileEffect().GetType(),
			new AddNoiseEffect().GetType(),
			new MedianEffect().GetType(),
			new ReduceNoiseEffect().GetType(),
			new GlowEffect().GetType(),
			new RedEyeRemoveEffect().GetType(),
			new SharpenEffect().GetType(),
			new SoftenPortraitEffect().GetType(),
			new CloudsEffect().GetType(),
			new JuliaFractalEffect().GetType(),
			new MandelbrotFractalEffect().GetType(),
			new EdgeDetectEffect().GetType(),
			new EmbossEffect().GetType(),
			new OutlineEffect().GetType(),
			new ReliefEffect().GetType(),
		};

		private List<int> types;
		private List<Effect> effects;
		private List<EffectConfigDialog> dialogs;
		private List<PropertyCollection> collections;

		public PDNPresetsConfigDialog()
		{
			InitializeComponent();
			this.types = new List<int>();
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
			token.types = new List<int>(this.types);
			token.effects = new List<Effect>(this.effects);
			token.dialogs = new List<EffectConfigDialog>(this.dialogs);
			token.collections = new List<PropertyCollection>(this.collections);
		}

		protected override void InitDialogFromToken(EffectConfigToken effectTokenCopy)
		{
			this.types = new List<int>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			this.collections = new List<PropertyCollection>();
			FinishTokenUpdate();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			int type = this.cbEffect.SelectedIndex;
			Effect effect = null;
			EffectConfigDialog dialog = null;
			PropertyCollection collection = null;

			effect = (Effect)available[type].GetConstructor(Type.EmptyTypes).Invoke(new object[0]);

			if ((effect.Options.Flags & EffectFlags.Configurable) != 0)
			{
				dialog = effect.CreateConfigDialog();
				dialog.Effect = effect;

				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (dialog.EffectToken is PropertyBasedEffectConfigToken)
					{
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
					}
				}
				else
				{
					return;
				}	
			}

			this.lbEffect.Items.Add(this.cbEffect.Text);
			this.types.Add(type);
			this.effects.Add(effect);
			this.dialogs.Add(dialog);
			this.collections.Add(collection);
			FinishTokenUpdate();
		}

		private void btnRemove_Click(object sender, EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index >= 0)
			{
				this.lbEffect.Items.RemoveAt(index);
				this.types.RemoveAt(index);
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
				FileStream stream = File.Open(saveFileDialog.FileName, FileMode.Create);
				BinaryFormatter writer = new BinaryFormatter();

				int effectCount = this.types.Count;

				writer.Serialize(stream, effectCount);
				for (int i = 0; i < effectCount; i++)
				{
					writer.Serialize(stream, this.types[i]);

					if (this.dialogs[i]?.EffectToken is PropertyBasedEffectConfigToken)
					{
						IEnumerator<Property> enumerator = this.collections[i].GetEnumerator();
						while (enumerator.MoveNext())
						{
							writer.Serialize(stream, enumerator.Current.Value);
						}
					}
					else if (this.dialogs[i] != null)
					{
						Type tokenType = this.dialogs[i].EffectToken.GetType();
						PropertyInfo[] info = tokenType.GetProperties();

						for (int j = 0; j < info.Length; j++)
						{
							object property = info[j].GetValue(this.dialogs[i].EffectToken);
							if (property.GetType().IsSerializable == true)
							{
								writer.Serialize(stream, property);
							}
						}
					}
				}

				stream.Close();
			}
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "PDN Preset Files (.pst)|*.pst";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				FileStream stream = File.Open(openFileDialog.FileName, FileMode.Open);
				BinaryFormatter reader = new BinaryFormatter();

				this.lbEffect.Items.Clear();
				this.types = new List<int>();
				this.effects = new List<Effect>();
				this.dialogs = new List<EffectConfigDialog>();
				this.collections = new List<PropertyCollection>();

				int type = 0;
				Effect effect = null;
				EffectConfigDialog dialog = null;
				PropertyCollection collection = null;
				PropertyBasedEffectConfigToken token = null;

				int effectCount = (int)reader.Deserialize(stream);
				for (int i = 0; i < effectCount; i++)
				{
					type = (int)reader.Deserialize(stream);

					effect = (Effect)available[type].GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
					
					if ((effect.Options.Flags & EffectFlags.Configurable) != 0)
					{
						dialog = effect.CreateConfigDialog();
						dialog.Effect = effect;

						if (dialog.EffectToken is PropertyBasedEffectConfigToken)
						{
							collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
							token = new PropertyBasedEffectConfigToken(collection);

							IEnumerator<Property> enumerator = collection.GetEnumerator();
							while (enumerator.MoveNext())
							{
								token.SetPropertyValue(enumerator.Current.Name, reader.Deserialize(stream));
							}

							dialog.EffectToken = token;
							collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						}
						else
						{
							Type tokenType = dialog.EffectToken.GetType();
							PropertyInfo[] info = tokenType.GetProperties();

							for (int j = 0; j < info.Length; j++)
							{
								if (info[j].GetValue(dialog.EffectToken).GetType().IsSerializable == true)
								{
									object property = reader.Deserialize(stream);
									info[j].SetValue(dialog.EffectToken, property);
								}
							}
						}
					}

					this.lbEffect.Items.Add(this.cbEffect.Items[type]);
					this.types.Add(type);
					this.effects.Add(effect);
					this.dialogs.Add(dialog);
					this.collections.Add(collection);
				}

				FinishTokenUpdate();
				stream.Close();
			}
		}

		private void btnModify_Click(object sender, EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index >= 0)
			{
				if ((this.effects[index].Options.Flags & EffectFlags.Configurable) != 0)
				{
					if (this.dialogs[index].ShowDialog() == DialogResult.OK)
					{
						if (this.dialogs[index].EffectToken is PropertyBasedEffectConfigToken)
						{
							this.collections[index] = ((PropertyBasedEffectConfigToken)this.dialogs[index].EffectToken).Properties;
						}
						FinishTokenUpdate();
					}
				}
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index > 0)
			{
				dynamic tmp = null;

				--this.lbEffect.SelectedIndex;

				tmp = this.lbEffect.Items[index];
				this.lbEffect.Items[index] = this.lbEffect.Items[index - 1];
				this.lbEffect.Items[index - 1] = (string)tmp;

				tmp = this.types[index];
				this.types[index] = this.types[index - 1];
				this.types[index - 1] = (int)tmp;

				tmp = this.effects[index];
				this.effects[index] = this.effects[index - 1];
				this.effects[index - 1] = (Effect)tmp;

				tmp = this.dialogs[index];
				this.dialogs[index] = this.dialogs[index - 1];
				this.dialogs[index - 1] = (EffectConfigDialog)tmp;

				tmp = this.collections[index];
				this.collections[index] = this.collections[index - 1];
				this.collections[index - 1] = (PropertyCollection)tmp;

				FinishTokenUpdate();
			}
		}

		private void btnDown_Click(object sender, EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index < this.lbEffect.Items.Count - 1)
			{
				dynamic tmp = null;

				++this.lbEffect.SelectedIndex;

				tmp = this.lbEffect.Items[index];
				this.lbEffect.Items[index] = this.lbEffect.Items[index + 1];
				this.lbEffect.Items[index + 1] = (string)tmp;

				tmp = this.types[index];
				this.types[index] = this.types[index + 1];
				this.types[index + 1] = (int)tmp;

				tmp = this.effects[index];
				this.effects[index] = this.effects[index + 1];
				this.effects[index + 1] = (Effect)tmp;

				tmp = this.dialogs[index];
				this.dialogs[index] = this.dialogs[index + 1];
				this.dialogs[index + 1] = (EffectConfigDialog)tmp;

				tmp = this.collections[index];
				this.collections[index] = this.collections[index + 1];
				this.collections[index + 1] = (PropertyCollection)tmp;

				FinishTokenUpdate();
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			this.lbEffect.Items.Clear();
			this.types = new List<int>();
			this.effects = new List<Effect>();
			this.dialogs = new List<EffectConfigDialog>();
			this.collections = new List<PropertyCollection>();
			FinishTokenUpdate();
		}
	}
}
