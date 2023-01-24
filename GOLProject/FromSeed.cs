using System;
using System.Windows.Forms;

namespace GOLProject
{
    public partial class FromSeed : Form
    {
        public FromSeed()
        {
            InitializeComponent();
        }

        public int Seed
        {
            get
            {
                return (int)seedUpDown.Value;
            }
            set
            {
                seedUpDown.Value = value;
            }
        }

        private void randomizeButton_Click(object sender, System.EventArgs e)
        {
            Random rand = new Random();
            seedUpDown.Value = rand.Next(-100000000, 100000000);
        }
    }
}
