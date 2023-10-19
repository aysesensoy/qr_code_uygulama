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
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;


namespace qr_code_okuma
{
    public partial class Form1 : Form
    {
        FilterInfoCollection webcams;
        VideoCaptureDevice cam;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webcams = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo dev in webcams )
            {
                comboBox1.Items.Add( dev.Name );
                
            }
            
        }
        private void cam_NewCam (object sender, NewFrameEventArgs EventArgs) 
        { 
            pictureBox1.Image = ((Bitmap)EventArgs.Frame.Clone());
        }

        private void kamera_ac_Click(object sender, EventArgs e)
        {
            cam =new VideoCaptureDevice(webcams[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewCam);
            cam.Start();
        }

        private void tara_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BarcodeReader barcod = new BarcodeReader();
            if (pictureBox1.Image!=null)
            {
                Result res = barcod.Decode((Bitmap)pictureBox1.Image);
                try
                {
                    string dec = res.ToString().Trim();
                    if (dec != "") 
                    { 
                        timer1.Stop();
                        textBox1.Text = dec;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cam != null) 
                if (cam.IsRunning == true) 
                { 
                    cam.Stop();
                }  
           
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            Form2 ekle = new Form2();
            ekle.Show();
            this.Hide();
        }
    }
}
