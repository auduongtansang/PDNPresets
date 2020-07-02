using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using PaintDotNet.PropertySystem;
using System.IO;

namespace PDNPresets
{
	internal partial class PDNPresetsConfigDialog : EffectConfigDialog
	{
		private static List<Type> available = new List<Type>()
		{
			new AutoLevelEffect().GetType(),  //Not working
			new DesaturateEffect().GetType(),
			new BrightnessAndContrastAdjustment().GetType(),
			new CurvesEffect().GetType(),  //Cannot save
			new HueAndSaturationAdjustment().GetType(),
			new InvertColorsEffect().GetType(),
			new LevelsEffect().GetType(),  //Error
			new PosterizeAdjustment().GetType(),
			new SepiaEffect().GetType(),
			new InkSketchEffect().GetType(),
			new OilPaintingEffect().GetType(),
			new PencilSketchEffect().GetType(),
			new FragmentEffect().GetType(),
			new GaussianBlurEffect().GetType(),
			new MotionBlurEffect().GetType(),
			new RadialBlurEffect().GetType(),  //Error
			new SurfaceBlurEffect().GetType(),
			new UnfocusEffect().GetType(),
			new ZoomBlurEffect().GetType(),  //Error
			new BulgeEffect().GetType(),  //Error
			new CrystalizeEffect().GetType(),
			new DentsEffect().GetType(),  //Not working
			new FrostedGlassEffect().GetType(),
			//new MorphologyEffect().GetType(),  //Cannot create type
			new PixelateEffect().GetType(),
			new PolarInversionEffect().GetType(),  //Error
			new TileEffect().GetType(),
			new TwistEffect().GetType(),  //Error
			new AddNoiseEffect().GetType(),
			new MedianEffect().GetType(),
			new ReduceNoiseEffect().GetType(),
			new GlowEffect().GetType(),
			new RedEyeRemoveEffect().GetType(),
			new SharpenEffect().GetType(),
			new SoftenPortraitEffect().GetType(),
			new VignetteEffect().GetType(),  //Error
			new CloudsEffect().GetType(),
			new JuliaFractalEffect().GetType(),
			new MandelbrotFractalEffect().GetType(),
			//new TurbulenceEffect().GetType(),  //Cannot create type
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
			dialog = effect.CreateConfigDialog();

			if ((effect.Options.Flags & EffectFlags.Configurable) == 0)
			{
				dialog.DialogResult = DialogResult.OK;
			}
			else
			{
				dialog.ShowDialog();
			}

			if (dialog.DialogResult == DialogResult.OK)
			{
				if (dialog.EffectToken is PropertyBasedEffectConfigToken)
				{
					collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
				}

				this.lbEffect.Items.Add(this.cbEffect.Text);
				this.types.Add(type);
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
				BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create));

				int effectCount = this.types.Count;

				writer.Write(effectCount);
				for (int i = 0; i < effectCount; i++)
				{
					writer.Write(this.types[i]);

					if (this.dialogs[i].EffectToken is PropertyBasedEffectConfigToken)
					{
						IEnumerator<Property> enumerator = this.collections[i].GetEnumerator();
						while (enumerator.MoveNext())
						{
							Property property = enumerator.Current;

							if (property is Int32Property)
								writer.Write((int)property.Value);
							else if (property is DoubleProperty)
								writer.Write((double)property.Value);
							else if (property is BooleanProperty)
								writer.Write((bool)property.Value);
						}
					}
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
				this.types = new List<int>();
				this.effects = new List<Effect>();
				this.dialogs = new List<EffectConfigDialog>();
				this.collections = new List<PropertyCollection>();

				int type = 0;
				Effect effect = null;
				EffectConfigDialog dialog = null;
				PropertyCollection collection = null;
				PropertyBasedEffectConfigToken token = null;

				int effectCount = reader.ReadInt32();
				for (int i = 0; i < effectCount; i++)
				{
					type = reader.ReadInt32();

					effect = (Effect)available[type].GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
					dialog = effect.CreateConfigDialog();
					
					if (dialog.EffectToken is PropertyBasedEffectConfigToken)
					{
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
						token = new PropertyBasedEffectConfigToken(collection);

						IEnumerator<Property> enumerator = collection.GetEnumerator();
						while (enumerator.MoveNext())
						{
							Property property = enumerator.Current;

							if (property is Int32Property)
								token.SetPropertyValue(property.Name, reader.ReadInt32());
							else if (property is DoubleProperty)
								token.SetPropertyValue(property.Name, reader.ReadDouble());
							else if (property is BooleanProperty)
								token.SetPropertyValue(property.Name, reader.ReadBoolean());
						}

						dialog.EffectToken = token;
						collection = ((PropertyBasedEffectConfigToken)dialog.EffectToken).Properties;
					}

					this.lbEffect.Items.Add(this.cbEffect.Items[type]);
					this.types.Add(type);
					this.effects.Add(effect);
					this.dialogs.Add(dialog);
					this.collections.Add(collection);
				}

				FinishTokenUpdate();
				reader.Close();
			}
		}

		private void btnUp_Click(object sender, EventArgs e)
		{
			int index = this.lbEffect.SelectedIndex;

			if (index > 0)
			{
				object tmp = null;

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
				object tmp = null;

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
