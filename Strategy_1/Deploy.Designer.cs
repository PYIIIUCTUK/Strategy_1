namespace Strategy_1
{
    partial class Deploy
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
            this.rbLight = new System.Windows.Forms.RadioButton();
            this.rbMedium = new System.Windows.Forms.RadioButton();
            this.rbHeavy = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.rbDestroyer = new System.Windows.Forms.RadioButton();
            this.rbSpireLight = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rbLight
            // 
            this.rbLight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbLight.AutoSize = true;
            this.rbLight.Location = new System.Drawing.Point(704, 104);
            this.rbLight.Name = "rbLight";
            this.rbLight.Size = new System.Drawing.Size(48, 17);
            this.rbLight.TabIndex = 999;
            this.rbLight.Text = "Light";
            this.rbLight.UseVisualStyleBackColor = true;
            this.rbLight.CheckedChanged += new System.EventHandler(this.rbLight_CheckedChanged);
            // 
            // rbMedium
            // 
            this.rbMedium.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbMedium.AutoSize = true;
            this.rbMedium.Location = new System.Drawing.Point(704, 143);
            this.rbMedium.Name = "rbMedium";
            this.rbMedium.Size = new System.Drawing.Size(62, 17);
            this.rbMedium.TabIndex = 999;
            this.rbMedium.Text = "Medium";
            this.rbMedium.UseVisualStyleBackColor = true;
            this.rbMedium.CheckedChanged += new System.EventHandler(this.rbMedium_CheckedChanged);
            // 
            // rbHeavy
            // 
            this.rbHeavy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbHeavy.AutoSize = true;
            this.rbHeavy.Location = new System.Drawing.Point(704, 180);
            this.rbHeavy.Name = "rbHeavy";
            this.rbHeavy.Size = new System.Drawing.Size(56, 17);
            this.rbHeavy.TabIndex = 999;
            this.rbHeavy.Text = "Heavy";
            this.rbHeavy.UseVisualStyleBackColor = true;
            this.rbHeavy.CheckedChanged += new System.EventHandler(this.rbHeavy_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(691, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Готово";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rbDestroyer
            // 
            this.rbDestroyer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbDestroyer.AutoSize = true;
            this.rbDestroyer.Location = new System.Drawing.Point(710, 218);
            this.rbDestroyer.Name = "rbDestroyer";
            this.rbDestroyer.Size = new System.Drawing.Size(70, 17);
            this.rbDestroyer.TabIndex = 1000;
            this.rbDestroyer.Text = "Destroyer";
            this.rbDestroyer.UseVisualStyleBackColor = true;
            this.rbDestroyer.CheckedChanged += new System.EventHandler(this.rbDestroyer_CheckedChanged);
            // 
            // rbSpireLight
            // 
            this.rbSpireLight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbSpireLight.AutoSize = true;
            this.rbSpireLight.Location = new System.Drawing.Point(710, 250);
            this.rbSpireLight.Name = "rbSpireLight";
            this.rbSpireLight.Size = new System.Drawing.Size(75, 17);
            this.rbSpireLight.TabIndex = 1001;
            this.rbSpireLight.Text = "Spire Light";
            this.rbSpireLight.UseVisualStyleBackColor = true;
            this.rbSpireLight.CheckedChanged += new System.EventHandler(this.rbSpireLight_CheckedChanged);
            // 
            // Deploy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rbSpireLight);
            this.Controls.Add(this.rbDestroyer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rbHeavy);
            this.Controls.Add(this.rbMedium);
            this.Controls.Add(this.rbLight);
            this.Name = "Deploy";
            this.Text = "Deploy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Deploy_FormClosed);
            this.Shown += new System.EventHandler(this.Deploy_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Deploy_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Deploy_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Deploy_MouseDoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbLight;
        private System.Windows.Forms.RadioButton rbMedium;
        private System.Windows.Forms.RadioButton rbHeavy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton rbDestroyer;
        private System.Windows.Forms.RadioButton rbSpireLight;
    }
}