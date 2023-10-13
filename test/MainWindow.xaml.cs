using System;
using System.Windows;
using System.Windows.Controls;
namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private string input = ""; private string operation = "";
        private long number1 = 0; private long number2 = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            input += button.Content.ToString();
            ResultTextBox.Text = input;
        }
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Content.ToString();
            number1 = long.Parse(input);
            input = ""; ResultTextBox.Text = operation;
        }
        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            number2 = long.Parse(input);
            long result = 0; switch (operation)
            {
                case "+":
                    result = (number1 + number2); 
                    break;
                case "-":
                    result = (number1 - number2);
                    break;
                case "*":
                    result = (number1 * number2);
                    break;
                case "/":
                    result = (number1 / number2);
                    break;
                case "^":
                    result = (long)Math.Pow(number1, number2); 
                    break;
                case "Корень":
                    result = (long)Math.Sqrt(number1);
                    break;
            }
            //ResultTextBox.Text = result.ToString(); 
            //input = result.ToString();
            // DisplayNumberAsText(result);
            if (result < 0)
            {
                ResultTextBox.Text = "минус " + DisplayNumberAsText(Math.Abs(result));
            }

            else
            {
                ResultTextBox.Text = DisplayNumberAsText((int)result);
            }

            input = result.ToString();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            input = ""; operation = "";
            number1 = 0; number2 = 0;
            ResultTextBox.Text = "";
        }

        public string DisplayNumberAsText(long number)
        {
            string[] units = { "", "один", "два", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять", "десять", "одиннадцать", "двенадцать", "тринадцать", "четырнадцать", "пятнадцать", "шестнадцать", "семнадцать", "восемнадцать", "девятнадцать" };
            string[] tens = { "", "десять", "двадцать", "тридцать", "сорок", "пятьдесят", "шестьдесят", "семьдесят", "восемьдесят", "девяносто" };
            string[] hundreds = { "", "сто", "двести", "триста", "четыреста", "пятьсот", "шестьсот", "семьсот", "восемьсот", "девятьсот" }; 
            string[] thousands = { "", "тысяча", "тысячи", "тысяч" };
            string[] numberOfthousands = { "", "одна", "две", "три", "четыре", "пять", "шесть", "семь", "восемь", "девять" };

            string result = "";

            if (number == 0)
            {
                return ResultTextBox.Text = "Ноль";
                //return;
            }
            if(number < 0 )
            {
                ResultTextBox.Text = "минус " + DisplayNumberAsText(Math.Abs(number)) ;
                //return;
            }
            if (number < -10000 || number > 10000)
            {
                return ResultTextBox.Text = "Число вне диапазона"; 
                //return;
            }
         
            else
            {
                long thousandsPart = number / 1000;//кол во тысяч
                long hundredsPart = (number % 1000) / 100;//сотен
                long tensPart = (number % 100) / 10;//десятков
                long onesPart = number % 10;//единиц


                if (thousandsPart >= 1)
                {
                    result += numberOfthousands[thousandsPart] +  " " + thousands[GetThousandsForm((int)thousandsPart)] + " ";
                }

                if (hundredsPart >= 1)
                {
                    result += hundreds[hundredsPart] + " ";
                }

                if (tensPart > 0 && tensPart != 1)
                {
                    result += tens[tensPart] + " ";
                }

                if (tensPart == 1)
                {
                    result += units[number % 100] + " ";
                }

                else if (onesPart > 0)
                {
                    result += units[onesPart] + " ";
                }
            }

            ResultTextBox.Text = result.TrimEnd();
            return result;

        }

        public static int GetThousandsForm(long number)
        {
            if (number % 10 == 1 && number % 100 != 11)
            {
                return 1; // Тысяча
            }
            else if (number % 10 >= 2 && number % 10 <= 4 && !(number % 100 >= 12 && number % 100 <= 14))
            {
                return 2; // Тысячи
            }
            else
            {
                return 3; // Тысяч
            }
        }
    }
}