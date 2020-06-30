using PaintDotNet;
using PaintDotNet.Effects;
using PaintDotNet.PropertySystem;
using System.Collections.Generic;
using System.Drawing;

namespace PDNPresets
{
	[PluginSupportInfo(typeof(PluginSupportInfo))]
	public class PDNPresetsPlugin : Effect
	{
		private List<string> names;
		private List<Effect> effects;
		private List<EffectConfigDialog> dialogs;
		private List<PropertyCollection> collections;

		public PDNPresetsPlugin()
			: base("PDNPresets", null, null, new EffectOptions { Flags = EffectFlags.Configurable })
		{
		}

		public override EffectConfigDialog CreateConfigDialog()
		{
			return new PDNPresetsConfigDialog();
		}

		protected override void OnSetRenderInfo(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs)
		{
			PDNPresetsConfigToken token = (PDNPresetsConfigToken)parameters;
			this.names = token.names;
			this.effects = token.effects;
			this.dialogs = token.dialogs;
			this.collections = token.collections;

			base.OnSetRenderInfo(parameters, dstArgs, srcArgs);
		}

		public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
		{
			if (length == 0) return;
			for (int i = startIndex; i < startIndex + length; ++i)
			{
				Render(dstArgs.Surface, srcArgs.Surface, rois[i]);
			}
		}

		private void Render(Surface dst, Surface src, Rectangle rect)
		{
			for (int y = rect.Top; y < rect.Bottom; y++)
			{
				if (IsCancelRequested) return;
				for (int x = rect.Left; x < rect.Right; x++)
				{
					dst[x, y] = src[x, y];
				}
			}

			int count = this.names.Count;
			for (int i = 0; i < count; i++)
			{
				string name = this.names[i];
				Effect effect = this.effects[i];
				EffectConfigDialog dialog = this.dialogs[i];
				PropertyCollection collection = this.collections[i];

				effect.Render(dialog.EffectToken, new RenderArgs(dst), new RenderArgs(dst), new Rectangle[1] { rect }, 0, 1);
			}
		}
	}
}
