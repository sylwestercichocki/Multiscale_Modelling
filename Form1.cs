using Multiscale_Modeling.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiscale_Modeling
{
    public partial class Form1 : Form
    {
        class ComboItem
        {
            public int ID { get; set; }
            public string Text { get; set; }
        }

        Controller controller;
        public Form1()
        {
            InitializeComponent();
            controller = new Controller();
            pictureBox1.Image = controller.bm;
            inclusionType.DataSource = new ComboItem[]
            {
                new ComboItem{ ID = 1, Text = "Square"},
                new ComboItem{ ID = 2, Text = "Circle"}
            };
            NeighborhoodType.DataSource = new ComboItem[]
            {
                new ComboItem{ ID = 1, Text = "Von Neumann" },
                new ComboItem{ ID = 2, Text = "Advanced Moore"}
            };

            Propability.Enabled = false;
        }

        private void seed_button_Click(object sender, EventArgs e)
        {
            
            pictureBox1.Image = controller.GenerateSeeds(Int32.Parse(numOfNucleations.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((int)NeighborhoodType.SelectedValue == 1)
            {
                pictureBox1.Image = controller.update((int)NeighborhoodType.SelectedValue, 0);

            }
            if ((int)NeighborhoodType.SelectedValue == 2)
            {
                pictureBox1.Image = controller.update((int)NeighborhoodType.SelectedValue, (int)NeighborhoodType.SelectedValue);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                controller.bm.Save(dialog.FileName + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                controller.saveTxt(dialog.FileName + ".txt");
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                var filepath = fileDialog.FileName;
                if (filepath.Contains(".txt"))
                {
                    controller.loadTxt(filepath);
                    pictureBox1.Image = controller.bm;
                }
                else
                {
                    controller.loadBmp(filepath);
                   // controller.bm = (Bitmap)Image.FromFile(filepath);

                    pictureBox1.Image = controller.bm;
                }
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            controller.clear();
            pictureBox1.Image = controller.bm;
        }

        private void generateInclusions_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = controller.GenerateInclusions(Int32.Parse(numOfInclusions.Text), Int32.Parse(sizeOfInclusions.Text), (int)inclusionType.SelectedValue );
        }

        private void NeighborhoodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)NeighborhoodType.SelectedValue == 1)
            {
                Propability.Enabled = false;
            }
            if ((int)NeighborhoodType.SelectedValue == 2)
            {
                Propability.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            Console.WriteLine(coordinates.X + " " + coordinates.Y);
            controller.selectGrains(coordinates);
        }

        private void Substructure_Click(object sender, EventArgs e)
        {
            controller.substructure();
            pictureBox1.Image = controller.bm;
        }

        private void DualPhase_Click(object sender, EventArgs e)
        {
            controller.dualPhase();
            pictureBox1.Image = controller.bm;
        }

        private void SingleGrain_Click(object sender, EventArgs e)
        {
            controller.singleGrain();
            pictureBox1.Image = controller.bm;
        }

        private void AllGrains_Click(object sender, EventArgs e)
        {
            controller.allGrains();
            pictureBox1.Image = controller.bm;
        }

        private void GenRand_Click(object sender, EventArgs e)
        {
            controller.genMC(Int32.Parse(N.Text));
            pictureBox1.Image = controller.bm;
        }

        private void RunMC_Click(object sender, EventArgs e)
        {
            controller.updateMC(Int32.Parse(Niter.Text));
            pictureBox1.Image = controller.bm;
        }
    }
}
