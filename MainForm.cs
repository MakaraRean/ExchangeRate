using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
                textBox.Text = textBox.Text.Replace(" ", "");
                if (ignoreTextbox != textBox)
                {
                    textBox.Text = (value * textBoxesAndRates[textBox]).ToString("0.##");
                    formart_number(textBox);
                }
            }
        }

        void checkLeadingZero(object sender)
        {
            TextBox textBox = sender as TextBox;

            if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text[0] == '0' && textBox.Text.Length > 1 && textBox.Text[1] != '.')
            {
                textBox.Text = textBox.Text.TrimStart('0');

                // Set the cursor position to the end of the text
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        void checkNumberInput(object sender, KeyPressEventArgs e)
        {
            // Check if the input is a digit, a control character, a period, or a space
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
            // Check if it's a period, and if one already exists in the TextBox
            else if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            // Check if there are more than two digits after the decimal point
            else if (e.KeyChar != '.' && ((sender as TextBox).Text.IndexOf('.') > -1) &&
                     ((sender as TextBox).Text.Length - (sender as TextBox).Text.IndexOf('.')) > 2)
            {
                e.Handled = true;
            }
        }


        void formart_number(TextBox number, bool cusorCheck = false)
        {
            decimal num;
            decimal.TryParse(number.Text.Replace(" ", ""),out num);
            var cultureInfo = new CultureInfo(CultureInfo.CurrentCulture.Name);
            cultureInfo.NumberFormat.NumberGroupSeparator = " ";
            cultureInfo.NumberFormat.NumberGroupSizes = new int[] { 3 };
            var formattedNumber = num.ToString("N0", cultureInfo);
            number.Text = formattedNumber;

            // Move cusor to last of text
            if (cusorCheck)
            {
                number.Select(number.Text.Length, 0);

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
            else if (double.TryParse(txtUsd.Text.Replace(" ", ""), out double val))
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
            else if (double.TryParse(txtKhr.Text.Replace(" ", ""), out double khrInput))
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
            else if (double.TryParse(txtEur.Text.Replace(" ", ""), out double eurInput))
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
            else if (double.TryParse(txtBah.Text.Replace(" ", ""), out double bahInput))
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
            else if (double.TryParse(txtAud.Text.Replace(" ", ""), out double audInput))
            {
                double usdRate = audInput / audRate;
                calcutate(usdRate, txtAud);
            }
        }

        private void txtUsd_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkNumberInput(sender, e);
            //formart_number(sender as TextBox, true, e.KeyChar == '.');
        }

        private void txtUsd_TextChanged(object sender, EventArgs e)
        {
            checkLeadingZero(sender);
            
        }

        private void txtKhr_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkNumberInput(sender, e);
        }

        private void txtKhr_TextChanged(object sender, EventArgs e)
        {
            checkLeadingZero(sender);
        }

        private void txtEur_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkNumberInput(sender, e);
        }

        private void txtEur_TextChanged(object sender, EventArgs e)
        {
            checkLeadingZero(sender);
        }

        private void txtBah_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkNumberInput(sender, e);
        }

        private void txtBah_TextChanged(object sender, EventArgs e)
        {
            checkLeadingZero(sender);
        }

        private void txtAud_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkNumberInput(sender, e);
        }

        private void txtAud_TextChanged(object sender, EventArgs e)
        {
            checkLeadingZero(sender);
        }
    }
}
