namespace PDNPresets
{
	partial class PDNPresetsConfigDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cbEffect = new System.Windows.Forms.ComboBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.lbEffect = new System.Windows.Forms.ListBox();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnModify = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cbEffect
			// 
			this.cbEffect.FormattingEnabled = true;
			this.cbEffect.Items.AddRange(new object[] {
			"Auto-Level (*)",
			"Black and White",
			"Brightness / Contrast",
			"Curves",
			"Hue / Saturation",
			"Invert Colors",
			"Levels (*)",
			"Posterize",
			"Sepia",
			"Ink Sketch",
			"Oil Painting",
			"Pencil Sketch",
			"Fragment",
			"Gaussian Blur",
			"Motion Blur",
			"Radial Blur  (*)",
			"Surface Blur",
			"Unfocus",
			"Zoom Blur (*)",
			"Bulge (*)",
			"Crystalize",
			"Dents (*)",
			"Frosted Glass",
			"Pixelate",
			"Polar Inversion (*)",
			"Tile Reflection",
			"Twist (*)",
			"Add Noise",
			"Median",
			"Reduce Noise",
			"Glow",
			"Red Eye Removal",
			"Sharpen",
			"Soften Portrait",
			"Vignette (*)",
			"Clouds",
			"Julia Fractal",
			"Mandelbrot Fractal",
			"Edge Detect",
			"Embose",
			"Outline",
			"Relief"});
			this.cbEffect.Location = new System.Drawing.Point(12, 12);
			this.cbEffect.Name = "cbEffect";
			this.cbEffect.Size = new System.Drawing.Size(140, 23);
			this.cbEffect.DropDownHeight = 200;
			this.cbEffect.SelectedIndex = 0;
			this.cbEffect.TabIndex = 0;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(158, 12);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(83, 23);
			this.btnAdd.TabIndex = 1;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// lbEffect
			// 
			this.lbEffect.FormattingEnabled = true;
			this.lbEffect.ItemHeight = 15;
			this.lbEffect.Location = new System.Drawing.Point(12, 41);
			this.lbEffect.Name = "lbEffect";
			this.lbEffect.Size = new System.Drawing.Size(228, 94);
			this.lbEffect.TabIndex = 2;
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(243, 41);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(243, 70);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 4;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(243, 99);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(75, 23);
			this.btnLoad.TabIndex = 5;
			this.btnLoad.Text = "Load";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnModify
			// 
			this.btnModify.Location = new System.Drawing.Point(15, 141);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(70, 23);
			this.btnModify.TabIndex = 6;
			this.btnModify.Text = "Modify";
			this.btnModify.UseVisualStyleBackColor = true;
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// btnUp
			// 
			this.btnUp.Location = new System.Drawing.Point(91, 141);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(70, 23);
			this.btnUp.TabIndex = 7;
			this.btnUp.Text = "Up";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(167, 141);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(70, 23);
			this.btnDown.TabIndex = 8;
			this.btnDown.Text = "Down";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(243, 141);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 9;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOK.Location = new System.Drawing.Point(151, 198);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(83, 23);
			this.buttonOK.TabIndex = 10;
			this.buttonOK.Text = "OK";
			this.buttonOK.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonCancel.Location = new System.Drawing.Point(243, 198);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(83, 23);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			// 
			// PDNPresetsConfigDialog
			// 
			this.AcceptButton = this.buttonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(334, 231);
			this.Controls.Add(this.cbEffect);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.lbEffect);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnModify);
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.buttonCancel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.Black;
			this.Location = new System.Drawing.Point(0, 0);
			this.Name = "PDNPresetsConfigDialog";
			this.Text = "PDNPresets";
			this.UseAppThemeColors = true;
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbEffect;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.ListBox lbEffect;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.Button btnModify;
		private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
	}
}
