using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace BinaryConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int GetDataType(string data_type)
        {

            //0 - unsigned char
            //1 - signed char
            //2 - unsigned short
            //3 - signed short
            //4 - unsigned int
            //5 - signed int
            //6 - float
            //7 - double
            List<string> list_dat_type = new List<string>()
            {
                "Unsigned char",
                "Signed char",
                "Unsigned short",
                "Signed short",
                "Unsigned int",
                "Signed int",
                "Float",
                "Double"
            };
            for (int i = 0; i < list_dat_type.Count; i++)
                if (list_dat_type[i] == data_type)
                    return i;
            return -1;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public static bool IsOverMaxRange(string data, int type)
        {
            List<double> max_range = new List<double>() {
                byte.MaxValue,
                sbyte.MaxValue,
                ushort.MaxValue,
                short.MaxValue,
                uint.MaxValue,
                int.MaxValue,
                float.MaxValue,
                double.MaxValue
            };
            double check_max_value = 0;
            double.TryParse(data, out check_max_value);
            if (check_max_value > max_range[type])
                return true;
            return false;
        }

        public static bool IsOverMinRange(string data, int type)
        {
            List<double> min_range = new List<double>() {
                byte.MinValue,
                sbyte.MinValue,
                ushort.MinValue,
                short.MinValue,
                uint.MinValue,
                int.MinValue,
                float.MinValue,
                double.MinValue
            };
            double check_min_value = 0;
            double.TryParse(data, out check_min_value);
            if (check_min_value < min_range[type])
                return true;
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("From Hexadecimal");
            comboBox1.Items.Add("From Decimal");
            comboBox1.Items.Add("From Binary");

        }

        public static string ConvertDecimalToHex(string data_str, string data_type)
        {
            string hex_value = "0";
            int type = GetDataType(data_type);
            {
                if (IsOverMaxRange(data_str, type))
                {
                    hex_value = "F";
                    return hex_value;
                }
                if (IsOverMinRange(data_str, type))
                {
                    hex_value = "0";
                    return hex_value;
                }
                
            }
            double data_numeric = 0;
            bool isNumeric = double.TryParse(data_str, out data_numeric);
            int hex_size = 0;
            if (isNumeric)
            {
                if(type == 0)   //unsigned char/byte
                {
                    byte number_byte = (byte)(data_numeric);
                    hex_value = number_byte.ToString("X");
                    hex_size = 2;
                }
                else if(type == 1)  //signed char/byte
                {
                    sbyte number_sbyte = (sbyte)(data_numeric);
                    hex_value = number_sbyte.ToString("X");
                    hex_size = 2;
                }
                else if(type == 2)  //unsigned short
                {
                    ushort number_ushort = (ushort)(data_numeric);
                    hex_value = number_ushort.ToString("X");
                    hex_size = 4;
                }   
                else if(type == 3)  //short
                {
                    short number_short = (short)(data_numeric);
                    hex_value = number_short.ToString("X");
                    hex_size = 4;
                }
                else if(type == 4)  //usigned int
                {
                    uint number_uint = (uint)(data_numeric);
                    hex_value = number_uint.ToString("X");
                    hex_size = 8;
                }
                else if(type ==5)   //int
                {
                    int number_int = (int)(data_numeric);
                    hex_value = number_int.ToString("X");
                    hex_size = 8;
                }
                else if(type == 6)
                {
                    float number_float = (float)(data_numeric);
                    int tmp = BitConverter.ToInt32(BitConverter.GetBytes(number_float), 0);
                    hex_value = tmp.ToString("X");
                    hex_size = 8;
                }
                else
                {
                    long tmp = BitConverter.DoubleToInt64Bits(data_numeric);
                    hex_value = tmp.ToString("X");
                    hex_size = 16;
                }
            }
            while(hex_size > hex_value.Length)
                hex_value = "0" + hex_value;
            return hex_value;
        }

        public static bool IsInvalidHexNumber(ref string hex_value, int type)
        {
            hex_value = hex_value.ToUpper();
            List<char> list_char_hex = new List<char>()
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
            };
            for (int i = 0; i < hex_value.Length; i++)
                if (!list_char_hex.Contains(hex_value[i]))
                    return false;
            int hex_size = 0;
            if (type == 0 || type == 1) hex_size = 2;
            else if (type == 2 || type == 3) hex_size = 4;
            else if (type == 4 || type == 5 || type == 6) hex_size = 8;
            else if (type == 7) hex_size = 16;

            while (hex_value.Length < hex_size)
                hex_value = "0" + hex_value;
            return true;
        }

        public static string ConvertHexToBinary(string hex_value)
        {

            string bit_sequence = "";
            int count = 0;
            for(int i=0; i<hex_value.Length; i++)
            {
                switch (hex_value[i])
                {
                    case '0':
                        bit_sequence += "0000";
                        break;
                    case '1':
                        bit_sequence += "0001";
                        break;
                    case '2':
                        bit_sequence += "0010";
                        break;
                    case '3':
                        bit_sequence += "0011";
                        break;
                    case '4':
                        bit_sequence += "0100";
                        break;
                    case '5':
                        bit_sequence += "0101";
                        break;
                    case '6':
                        bit_sequence += "0110";
                        break;
                    case '7':
                        bit_sequence += "0111";
                        break;
                    case '8':
                        bit_sequence += "1000";
                        break;
                    case '9':
                        bit_sequence += "1001";
                        break;
                    case 'A':
                    case 'a':
                        bit_sequence += "1010";
                        break;
                    case 'B':
                    case 'b':
                        bit_sequence += "1011";
                        break;
                    case 'C':
                    case 'c':
                        bit_sequence += "1100";
                        break;
                    case 'D':
                    case 'd':
                        bit_sequence += "1101";
                        break;
                    case 'E':
                    case 'e':
                        bit_sequence += "1110";
                        break;
                    case 'F':
                    case 'f':
                        bit_sequence += "1111";
                        break;
                    default:
                        break;
                }
                count++;
                if(count == 2)
                {
                    bit_sequence += " ";
                    count = 0;
                }
            }
            return bit_sequence;
        }

        public static bool IsInvalidBitSequence(ref string data_bin, int type)
        {
            int bit_size = 0;
            switch (type)
            {
                case 0:
                    bit_size = 8;
                    break;
                case 1:
                    bit_size = 8;
                    break;
                case 2:
                    bit_size = 16;
                    break;
                case 3:
                    bit_size = 16;
                    break;
                case 4:
                    bit_size = 32;
                    break;
                case 5:
                    bit_size = 32;
                    break;
                case 6:
                    bit_size = 32;
                    break;
                case 7:
                    bit_size = 64;
                    break;
                default:
                    break;
            }
            if (data_bin.Length > bit_size)
                return false;
            for(int i=0; i<data_bin.Length; i++)
            {
                if (data_bin[i] != '0' && data_bin[i] != '1')
                    return false;

            }
            if (data_bin.Length < bit_size)
                while (data_bin.Length < bit_size)
                    data_bin = "0" + data_bin;
            return true;
        }

        public string ConvertBinaryToHex(string data_bin, string data_type)
        {
            string hex_value = "0";
            int type = GetDataType(data_type);
            if (IsInvalidBitSequence(ref data_bin, type))
            {
                string sub_bit_str = "";
                for(int i=0; i<data_bin.Length; i+=4)
                {
                    sub_bit_str = data_bin.Substring(i, 4);
                    switch (sub_bit_str)
                    {
                        case "0000":
                            hex_value += "0";
                            break;
                        case "0001":
                            hex_value += "1";
                            break;
                        case "0010":
                            hex_value += "2";
                            break;
                        case "0011":
                            hex_value += "3";
                            break;
                        case "0100":
                            hex_value += "4";
                            break;
                        case "0101":
                            hex_value += "5";
                            break;
                        case "0110":
                            hex_value += "6";
                            break;
                        case "0111":
                            hex_value += "7";
                            break;
                        case "1000":
                            hex_value += "8";
                            break;
                        case "1001":
                            hex_value += "9";
                            break;
                        case "1010":
                            hex_value += "A";
                            break;
                        case "1011":
                            hex_value += "B";
                            break;
                        case "1100":
                            hex_value += "C";
                            break;
                        case "1101":
                            hex_value += "D";
                            break;
                        case "1110":
                            hex_value += "E";
                            break;
                        case "1111":
                            hex_value += "F";
                            break;
                        default:
                            break;
                    }
                }
            }
            return hex_value;

        }
        public static string ConvertHexToDecimal(ref string hex_value, string data_type)
        {
            int type = GetDataType(data_type);
            string decimal_str = "0.0";
            if (IsInvalidHexNumber(ref hex_value, type))
            {
                if (type == 0)   //unsigned char/byte
                {
                    byte number_byte = Convert.ToByte(hex_value, 16);
                    decimal_str = number_byte.ToString();
                }
                else if (type == 1)  //signed char/byte
                {
                    sbyte number_sbyte = Convert.ToSByte(hex_value, 16);
                    decimal_str = number_sbyte.ToString();
                }
                else if (type == 2)  //unsigned short
                {
                    ushort number_ushort = Convert.ToUInt16(hex_value, 16);
                    decimal_str = number_ushort.ToString();
                }
                else if (type == 3)  //short
                {
                    short number_short = Convert.ToInt16(hex_value, 16);
                    decimal_str = number_short.ToString();
                }
                else if (type == 4)  //usigned int
                {
                    uint number_uint = Convert.ToUInt32(hex_value, 16);
                    decimal_str = number_uint.ToString();
                }
                else if (type == 5)   //int
                {
                    int number_int = Convert.ToInt32(hex_value, 16);
                    decimal_str = number_int.ToString();
                }
                else if (type == 6) //float
                {
                    int number_int = Convert.ToInt32(hex_value, 16);
                    float number_float = BitConverter.ToSingle(BitConverter.GetBytes(number_int), 0);
                    decimal_str = number_float.ToString();

                }
                else    //double
                {
                    long number_long = Convert.ToInt64(hex_value, 16);
                    double number_double = BitConverter.Int64BitsToDouble(number_long);
                    decimal_str = number_double.ToString();
                }

            }
                return decimal_str;
        }

        string data_type = "";
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton1.Checked;
            if (isChecked)
                data_type = radioButton1.Text;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton5.Checked;
            if (isChecked)
                data_type = radioButton5.Text;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton3.Checked;
            if (isChecked)
                data_type = radioButton3.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton2.Checked;
            if (isChecked)
                data_type = radioButton2.Text;
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {
            bool isChecked = radioButton4.Checked;
            if (isChecked)
                data_type = radioButton4.Text;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton6.Checked;
            if (isChecked)
                data_type = radioButton6.Text;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton7.Checked;
            if (isChecked)
                data_type = radioButton7.Text;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = radioButton8.Checked;
            if (isChecked)
                data_type = radioButton8.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(data_type == "")
            {
                MessageBox.Show("Please choose a data type!");
                return;
            }
            string type_convert = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            string decimal_str = textBoxDecimal.Text;
            string hex_str = textBoxHex.Text;
            string binary_str = textBoxBinary.Text;
            if(type_convert == "From Decimal")
            {
                hex_str = ConvertDecimalToHex(decimal_str, data_type);
                binary_str = ConvertHexToBinary(hex_str);
            }
            else if(type_convert == "From Hexadecimal")
            {
                decimal_str = ConvertHexToDecimal(ref hex_str, data_type);
                binary_str = ConvertHexToBinary(hex_str);
            }
            else if(type_convert == "From Binary")
            {
                hex_str = ConvertBinaryToHex(binary_str, data_type);
                decimal_str = ConvertHexToDecimal(ref hex_str, data_type);
            }
            else
            {
                MessageBox.Show("Please choose a type convert!");
                return;
            }
            textBoxBinary.Text = binary_str;
            textBoxDecimal.Text = decimal_str;
            textBoxHex.Text = hex_str;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
