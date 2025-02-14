using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoSecurity
{
    public partial class Form1 : Form
    {

        public char[] cipherDictionary = { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
        public char[] plainDictionary = { 'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        public int[] randArr;
        public Form1()
        {
            InitializeComponent();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String plainText = textBox1.Text;
            if (plainText.Length < 1) {

                MessageBox.Show(this, "Cannot Encrypt Empty String","Exit Status 1");
                Environment.Exit(1);
            }
           
            plainText = plainText.Replace(" ","");
            //textBox2.Text = plainText;
            if (!plainText.All(c => char.IsUpper(c)))
            {
                MessageBox.Show(this, "Plain Text can only contain Upper Case Characters", "Error");
                Environment.Exit(1);
            }
            else {
                randArr = randomNumberArrayGenerator(plainText.Length);
                
                //foreach (int i in randArr)
                  //  MessageBox.Show(i.ToString());
                encryptMessage(plainText);
            }
        }
        private void encryptMessage(String plainText) {
            String C = "";
            for (int i = 0; i < plainText.Length; i++) {
                int numEq = posOfChar(plainText[i], plainDictionary);
                char ci = cipherImpliment(numEq, plainText.Length, randArr[i]);
                C = C + ci;
            }
            textBox2.Text = C;
        }
        private int posOfChar(char temp, char[] TempArr) {
            int numEq = -1;
            int j;

            for (j = 0; temp != TempArr[j]; j++)
                if (j >= 26) {
                    j = -2;
                    break; }
            if (j < 0 && j > 25)
            {
                MessageBox.Show(this, "Unrecognized Character", "Error");
                Environment.Exit(1);
            }
            numEq = j;
            return numEq;
        }

        private char cipherImpliment(int posOfPi, int length, int temp) {
            char ci = '\0';

            ci = cipherDictionary[(posOfPi+temp)%26];

            return ci;
        }
        private int[] randomNumberArrayGenerator(int length) {
            int[] temp = new int[length];
             //int[] randArr = new int[temp.Length];
            Random rand = new Random();
            for (int x = 0; x < length; x++)
            {
                temp[x] = rand.Next(0, 100);
                //MessageBox.Show(randomNumber.ToString());
                
            }

            temp = repeatChk(temp);
            //temp = repeatChk(temp);
            //temp = repeatChk(temp);

            return temp;
        }
        private int[] repeatChk(int[] Arr) {

            Random rand = new Random();
            for (int i = 0; i < Arr.Length; i++)
            {
                for (int j = i; j < Arr.Length; j++)
                {
                    if (Arr[i] == Arr[j])
                    {
                        Arr[j] = rand.Next(0, 100);
                    }
                }
            }

            return Arr;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void decrypt_Click(object sender, EventArgs e)
        {
            String cipherText = textBox2.Text;
            textBox1.Text = null;
            decryptMessage(cipherText);
            
        }
        private void decryptMessage(String cipherText) {
            String P = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                int numEq = posOfChar(cipherText[i], cipherDictionary);
                char pi = deCipherImpliment(numEq, cipherText.Length, randArr[i]);
                P = P + pi;
            }
            textBox1.Text = "DECRYPTED TEXT: "+P;
        }
        private char deCipherImpliment(int posOfPi, int length, int temp)
        {
            char pi = '\0';
            int index = -22222;
            int t = (posOfPi - temp) % 26;
            if (t < 0)
            {
                index = t + 26;
                pi = plainDictionary[index];
            }
            else
                pi = plainDictionary[t];

            return pi;
        }

        private void clrField_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
        }
    }
}
