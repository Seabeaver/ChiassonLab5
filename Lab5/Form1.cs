//jacob chiasson, https://github.com/Seabeaver/ChiassonLab5
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name:
         * Date: November 2024
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: 
         * */

        //class-level random object
        Random rand = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Select();

            //add your name to end of form title
            this.Text += "Jacob Chiasson";
            
        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2;
            //call ftn RollDice, placing returned number into integers
            dice1 = RollDice();
            dice2 = RollDice();
            //place integers into labels
            lblDice1.Text = dice1.ToString();
            lblDice2.Text = dice2.ToString();

            // call ftn GetName sending total and returning name
            int total = dice1 + dice2;
            lblRollName.Text = GetName(total);

            //display name in label

        }
        private void ClearOneRoll() {
            /* Name: ClearOneRoll
            *  Sent: nothing
            *  Return: nothing
            *  Clear the labels */
            lblDice1.Text = "";
            lblDice2.Text = "";
            lblRollName.Text = "";
        }

        private void ClearStats() {
            /* Name: ClearStats
            *  Sent: nothing
            *  Return: nothing
            *  Reset nud to minimum value, chkbox unselected, 
            *  clear labels and listbox */
            nudNumber.Value = 50;
            chkSeed.Checked = false;
            lstMarks.Items.Clear();
            lblPass.Text = "";
            lblFail.Text = "";
            lblAverage.Text = "";
        }

        private int RollDice()
        {
            /* Name: RollDice
            * Sent: nothing
            * Return: integer (1-6)
            * Simulates rolling one dice */
            
            return rand.Next(1, 7);
        }

        private string GetName(int diceTotal)
        {
            /* Name: GetName
            * Sent: 1 integer (total of dice1 and dice2) 
            * Return: string (name associated with total) 
            * Finds the name of dice roll based on total.
            * Use a switch statement with one return only
            * Names: 2 = Snake Eyes
            *        3 = Litle Joe
            *        5 = Fever
            *        7 = Most Common
            *        9 = Center Field
            *        11 = Yo-leven
            *        12 = Boxcars
            * Anything else = No special name*/

            string name = "";
            switch (diceTotal)
            {
                case 2:
                    name = "Snake Eyes";
                    break;
                case 3:
                    name = "Litle Joe";
                    break;
                case 5:
                    name = "Fever";
                    break;
                case 7:
                    name = "Most Common";
                    break;
                case 9:
                    name = "Center Field";
                    break;
                case 11:
                    name = "Yo-leven";
                    break;
                case 12:
                    name = "Boxcars";
                    break;
                default:
                    name = "";
                    break;
            }
            return name;
        }

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            //call ftn DataPresent twice sending string returning boolean
            bool dice1Valid = DataPresent(lblDice1.Text);
            bool dice2Valid = DataPresent(lblDice2.Text);
            //if data present in both labels, call SwapData sending both strings
            if (dice1Valid && dice2Valid) {
                SwapData(lblDice1.Text, lblDice2.Text);
            }
            else
            {
                //if data not present in either label display error msg
                MessageBox.Show("Roll the dice", "Data Missing");
            }
            //put data back into labels

            
        }

        private bool DataPresent(string data)
        {
            /* Name: DataPresent
            * Sent: string
            * Return: bool (true if data, false if not) 
            * See if string is empty or not*/
            if (data != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void SwapData(string dice1, string dice2)
        {
            /* Name: SwapData
            * Sent: 2 strings
            * Return: none 
            * Swaps the memory locations of two strings*/
            lblDice1.Text = dice2;
            lblDice2.Text = dice1;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //declare variables and array
            int number = Convert.ToInt32(nudNumber.Value);
            double[] intArray = new double[number];
            //check if seed value
            if (chkSeed.Checked == true)
            {
                rand = new Random(1000);
            }
            lstMarks.Items.Clear();

            //fill array using random number
            int i = 0;
            while (i < number)
            {
                intArray[i] = rand.Next(40, 101);
                i++;
            }
            //call CalcStats sending and returning data
            int passNumber = 0;
            double average = CalcStats(intArray, number, ref passNumber);

            //display data sent back in labels - average, pass and fail
            // Format average always showing 2 decimal places 
            lblAverage.Text = average.ToString();
            lblPass.Text = passNumber.ToString();
            lblFail.Text = (number - passNumber).ToString();

        } // end Generate click

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            DialogResult Selection = MessageBox.Show("Are you sure you want a seed value?", "confirm seed value", MessageBoxButtons.YesNo);
            if (Selection == DialogResult.No)
            {
                chkSeed.Checked = false;
            }
        }

        private double CalcStats(double[] array, int arrayLimit, ref int passNumber)
        {
            double average = 0;
            foreach (int value in array)
            {
                lstMarks.Items.Add(value.ToString());
                average += value;
                if (value >= 60){
                    passNumber++;
                }
            }
            average /= arrayLimit;
            return average;

            /* Name: CalcStats
            * Sent: array and 2 integers
            * Return: average (double) 
            * Run a foreach loop through the array.
            * Passmark is 60%
            * Calculate average and count how many marks pass and fail
            * The pass and fail values must also get returned for display*/
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOneRoll.Checked)
            {
                ClearOneRoll();
                grpMarkStats.Hide();
                grpOneRoll.Show();
            }
            if (radRollStats.Checked)
            {
                ClearStats();
                grpOneRoll.Hide();
                grpMarkStats.Show();
            }
        }
    }
}
