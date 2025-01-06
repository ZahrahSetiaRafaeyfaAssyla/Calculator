using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace calcu
{
    public partial class FormCalculator : Form
    {
        public FormCalculator()
        {
            InitializeComponent();
        }

        public string GetMacAddress()
        {
            var macAddr = (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            return macAddr;
        }

        // Function to save serial number to registry
        public void SaveSerialToRegistry(string serialNumber)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\MyApp");
            key.SetValue("SerialNumber", serialNumber);
            key.Close();
        }

        // Function to get serial number from registry
        public string GetSerialFromRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\MyApp");
            if (key != null)
            {
                return (string)key.GetValue("SerialNumber");
            }
            return null;
        }

        private decimal firstNum = 0.0m;
        private decimal seccondNum = 0.0m;
        private decimal resultNum = 0.0m;
        private int operatorType = 0;

        public enum Mathoperations
        {
            NoOperator = 0,
            tambah = 1,
            kurang = 2,
            bagi = 3,
            kali = 4,
            persen = 5
        }

        private void FormCalculator_Load(object sender, EventArgs e)
        {
            string serialNumber = "4127-7437-4686"; // Simulate user input
            string storedSerial = GetSerialFromRegistry();
            string macAddress = GetMacAddress();


            // Logic to check if serial is valid and associated with this MAC address
            if (storedSerial == null)
            {
                // If no serial is stored, save it along with MAC address
                SaveSerialToRegistry(serialNumber);
                MessageBox.Show("Serial Number saved!");
            }
            else if (storedSerial == serialNumber)
            {
                // If serial is already stored, validate it
                MessageBox.Show("Serial Number already registered on this machine!");
            }
            else
            {
                MessageBox.Show("Invalid Serial Number or this serial is used on another machine!");
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if(TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "1";
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "0";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text == "0")
            {
                TxtBoxTampilan.Clear();
            }
            TxtBoxTampilan.Text = TxtBoxTampilan.Text + "9";
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (!TxtBoxTampilan.Text.Contains("."))
            {
                TxtBoxTampilan.Text += ".";
            }
        }

        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (!TxtBoxTampilan.Text.Contains("-"))
            {
                TxtBoxTampilan.Text = "-" + TxtBoxTampilan.Text;
            }
            else
            {
                TxtBoxTampilan.Text = TxtBoxTampilan.Text.Trim('-');
            }

        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            TxtBoxTampilan.Text = "0";
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            SimpanNilaiPadaOperatorTipe((int)Mathoperations.tambah);
        }

        private void btnKurang_Click(object sender, EventArgs e)
        {
            SimpanNilaiPadaOperatorTipe((int)Mathoperations.kurang);
        }

        private void btnBagi_Click(object sender, EventArgs e)
        {
            SimpanNilaiPadaOperatorTipe((int)Mathoperations.bagi);
        }

        private void SimpanNilaiPadaOperatorTipe (int operation)
        {
            operatorType = operation;
            firstNum = Convert.ToDecimal(TxtBoxTampilan.Text);
            TxtBoxTampilan.Text = "0";
        }

        private void btnKali_Click(object sender, EventArgs e)
        {
            SimpanNilaiPadaOperatorTipe((int)Mathoperations.kali);
        }

        private void btnPersen_Click(object sender, EventArgs e)
        {
            SimpanNilaiPadaOperatorTipe((int)Mathoperations.persen);
        }

        private void btnJumlah_Click(object sender, EventArgs e)
        {
            seccondNum = Convert.ToDecimal(TxtBoxTampilan.Text);

            switch (operatorType) 
            {
                case (int)Mathoperations.tambah:
                    resultNum = firstNum + seccondNum;
                    break;
                case (int)Mathoperations.kurang:
                    resultNum = firstNum - seccondNum;
                    break;
                case (int)Mathoperations.bagi:
                    resultNum = firstNum / seccondNum;
                    break;
                case (int)Mathoperations.kali:
                    resultNum = firstNum * seccondNum;
                    break;
                case (int)Mathoperations.persen:
                    resultNum = (firstNum / seccondNum) *100;
                    break;
            }
            TxtBoxTampilan.Text = resultNum.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TxtBoxTampilan.Text = "0";
            firstNum = 0.0m;
            seccondNum = 0.0m;
            resultNum = 0;
            operatorType= (int)Mathoperations.NoOperator;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TxtBoxTampilan.Text.Length > 0)
            {
                TxtBoxTampilan.Text = TxtBoxTampilan.Text.Remove(TxtBoxTampilan.Text.Length - 1, 1);
            }

            if (TxtBoxTampilan.Text == "")
            {
                TxtBoxTampilan.Text = "0";
            }
        }

        private void TxtBoxTampilan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
