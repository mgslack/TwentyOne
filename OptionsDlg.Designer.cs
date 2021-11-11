
namespace TwentyOne
{
    partial class OptionsDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbNumDecks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudMax = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudBank = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbImage = new System.Windows.Forms.ComboBox();
            this.cbBetMax = new System.Windows.Forms.CheckBox();
            this.pbCardBack = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBank)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCardBack)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Number of Decks to Use:";
            // 
            // cbNumDecks
            // 
            this.cbNumDecks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNumDecks.FormattingEnabled = true;
            this.cbNumDecks.Location = new System.Drawing.Point(145, 6);
            this.cbNumDecks.Name = "cbNumDecks";
            this.cbNumDecks.Size = new System.Drawing.Size(124, 21);
            this.cbNumDecks.TabIndex = 1;
            this.cbNumDecks.SelectedIndexChanged += new System.EventHandler(this.CbNumDecks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(275, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "Changes to card deck number will take place next shuffle";
            this.label2.UseMnemonic = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "M&inimum Bet:";
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(88, 41);
            this.nudMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(80, 20);
            this.nudMin.TabIndex = 4;
            this.nudMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Ma&ximum Bet:";
            // 
            // nudMax
            // 
            this.nudMax.Location = new System.Drawing.Point(262, 41);
            this.nudMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMax.Name = "nudMax";
            this.nudMax.Size = new System.Drawing.Size(80, 20);
            this.nudMax.TabIndex = 6;
            this.nudMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Initial &Bank:";
            // 
            // nudBank
            // 
            this.nudBank.Location = new System.Drawing.Point(88, 67);
            this.nudBank.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBank.Name = "nudBank";
            this.nudBank.Size = new System.Drawing.Size(100, 20);
            this.nudBank.TabIndex = 8;
            this.nudBank.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Will take affect next reset/new game";
            this.label6.UseWaitCursor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pbCardBack);
            this.panel1.Controls.Add(this.cbImage);
            this.panel1.Location = new System.Drawing.Point(88, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 105);
            this.panel1.TabIndex = 11;
            // 
            // cbImage
            // 
            this.cbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImage.FormattingEnabled = true;
            this.cbImage.Location = new System.Drawing.Point(3, 3);
            this.cbImage.Name = "cbImage";
            this.cbImage.Size = new System.Drawing.Size(185, 21);
            this.cbImage.TabIndex = 0;
            this.cbImage.SelectedIndexChanged += new System.EventHandler(this.CbImage_SelectedIndexChanged);
            // 
            // cbBetMax
            // 
            this.cbBetMax.AutoSize = true;
            this.cbBetMax.Location = new System.Drawing.Point(15, 93);
            this.cbBetMax.Name = "cbBetMax";
            this.cbBetMax.Size = new System.Drawing.Size(65, 17);
            this.cbBetMax.TabIndex = 10;
            this.cbBetMax.Text = "B&et Max";
            this.cbBetMax.UseVisualStyleBackColor = true;
            this.cbBetMax.CheckedChanged += new System.EventHandler(this.CbBetMax_CheckedChanged);
            // 
            // pbCardBack
            // 
            this.pbCardBack.Location = new System.Drawing.Point(194, 3);
            this.pbCardBack.Name = "pbCardBack";
            this.pbCardBack.Size = new System.Drawing.Size(71, 96);
            this.pbCardBack.TabIndex = 12;
            this.pbCardBack.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(15, 213);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(96, 213);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OptionsDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 248);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbBetMax);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudBank);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbNumDecks);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TwentyOne Options";
            this.Load += new System.EventHandler(this.OptionsDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBank)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCardBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNumDecks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudBank;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbImage;
        private System.Windows.Forms.CheckBox cbBetMax;
        private System.Windows.Forms.PictureBox pbCardBack;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}