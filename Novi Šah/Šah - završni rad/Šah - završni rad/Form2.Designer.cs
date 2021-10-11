namespace Šah___završni_rad
{
    partial class Form2
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
            this.grbFigure = new System.Windows.Forms.GroupBox();
            this.rbRandom = new System.Windows.Forms.RadioButton();
            this.rbBijele = new System.Windows.Forms.RadioButton();
            this.rbCrne = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.pcbNaslovnaSlika = new System.Windows.Forms.PictureBox();
            this.grbFigure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbNaslovnaSlika)).BeginInit();
            this.SuspendLayout();
            // 
            // grbFigure
            // 
            this.grbFigure.Controls.Add(this.rbRandom);
            this.grbFigure.Controls.Add(this.rbBijele);
            this.grbFigure.Controls.Add(this.rbCrne);
            this.grbFigure.Location = new System.Drawing.Point(12, 164);
            this.grbFigure.Name = "grbFigure";
            this.grbFigure.Size = new System.Drawing.Size(181, 54);
            this.grbFigure.TabIndex = 1;
            this.grbFigure.TabStop = false;
            this.grbFigure.Text = "Odaberite figure:";
            // 
            // rbRandom
            // 
            this.rbRandom.AutoSize = true;
            this.rbRandom.Location = new System.Drawing.Point(115, 31);
            this.rbRandom.Name = "rbRandom";
            this.rbRandom.Size = new System.Drawing.Size(65, 17);
            this.rbRandom.TabIndex = 2;
            this.rbRandom.Text = "Random";
            this.rbRandom.UseVisualStyleBackColor = true;
            // 
            // rbBijele
            // 
            this.rbBijele.AutoSize = true;
            this.rbBijele.Location = new System.Drawing.Point(59, 31);
            this.rbBijele.Name = "rbBijele";
            this.rbBijele.Size = new System.Drawing.Size(50, 17);
            this.rbBijele.TabIndex = 1;
            this.rbBijele.Text = "Bijele";
            this.rbBijele.UseVisualStyleBackColor = true;
            // 
            // rbCrne
            // 
            this.rbCrne.AutoSize = true;
            this.rbCrne.Checked = true;
            this.rbCrne.Location = new System.Drawing.Point(6, 31);
            this.rbCrne.Name = "rbCrne";
            this.rbCrne.Size = new System.Drawing.Size(47, 17);
            this.rbCrne.TabIndex = 0;
            this.rbCrne.TabStop = true;
            this.rbCrne.Text = "Crne";
            this.rbCrne.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(259, 226);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pcbNaslovnaSlika
            // 
            this.pcbNaslovnaSlika.Dock = System.Windows.Forms.DockStyle.Top;
            this.pcbNaslovnaSlika.Image = global::Šah___završni_rad.Properties.Resources.sah_naslovna;
            this.pcbNaslovnaSlika.Location = new System.Drawing.Point(0, 0);
            this.pcbNaslovnaSlika.Name = "pcbNaslovnaSlika";
            this.pcbNaslovnaSlika.Size = new System.Drawing.Size(346, 158);
            this.pcbNaslovnaSlika.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbNaslovnaSlika.TabIndex = 0;
            this.pcbNaslovnaSlika.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 261);
            this.ControlBox = false;
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grbFigure);
            this.Controls.Add(this.pcbNaslovnaSlika);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "Šah - Završni rad - Danijel Krajnović - 2016/2017";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.grbFigure.ResumeLayout(false);
            this.grbFigure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbNaslovnaSlika)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbNaslovnaSlika;
        private System.Windows.Forms.GroupBox grbFigure;
        private System.Windows.Forms.RadioButton rbRandom;
        private System.Windows.Forms.RadioButton rbBijele;
        private System.Windows.Forms.RadioButton rbCrne;
        private System.Windows.Forms.Button btnStart;
    }
}