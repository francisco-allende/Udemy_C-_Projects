using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class mainCalculator : Form
    {
        //Declaro mis variables de scope global. Asi almaceno por largo tiempo en memoria.
        string selectedOperator;
        int firstAcumulatedValue;
        int secondAcumulatedValue;
        int result;

        public mainCalculator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
    
        }
      
        
        //Abstraccion de los botones
        private void BtnNumberAction_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender; //parseo estructuras (tipos de objetos) como c a lo malloc. 

            //Quiero acumular los numeros, sino solo escribe uno y borra el otro
            //Voy a crear un acumulador, antes parseando todo a enteros

            int buttonValue = int.Parse(button.Text); //valor enviado por parametro
            int currentTextBoxValue = int.Parse(textBoxValue.Text); //valor actual 

            //Asi es como muevo un dijito de posicion. Multiplico por 10, estoy en base decimal.
            currentTextBoxValue = (currentTextBoxValue * 10) + buttonValue;
            textBoxValue.Text = currentTextBoxValue.ToString();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            //ademas de borrar el texto, vacio los numeros y operandos acumulados en memoria
            firstAcumulatedValue = 0;
            secondAcumulatedValue = 0;
            selectedOperator = string.Empty; //asi vacio un string de forma prolija 
            MiniTextBox.Text = string.Empty;
            textBoxValue.Text = "0";
        }

        private void BtnOperador_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            selectedOperator = button.Text; //Accedo al valor del boton operando. Lo almaceno en memoria, queda ahi
            firstAcumulatedValue = int.Parse(textBoxValue.Text); //acumulo el valor hasta el momenton ingresado
            textBoxValue.Text = "0"; //vacio con 0 asi puedo ingresar el nuevo 
            Mini_TextChanged();
        }

        //muestra la cuenta anterior y el operando
        public void Mini_TextChanged()
        {
            MiniTextBox.Text = $"{firstAcumulatedValue} {selectedOperator} {secondAcumulatedValue}";
        }

        private void BtnIgual_Click(object sender, EventArgs e)
        {
            secondAcumulatedValue = int.Parse(textBoxValue.Text);
            Mini_TextChanged();

            switch (selectedOperator)
            {
                case "+":
                    result = firstAcumulatedValue + secondAcumulatedValue;
                    break;
                case "-":
                    result = firstAcumulatedValue - secondAcumulatedValue;
                    break;
                case "x":
                    result = firstAcumulatedValue * secondAcumulatedValue;
                    break;
                case "/":
                    result = firstAcumulatedValue / secondAcumulatedValue;
                    break;
            }

            //Imprimo el resultado 
            textBoxValue.Text = result.ToString();
        }
    }
}
