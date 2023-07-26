using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace QMS_Test
{
    public partial class VoicePlay : Form
    {
        public VoicePlay()
        {
            InitializeComponent();
            cbFolder.Items.AddRange(Directory.GetDirectories(Path.Combine(Application.StartupPath, "Wavs")));
            if (cbFolder.Items.Count > 0) cbFolder.SelectedIndex = 0;

            if (cbVoice.Items.Count > 0) cbVoice.SelectedIndex = 0;
        }

        private FileInfo[] GetWavFiles()
        {
            DirectoryInfo folder = new DirectoryInfo(cbFolder.Text.Trim());
            return folder.GetFiles("*.wav");
        }

        private void Play1_Click(object sender, EventArgs e)
        {
            Player1 p1= new Player1();
            p1.PlaySound(GetWavFiles());
        }

        private void Play2_Click(object sender, EventArgs e)
        {
            Player2 p2 = new Player2();
            p2.PlayAsync(GetWavFiles());
        }

        private void Play3_Click(object sender, EventArgs e)
        {
            Player3 p3 = new Player3();
            p3.PlaySound(txtText.Text.Trim(), cbVoice.Text.Trim());
        }

        /// <summary>
        /// english button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.txtText.Text = "Hello Ticket number C 0 0 1 , english";
            this.cbVoice.SelectedIndex = 0;
        }

        /// <summary>
        /// Traditional Chinese button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.txtText.Text = "你好 C 0 1 廣東話";
            this.cbVoice.SelectedIndex = 1;
        }

        /// <summary>
        /// Simplified Chinese
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.txtText.Text = "你好 D 0 1 普通话";
            this.cbVoice.SelectedIndex = 2;
        }

        private void cbVoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
