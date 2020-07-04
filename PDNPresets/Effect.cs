using PaintDotNet;
using PaintDotNet.Effects;
using System.Collections.Generic;
using System.Drawing;

namespace PDNPresets
{
	[PluginSupportInfo(typeof(PluginSupportInfo))]
	public class PDNPresetsPlugin : Effect
	{
		private List<Effect> effects;
		private List<EffectConfigDialog> dialogs;

		private bool needReRender = false;

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
			this.effects = token.effects;
			this.dialogs = token.dialogs;

			base.OnSetRenderInfo(parameters, dstArgs, srcArgs);

			needReRender = true;
		}

		public override void Render(EffectConfigToken parameters, RenderArgs dstArgs, RenderArgs srcArgs, Rectangle[] rois, int startIndex, int length)
		{
			if (needReRender == false || length == 0) return;

			needReRender = false;

			PdnRegion selection = EnvironmentParameters.GetSelectionAsPdnRegion();

			using (Surface tmp = new Surface(srcArgs.Size))
			{
				tmp.CopySurface(srcArgs.Surface);

				Effect effect = null;
				EffectConfigToken token = null;

				int count = this.effects.Count;
				for (int i = 0; i < count; i++)
				{
					effect = this.effects[i];
					token = this.dialogs[i]?.EffectToken;

					effect.SetRenderInfo(token, dstArgs, new RenderArgs(tmp));
					effect.Render(token, dstArgs, new RenderArgs(tmp), selection);

					tmp.CopySurface(dstArgs.Surface);
				}

				dstArgs.Surface.CopySurface(tmp);
			}
		}
	}
}
