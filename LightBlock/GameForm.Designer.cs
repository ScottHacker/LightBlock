namespace LightBlock
{
    partial class GameForm
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
            this.button_new = new System.Windows.Forms.Button();
            this.button_restart = new System.Windows.Forms.Button();
            this.panel_setup = new System.Windows.Forms.Panel();
            this.num_width = new System.Windows.Forms.NumericUpDown();
            this.num_height = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button_play = new System.Windows.Forms.Button();
            this.panel_buttons = new System.Windows.Forms.Panel();
            this.gameboard = new System.Windows.Forms.Panel();
            this.panel_setup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_height)).BeginInit();
            this.panel_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_new
            // 
            this.button_new.Location = new System.Drawing.Point(0, 3);
            this.button_new.Name = "button_new";
            this.button_new.Size = new System.Drawing.Size(129, 41);
            this.button_new.TabIndex = 0;
            this.button_new.Text = "New Game";
            this.button_new.UseVisualStyleBackColor = true;
            this.button_new.Click += new System.EventHandler(this.button_new_Click);
            // 
            // button_restart
            // 
            this.button_restart.Location = new System.Drawing.Point(195, 3);
            this.button_restart.Name = "button_restart";
            this.button_restart.Size = new System.Drawing.Size(129, 41);
            this.button_restart.TabIndex = 1;
            this.button_restart.Text = "Start Over";
            this.button_restart.UseVisualStyleBackColor = true;
            this.button_restart.Click += new System.EventHandler(this.button_restart_Click);
            // 
            // panel_setup
            // 
            this.panel_setup.Controls.Add(this.num_width);
            this.panel_setup.Controls.Add(this.num_height);
            this.panel_setup.Controls.Add(this.label4);
            this.panel_setup.Controls.Add(this.label3);
            this.panel_setup.Controls.Add(this.label2);
            this.panel_setup.Controls.Add(this.label1);
            this.panel_setup.Controls.Add(this.button_play);
            this.panel_setup.Location = new System.Drawing.Point(0, 0);
            this.panel_setup.Name = "panel_setup";
            this.panel_setup.Size = new System.Drawing.Size(342, 291);
            this.panel_setup.TabIndex = 2;
            // 
            // num_width
            // 
            this.num_width.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_width.Location = new System.Drawing.Point(184, 72);
            this.num_width.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.num_width.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_width.Name = "num_width";
            this.num_width.ReadOnly = true;
            this.num_width.Size = new System.Drawing.Size(66, 53);
            this.num_width.TabIndex = 8;
            this.num_width.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // num_height
            // 
            this.num_height.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_height.Location = new System.Drawing.Point(81, 72);
            this.num_height.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.num_height.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.num_height.Name = "num_height";
            this.num_height.ReadOnly = true;
            this.num_height.Size = new System.Drawing.Size(66, 53);
            this.num_height.TabIndex = 7;
            this.num_height.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(149, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "BY";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pick your grid size";
            // 
            // button_play
            // 
            this.button_play.Location = new System.Drawing.Point(81, 153);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(169, 58);
            this.button_play.TabIndex = 0;
            this.button_play.Text = "Play!";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.button_play_Click);
            // 
            // panel_buttons
            // 
            this.panel_buttons.Controls.Add(this.button_new);
            this.panel_buttons.Controls.Add(this.button_restart);
            this.panel_buttons.Location = new System.Drawing.Point(55, 393);
            this.panel_buttons.Name = "panel_buttons";
            this.panel_buttons.Size = new System.Drawing.Size(327, 108);
            this.panel_buttons.TabIndex = 3;
            this.panel_buttons.Visible = false;
            // 
            // gameboard
            // 
            this.gameboard.Location = new System.Drawing.Point(44, 297);
            this.gameboard.Name = "gameboard";
            this.gameboard.Size = new System.Drawing.Size(34, 26);
            this.gameboard.TabIndex = 4;
            this.gameboard.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 474);
            this.Controls.Add(this.gameboard);
            this.Controls.Add(this.panel_buttons);
            this.Controls.Add(this.panel_setup);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Light Block";
            this.panel_setup.ResumeLayout(false);
            this.panel_setup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_height)).EndInit();
            this.panel_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_new;
        private System.Windows.Forms.Button button_restart;
        private System.Windows.Forms.Panel panel_setup;
        private System.Windows.Forms.Button button_play;
        private System.Windows.Forms.Panel panel_buttons;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel gameboard;
        private System.Windows.Forms.NumericUpDown num_height;
        private System.Windows.Forms.NumericUpDown num_width;

    }
}

