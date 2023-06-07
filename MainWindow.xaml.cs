using System;
using System.Collections;
//hello. GIT test
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SUM_ITcalculator_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void BasicOperations(string Operation)
        {
            try
            {

                string[] basicOperations = new string[2];
                bool hasPlus = previewOperationsPlane.Text.Contains('+');
                bool hasMinus = previewOperationsPlane.Text.Contains("-");
                bool hasMultiply = previewOperationsPlane.Text.Contains("*");
                bool hasDivide = previewOperationsPlane.Text.Contains("/");

                //mild operations
                bool hasSquare = previewOperationsPlane.Text.Contains("^(");
                bool hasRoot = previewOperationsPlane.Text.Contains("SQRT(");
                bool hasModulus = previewOperationsPlane.Text.Contains("MOD(");
             
                //unary operations
                bool hasTan = previewOperationsPlane.Text.Contains("TAN(");
                bool hasCos = previewOperationsPlane.Text.Contains("COS(");
                bool hasSin = previewOperationsPlane.Text.Contains("SIN(");


            if (hasPlus || hasMinus || hasDivide || hasMultiply || hasSquare || hasRoot || hasModulus)//
            {


                string ActiveOperation;
                int OperatorLength = 0;
                if (hasPlus)
                {
                    ActiveOperation = "+";
                }
                else if(hasMinus)
                {
                    ActiveOperation = "-";
                }
                else if(hasMultiply)
                {
                    ActiveOperation = "*";
                }
                else if (hasDivide)
                {
                    ActiveOperation = "/";
                }

                //mild operations
                else if (hasSquare)
                    {
                        ActiveOperation = "^(";
                        OperatorLength = ActiveOperation.Length;
                    }
                else if (hasRoot)
                {
                    ActiveOperation= "SQRT(";
                    OperatorLength += ActiveOperation.Length;
                }
                else if (hasModulus)
                {
                    ActiveOperation = "MOD(";
                    OperatorLength += ActiveOperation.Length;
                }
                else
                {
                    ActiveOperation = "*";
                    OperatorLength += ActiveOperation.Length;
                }
                /*! NOTICE‼️‼️ there is a simpler way of doing this following code (the one iko next). I've commented it for
                future reference*/
                // (the commented out)basicOperations[0] = previewOperationsPlane.Text.Substring(0, (previewOperationsPlane.Text.IndexOf(ActiveOperation)));
                basicOperations[0] = previewOperationsPlane.Text[..previewOperationsPlane.Text.IndexOf(ActiveOperation)];

                
                    //now I get the index of the operation sign; i then use it to capture the numbers that come after it
                int OperatorIndex = previewOperationsPlane.Text.IndexOf(ActiveOperation);//
                if (hasPlus || hasMinus || hasDivide || hasMultiply)
                {
                    // first number(before the opertor)
                    basicOperations[1] = previewOperationsPlane.Text.Substring(OperatorIndex + 1);
                }
                else
                {
                    //second number (after the opertor)
                    basicOperations[1] = previewOperationsPlane.Text.Substring(OperatorIndex + OperatorLength);
                }


                if (hasPlus)
                {
                    float Response = float.Parse(basicOperations[0]) + float.Parse(basicOperations[1]);
                    ResponsePlane.Text = Response.ToString();
                }
                else if (hasMinus)
                {
                    float Response = float.Parse(basicOperations[0]) - float.Parse(basicOperations[1]);
                    ResponsePlane.Text = Response.ToString();
                }
                else if (hasMultiply)
                {
                    float Response = float.Parse(basicOperations[0]) * float.Parse(basicOperations[1]);
                    ResponsePlane.Text = Response.ToString();
                }
                else if (hasDivide)
                {
                    float Response = float.Parse(basicOperations[0]) / float.Parse(basicOperations[1]);
                    ResponsePlane.Text = Response.ToString();
                }

                //mild operations
                else if (hasSquare)
                {
                    float val1 = float.Parse(basicOperations[0]);
                    for (int k = 2; k <= int.Parse(basicOperations[1]); k++)
                    {
                        float val2 = float.Parse(basicOperations[0]);
                        val1 *= val2;
                        ResponsePlane.Text = val1.ToString();
                    }
                }
                else if (hasRoot)
                {
                    // float val = ((float)(1 / int.Parse(basicOperations[1]) * Math.Log10(int.Parse(basicOperations[0]))));
                    double val = Math.Pow(float.Parse(basicOperations[0]), (1/float.Parse(basicOperations[1])));
                    ResponsePlane.Text = val.ToString();
                }
                else if (hasModulus)
                {
                    int Remainder = int.Parse(basicOperations[0]) % int.Parse(basicOperations[1]);
                    ResponsePlane.Text = Remainder.ToString();
                }
                else
                {
                    ResponsePlane.Text = "Unhandled Error Occured";
                }

                //int Response = int.Parse(basicOperations[0]) + int.Parse(basicOperations[1]);
                //ResponsePlane.Text = Response.ToString();
            }

            //! unary operations (sort of)
            else if (hasTan || hasSin || hasCos)
            {
                bool IsMultiplied = false;
                string ActiveCommand;
                if (hasTan) { ActiveCommand = "TAN("; }
                else if (hasSin) { ActiveCommand = "SIN("; }
                else if (hasCos) { ActiveCommand = "COS("; }
                else { ActiveCommand = string.Empty; }
                //! the following commented out code is a longer method of the next
                //int startVal = int.Parse(previewOperationsPlane.Text.Substring(0));
                string startVal = previewOperationsPlane.Text.Substring(0,1);
                int OperatorIndex = previewOperationsPlane.Text.IndexOf(ActiveCommand);
                int val1;
                if (startVal != ActiveCommand.Substring(0,1))
                {
                    val1 = int.Parse(previewOperationsPlane.Text[..OperatorIndex]);
                    IsMultiplied= true;
                }
                else
                {
                    val1 = 1;
                }

                int commandLength = ActiveCommand.Length; 
                double Radians = Math.PI / 180.0;
                float response;
                int index_after_command_and_val1;
                if (hasCos)
                {
                    if (IsMultiplied)
                    {
                        index_after_command_and_val1 = (val1.ToString().Length) + commandLength;
                    }
                    else
                    {
                        index_after_command_and_val1 = commandLength;
                    }
                    
                    float value = float.Parse(previewOperationsPlane.Text[index_after_command_and_val1..]);
                    //! Math.Cos takes values in radians
                    response = (float)Math.Cos(value * Radians);
                }
                else if (hasSin)
                {
                    if (IsMultiplied)
                    {
                        index_after_command_and_val1 = (val1.ToString().Length) + commandLength;
                    }
                    else
                    {
                        index_after_command_and_val1 = commandLength;
                    }
                    float value = float.Parse(previewOperationsPlane.Text[index_after_command_and_val1..]);
                    //! Math.Sin takes values in radians
                    response = (float)Math.Sin(value * Radians);
                }
                else if (hasTan)
                {
                    //! the following code was simplified into the next code. removed the substring part
                    //float value = float.Parse(previewOperationsPlane.Text.Substring(commandLength));
                    if (IsMultiplied)
                    {
                        index_after_command_and_val1 = (val1.ToString().Length) + commandLength;
                    }
                    else
                    {
                        index_after_command_and_val1 = commandLength;
                    }
                    float value = float.Parse(previewOperationsPlane.Text[index_after_command_and_val1..]);
                    //! Math.Tan takes values in radians
                    response = (float)Math.Tan(value * Radians);
                }
                else
                {
                    response = 0;
                }

                if (IsMultiplied)
                {
                    float finalResponse = val1 * response;
                    ResponsePlane.Text = finalResponse.ToString();
                    previewOperationsPlane.Text = finalResponse.ToString();
                }
                else
                {
                    ResponsePlane.Text = response.ToString();
                    previewOperationsPlane.Text = response.ToString();
                }
            }


            else if (Operation == "Execute")
                {
                    ResponsePlane.Text = previewOperationsPlane.Text;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("INPUT ERROR \n" + e.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(previewOperationsPlane.Text == "Preview") { previewOperationsPlane.Clear(); }
            
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "1";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "1";
            }
            else
            {
                InputPlane.Text = "1";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "1";
            }
        }
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            if (previewOperationsPlane.Text == "Preview") { previewOperationsPlane.Clear(); }

            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "2";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "2";
            }
            else
            {
                InputPlane.Text = "2";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "2";
            }
        }
        private void Btn3_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "3";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "3";
            }
            else
            {
                InputPlane.Text = "3";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "3";
            }
        }
        private void Btn4_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "4";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "4";
            }
            else
            {
                InputPlane.Text = "4";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "4";
            }
        }
        private void Btn5_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "5";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "5";
            }
            else
            {
                InputPlane.Text = "5";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "5";
            }
        }
        private void Btn6_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "6";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "6";
            }
            else
            {
                InputPlane.Text = "6";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "6";
            }
        }
        private void Btn7_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "7";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "7";
            }
            else
            {
                InputPlane.Text = "7";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "7";
            }
        }
        private void Btn8_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "8";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "8";
            }
            else
            {
                InputPlane.Text = "8";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "8";
            }
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Clear();
            }
            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "9";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "9";
            }
            else
            {
                InputPlane.Text = "9";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "9";
            }
        }

        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            if (previewOperationsPlane.Text == "Preview") { previewOperationsPlane.Clear(); }

            if (InputPlane.Text != "0")
            {
                InputPlane.Text += "0";
                previewOperationsPlane.Text = previewOperationsPlane.Text += "0";
            }
            else
            {
                InputPlane.Text = "0";
            }
        }

        private void DPbtn_Click(object sender, RoutedEventArgs e)
        {
            if (previewOperationsPlane.Text == "Preview") 
            { 
                previewOperationsPlane.Clear();
                if(InputPlane.Text == "0") { previewOperationsPlane.Text = "0"; }
            }
            if (InputPlane.Text.Contains("."))
            {
                ResponsePlane.Text = "Syntax Error";
            }
            else
            {
                InputPlane.Text += ".";
                previewOperationsPlane.Text = previewOperationsPlane.Text += ".";
            }
        }

        private void ACbtn_Click(object sender, RoutedEventArgs e)
        {
            ResponsePlane.Text="0";
            InputPlane.Text="0";
            previewOperationsPlane.Text="Preview";

        }


//OPERATORS START HERE 😃😃


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //plus sign
            BasicOperations("+");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "+";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "+";
                }
            }
            
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            //minus sign
            BasicOperations("-");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "-";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "-";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "-";
                }
            }
            
        }

        private void MultiplyBtn_Click(object sender, RoutedEventArgs e)
        {
            //multiply sign
            BasicOperations("*");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "*";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "*";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "*";
                }
            }
        }

        private void DivideBtn_Click(object sender, RoutedEventArgs e)
        {
            //divide sign
            BasicOperations("/");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "/";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "/";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "/";
                }
            }
        }

        // ANSWER BTN HERE 😃😃😃👌
        private void AnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            BasicOperations("Execute");
        }
        // MORE COMPLEX OPERATIONS (STILL BASIC THO😜)
        private void SqrBtn_Click(object sender, RoutedEventArgs e)
        {
            BasicOperations("SQR");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "^(";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "^(";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "^(";
                }
            }
        }

        private void SqrRootBtn_Click(object sender, RoutedEventArgs e)
        {
            BasicOperations("SQRT");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "SQRT(";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "SQRT(";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "SQRT(";
                }
            }
        }

        private void CloseBracketBtn_Click(object sender, RoutedEventArgs e)
        {
            BasicOperations("Execute");
            previewOperationsPlane.Text = ResponsePlane.Text;
        }

        private void TanBtn_Click(object sender, RoutedEventArgs e)
        {
            //! TAN command
            BasicOperations("TAN");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = "TAN(";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "TAN(";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "TAN(";
                }
            }
        }

        private void CosBtn_Click(object sender, RoutedEventArgs e)
        {
            //! COSINE command
            BasicOperations("COS");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = "COS(";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "COS(";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "COS(";
                }
            }
        }

        private void SinBtn_Click(object sender, RoutedEventArgs e)
        {
            //! SINE command
            BasicOperations("SIN");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = "SIN(";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "SIN(";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "SIN(";
                }
            }
        }

        private void modulusBtn_Click(object sender, RoutedEventArgs e)
        {
            BasicOperations("MODULUS");
            InputPlane.Text = "0";
            if (previewOperationsPlane.Text == "Preview")
            {
                previewOperationsPlane.Text = InputPlane.Text + "MOD(";
            }
            else
            {
                //previewOperationsPlane.Text = previewOperationsPlane.Text += "+";
                if (ResponsePlane.Text == "0")
                {
                    previewOperationsPlane.Text = previewOperationsPlane.Text += "MOD(";
                }
                else
                {
                    previewOperationsPlane.Text = ResponsePlane.Text + "MOD(";
                }
            }
        }
    }
}
