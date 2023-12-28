using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExchangeRate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            txtKhrRate.Text = khrRate.ToString();
            txtEurRate.Text = eurRate.ToString();
            txtBahRate.Text = bahRate.ToString();
            txtAudRate.Text = audRate.ToString();
        }
        // Main exchange rate
        double usdRate = 1;
        double khrRate = 4128;
        double eurRate = 0.96;
        double bahRate = 33.20;
        double audRate = 1.11;


        void calcutate(double value, TextBox ignoreTextbox)
        {
            Dictionary<TextBox, double> textBoxesAndRates = new Dictionary<TextBox, double>
            {
                { txtAud, audRate },
                { txtKhr, khrRate },
                { txtEur, eurRate },
                { txtBah, bahRate },
                { txtUsd, usdRate }
            };

            foreach (var textBox in textBoxesAndRates.Keys)
            {
                if (ignoreTextbox != textBox)
                {
                    textBox.Text = (value * textBoxesAndRates[textBox]).ToString("0.##");
                }
            }
        }

        void clear()
        {
            txtUsd.Clear();
            txtKhr.Clear();
            txtEur.Clear();
            txtBah.Clear();
            txtAud.Clear();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtKhrRate.Text, out khrRate) &&
                double.TryParse(txtEurRate.Text, out eurRate) &&
                double.TryParse(txtBahRate.Text, out bahRate) &&
                double.TryParse(txtAudRate.Text, out audRate))
            {
                clear();
                MessageBox.Show("Exchange rate successfully updated.");
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }

        private void txtUsd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                clear();
            }
            else if (double.TryParse(txtUsd.Text, out double val))
            {
                calcutate(val, txtUsd);
            }
        }

        private void txtKhr_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                clear();
            }
            else if (double.TryParse(txtKhr.Text, out double khrInput))
            {
                double usdRate = khrInput / khrRate;
                calcutate(usdRate, txtKhr);
            }
        }

        private void txtEur_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                clear();
            }
            else if (double.TryParse(txtEur.Text, out double eurInput))
            {
                double usdRate = eurInput / eurRate;
                calcutate(usdRate, txtEur);
            }
        }

        private void txtBah_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                clear();
            }
            else if (double.TryParse(txtBah.Text, out double bahInput))
            {
                double usdRate = bahInput / bahRate;
                calcutate(usdRate, txtBah);
            }
        }

        private void txtAud_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                clear();
            }
            else if (double.TryParse(txtAud.Text, out double audInput))
            {
                double usdRate = audInput / audRate;
                calcutate(usdRate, txtAud);
            }
        }
    }
}
