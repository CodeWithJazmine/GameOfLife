using System.Windows.Forms;

namespace GOLProject
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();

        }

        public int Interval
        {
            get
            {
                return (int)timerUpDown.Value;
            }
            set
            {
                timerUpDown.Value = value;
            }
        }

        public int UniverseWidth
        {
            get
            {
                return (int)widthUpDown.Value;
            }
            set
            {
                widthUpDown.Value = value;
            }
        }
        public int UniverseHeight
        {
            get
            {
                return (int)heightUpDown.Value;
            }
            set
            {
                heightUpDown.Value = value;
            }
        }
    }
}
