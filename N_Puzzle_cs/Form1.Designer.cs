namespace N_Puzzle_cs
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.exit_btn = new System.Windows.Forms.Button();
            this.play_btn = new System.Windows.Forms.Button();
            this.easy_btn = new System.Windows.Forms.RadioButton();
            this.medium_btn = new System.Windows.Forms.RadioButton();
            this.hard_btn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // exit_btn
            // 
            this.exit_btn.BackColor = System.Drawing.Color.Transparent;
            this.exit_btn.CausesValidation = false;
            this.exit_btn.Font = new System.Drawing.Font("Ravie", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit_btn.Location = new System.Drawing.Point(472, 173);
            this.exit_btn.Name = "exit_btn";
            this.exit_btn.Size = new System.Drawing.Size(150, 75);
            this.exit_btn.TabIndex = 0;
            this.exit_btn.Text = "EXIT";
            this.exit_btn.UseVisualStyleBackColor = false;
            this.exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            this.exit_btn.MouseLeave += new System.EventHandler(this.Exit_btn_MouseLeave);
            this.exit_btn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Exit_btn_MouseMove);
            // 
            // play_btn
            // 
            this.play_btn.BackColor = System.Drawing.Color.Transparent;
            this.play_btn.Font = new System.Drawing.Font("Ravie", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.play_btn.Location = new System.Drawing.Point(141, 173);
            this.play_btn.Name = "play_btn";
            this.play_btn.Size = new System.Drawing.Size(150, 75);
            this.play_btn.TabIndex = 0;
            this.play_btn.Text = "PLAY";
            this.play_btn.UseVisualStyleBackColor = false;
            this.play_btn.Click += new System.EventHandler(this.play_btn_Click);
            this.play_btn.MouseLeave += new System.EventHandler(this.Play_btn_MouseLeave);
            this.play_btn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Play_btn_MouseMove);
            // 
            // easy_btn
            // 
            this.easy_btn.AutoSize = true;
            this.easy_btn.Checked = true;
            this.easy_btn.Location = new System.Drawing.Point(141, 281);
            this.easy_btn.Name = "easy_btn";
            this.easy_btn.Size = new System.Drawing.Size(48, 17);
            this.easy_btn.TabIndex = 1;
            this.easy_btn.TabStop = true;
            this.easy_btn.Text = "Easy";
            this.easy_btn.UseVisualStyleBackColor = true;
            this.easy_btn.Click += new System.EventHandler(this.Easy_btn_Click);
            // 
            // medium_btn
            // 
            this.medium_btn.AutoSize = true;
            this.medium_btn.Location = new System.Drawing.Point(350, 281);
            this.medium_btn.Name = "medium_btn";
            this.medium_btn.Size = new System.Drawing.Size(62, 17);
            this.medium_btn.TabIndex = 1;
            this.medium_btn.Text = "Medium";
            this.medium_btn.UseVisualStyleBackColor = true;
            this.medium_btn.Click += new System.EventHandler(this.Medium_btn_Click);
            // 
            // hard_btn
            // 
            this.hard_btn.AutoSize = true;
            this.hard_btn.Location = new System.Drawing.Point(573, 281);
            this.hard_btn.Name = "hard_btn";
            this.hard_btn.Size = new System.Drawing.Size(48, 17);
            this.hard_btn.TabIndex = 1;
            this.hard_btn.Text = "Hard";
            this.hard_btn.UseVisualStyleBackColor = true;
            this.hard_btn.Click += new System.EventHandler(this.Hard_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::N_Puzzle_cs.Properties.Resources.img20150730152427343_0_16_458_640_crop_1438244757894;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.hard_btn);
            this.Controls.Add(this.medium_btn);
            this.Controls.Add(this.easy_btn);
            this.Controls.Add(this.play_btn);
            this.Controls.Add(this.exit_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "N Puzzle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exit_btn;
        private System.Windows.Forms.Button play_btn;
        private System.Windows.Forms.RadioButton easy_btn;
        private System.Windows.Forms.RadioButton medium_btn;
        private System.Windows.Forms.RadioButton hard_btn;
    }
}

