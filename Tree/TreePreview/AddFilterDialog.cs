using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreePreview {
	public partial class AddFilterDialog : Form {
		private filter filterChosen;
        public filter getFilterChosen
        {
            get
            {
                return filterChosen;
            }
        }
        public AddFilterDialog() {
			InitializeComponent();
		}

        private void dodajFiltrClicked(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                filterChosen = new filter(1, textBox1.Text, textBox2.Text, comboBox1.SelectedIndex);
            }
            else if (radioButton2.Checked)
            {
                filterChosen = new filter(2, textBox3.Text, textBox4.Text, comboBox2.SelectedIndex);
            }
            else if (radioButton3.Checked)
            {
                filterChosen = new filter(3, textBox5.Text, "", -1);
            }
            else if (radioButton4.Checked)
            {
                filterChosen = new filter(4, textBox6.Text, "", -1);
            }
            Close();
        }

        private void anulujFiltrClicked(object sender, EventArgs e)
        {
            //filterChosen = null;      don't know what to do
            Close();
        }

	}

    public class filter
    {
        int numberSelected;
        string argString1, argString2;
        int Combo; //don't know type yet

        public filter(int nr, String tBox1, String tBox2, int cmb) {
            this.numberSelected = nr;
            this.argString1 = tBox1;
            this.argString2 = tBox2;
            this.Combo = cmb;
		}

        public override string ToString()
        {

            return argString1;// +" " + argString2;
        }
        /*
                
                fun = lisat => lisat.Where(n => (n.Data[textBox1] == null || n.Data[textBox1].Equals(textBox2) ));

        */
    }
}
