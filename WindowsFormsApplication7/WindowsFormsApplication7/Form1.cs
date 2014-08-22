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

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();


        private void btnOpen_Click(object sender, EventArgs e)
        {

            ofd.Filter = "GBA File(*.gba)|*.gba";
            if (ofd.ShowDialog() == DialogResult.OK) 
            this.Text ="Loaded File: "+ ofd.FileName;
            BinaryReader br = new BinaryReader(File.OpenRead(ofd.FileName));
            string gamecode = null;
            for (int i = 0xAC; i <= 0xAF; i++)
            {
                br.BaseStream.Position = i;
                gamecode += br.ReadByte().ToString("X2");

            }
            br.Close();
            if (gamecode == "42505245")
            {
                label1.Text = "Loaded Game: Pokémon FireRed";
                btnEnable.Enabled = true;
                btnDisable.Enabled = true;
            }
            else
            {
                label1.Text = "Loaded Game: ???";
                btnEnable.Enabled = false;
                btnDisable.Enabled = false;
                MessageBox.Show ("The loaded game isn't supported");
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName));
            for (int x = 0xBD494; x <= 0xBD494; x++)
            {

                bw.BaseStream.Position = x;
                bw.Write(0x00);


            }
            bw.Close();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(ofd.FileName));
            for (int x = 0xBD494; x <= 0xBD494; x++)
            {

                bw.BaseStream.Position = x;
                bw.Write(0x08);


            }
            bw.Close();
        }
    }
}
