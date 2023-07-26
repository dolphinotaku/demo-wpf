
namespace QMS_Test
{
    partial class VoicePlay
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
            this.Play1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Play2 = new System.Windows.Forms.Button();
            this.Play3 = new System.Windows.Forms.Button();
            this.cbFolder = new System.Windows.Forms.ComboBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.cbVoice = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Play1
            // 
            this.Play1.Location = new System.Drawing.Point(78, 90);
            this.Play1.Name = "Play1";
            this.Play1.Size = new System.Drawing.Size(491, 26);
            this.Play1.TabIndex = 1;
            this.Play1.Text = "Play By Microsoft speech object Library";
            this.Play1.UseVisualStyleBackColor = true;
            this.Play1.Click += new System.EventHandler(this.Play1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "WAV files Folder";
            // 
            // Play2
            // 
            this.Play2.Location = new System.Drawing.Point(78, 132);
            this.Play2.Name = "Play2";
            this.Play2.Size = new System.Drawing.Size(491, 25);
            this.Play2.TabIndex = 1;
            this.Play2.Text = "Play By System.Media.SoundPlayer";
            this.Play2.UseVisualStyleBackColor = true;
            this.Play2.Click += new System.EventHandler(this.Play2_Click);
            // 
            // Play3
            // 
            this.Play3.Location = new System.Drawing.Point(77, 330);
            this.Play3.Name = "Play3";
            this.Play3.Size = new System.Drawing.Size(491, 25);
            this.Play3.TabIndex = 1;
            this.Play3.Text = "Play By SpeechSynthesizer";
            this.Play3.UseVisualStyleBackColor = true;
            this.Play3.Click += new System.EventHandler(this.Play3_Click);
            // 
            // cbFolder
            // 
            this.cbFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.cbFolder.FormattingEnabled = true;
            this.cbFolder.Location = new System.Drawing.Point(78, 44);
            this.cbFolder.Name = "cbFolder";
            this.cbFolder.Size = new System.Drawing.Size(490, 26);
            this.cbFolder.TabIndex = 5;
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(78, 220);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(490, 61);
            this.txtText.TabIndex = 6;
            this.txtText.Text = "Hello Ticket number C 0 0 1 , english";
            // 
            // cbVoice
            // 
            this.cbVoice.FormattingEnabled = true;
            this.cbVoice.Items.AddRange(new object[] {
            "Microsoft Zira Desktop",
            "Microsoft Tracy Desktop",
            "Microsoft Huihui Desktop"});
            this.cbVoice.Location = new System.Drawing.Point(77, 287);
            this.cbVoice.Name = "cbVoice";
            this.cbVoice.Size = new System.Drawing.Size(490, 26);
            this.cbVoice.TabIndex = 7;
            this.cbVoice.SelectedIndexChanged += new System.EventHandler(this.cbVoice_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(78, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Eng";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(159, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Traditional Chinese";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(300, 191);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Simplified Chinese";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // VoicePlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 372);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbVoice);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.cbFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Play3);
            this.Controls.Add(this.Play2);
            this.Controls.Add(this.Play1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "VoicePlay";
            this.Text = "VoicePlay";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Play1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Play2;
        private System.Windows.Forms.Button Play3;
        private System.Windows.Forms.ComboBox cbFolder;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.ComboBox cbVoice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

