namespace GutterLines
{
    partial class Window
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.LatLonLbl = new System.Windows.Forms.Label();
            this.gridMap = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ExitBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.titleLbl = new System.Windows.Forms.Label();
            this.NextClientBtn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextClientBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // LatLonLbl
            // 
            this.LatLonLbl.AutoSize = true;
            this.LatLonLbl.BackColor = System.Drawing.Color.Transparent;
            this.LatLonLbl.Location = new System.Drawing.Point(4, 178);
            this.LatLonLbl.Name = "LatLonLbl";
            this.LatLonLbl.Size = new System.Drawing.Size(40, 13);
            this.LatLonLbl.TabIndex = 2;
            this.LatLonLbl.Text = "LatLon";
            // 
            // gridMap
            // 
            this.gridMap.BackColor = System.Drawing.Color.White;
            this.gridMap.Location = new System.Drawing.Point(-2, 17);
            this.gridMap.Name = "gridMap";
            this.gridMap.Size = new System.Drawing.Size(159, 158);
            this.gridMap.TabIndex = 6;
            this.gridMap.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            this.pictureBox1.Location = new System.Drawing.Point(0, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 158);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.Color.Transparent;
            this.ExitBtn.BackgroundImage = global::GutterLines.Properties.Resources.close;
            this.ExitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ExitBtn.Location = new System.Drawing.Point(146, 4);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(9, 9);
            this.ExitBtn.TabIndex = 8;
            this.ExitBtn.TabStop = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::GutterLines.Properties.Resources.dot;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Location = new System.Drawing.Point(4, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(9, 9);
            this.pictureBox3.TabIndex = 9;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDown);
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.BackColor = System.Drawing.Color.Transparent;
            this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.Location = new System.Drawing.Point(13, 2);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(103, 13);
            this.titleLbl.TabIndex = 10;
            this.titleLbl.Text = "Gutter Lines by Tribe";
            this.titleLbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDown);
            // 
            // NextClientBtn
            // 
            this.NextClientBtn.BackColor = System.Drawing.Color.Transparent;
            this.NextClientBtn.BackgroundImage = global::GutterLines.Properties.Resources.arrow;
            this.NextClientBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.NextClientBtn.Location = new System.Drawing.Point(148, 181);
            this.NextClientBtn.Name = "NextClientBtn";
            this.NextClientBtn.Size = new System.Drawing.Size(6, 9);
            this.NextClientBtn.TabIndex = 11;
            this.NextClientBtn.TabStop = false;
            this.NextClientBtn.Click += new System.EventHandler(this.NextClientBtn_Click);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::GutterLines.Properties.Resources.back;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(158, 195);
            this.Controls.Add(this.gridMap);
            this.Controls.Add(this.NextClientBtn);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LatLonLbl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Window";
            this.Text = "GutterLines";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Window_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Window_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NextClientBtn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LatLonLbl;
        private System.Windows.Forms.PictureBox gridMap;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox ExitBtn;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.PictureBox NextClientBtn;
    }
}