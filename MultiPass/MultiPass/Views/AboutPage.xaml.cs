using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MultiPass.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            LoadDictionary();
        }

        public IList<int> WordList
        {
            get
            {
                return new List<int> { 4, 5, 6, 7, 8 };
            }
        }

        Dictionary<int, string> words = new Dictionary<int, string>();
        int wordNum = 4;
        string password = "";

        private void ButtonClick(object sender, EventArgs args)
        {
            password = "";
            wordCount();
            createWord();
            presentResult();
        }

        /// <summary>
        /// Assembles the suggested password
        /// </summary>
        private void createWord()
        {
            for (int counter = 0; counter < wordNum; counter++)
            {
                int value = RandomIntGenerator();
                while (words.ContainsKey(value) != true)
                {
                    value = RandomIntGenerator();
                }
                string holder = words[value].ToString();
                var capital = char.ToUpper(holder[0]) + holder.Substring(1);
                if (numCheck.IsChecked == true)
                {
                    capital = replaceWithNums(capital);
                }
                if (symCheck.IsChecked == true)
                {
                    capital = replaceWithSymbol(capital);
                }
                password += capital;
            }
        }

        /// <summary>
        /// Sets the number of words to the user specified value from the combo box
        /// </summary>
        private void wordCount()
        {
            int userNum = Int32.Parse(NumWords.SelectedIndex.ToString());
            switch (userNum)
            {
                case 0:
                    wordNum = 4;
                    break;
                case 1:
                    wordNum = 5;
                    break;
                case 2:
                    wordNum = 6;
                    break;
                case 3:
                    wordNum = 7;
                    break;
                case 4:
                    wordNum = 8;
                    break;
                default:
                    wordNum = 4;
                    break;
            }
        }

        /// <summary>
        /// Replaces certain characters with numbers. CNan be limited to a maximum number of replacements per word
        /// </summary>
        /// <param name="inputWord">the generated password</param>
        /// <returns></returns>
        private string replaceWithNums(string inputWord)
        {
            string outputString = "";
            int counter = 0;
            while (counter < 3)
            {
                foreach (char letter in inputWord)
                {

                    if (letter == 'a' && counter < 2 || letter == 'A' && counter < 2)
                    {
                        outputString += '4';
                        counter += 1;
                    }
                    else if (letter == 'b' && counter < 2 || letter == 'B' && counter < 2)
                    {
                        counter += 1;
                        outputString += '8';
                    }
                    else if (letter == 'e' && counter < 2 || letter == 'E' && counter < 2)
                    {
                        counter += 1;
                        outputString += '3';
                    }
                    else if (letter == 'g' && counter < 2 || letter == 'G' && counter < 2)
                    {
                        outputString += '6';
                        counter += 1;
                    }
                    else if (letter == 'i' && counter < 2 || letter == 'I' && counter < 2)
                    {
                        outputString += '1';
                        counter += 1;
                    }
                    else if (letter == 'o' && counter < 2 || letter == 'O' && counter < 2)
                    {
                        outputString += '0';
                        counter += 1;
                    }
                    else if (letter == 's' && counter < 2 || letter == 'S' && counter < 2)
                    {
                        outputString += '5';
                        counter += 1;
                    }
                    else if (letter == 't' && counter < 2 || letter == 'T' && counter < 2)
                    {
                        outputString += '7';
                        counter += 1;
                    }
                    else if (letter == 'z' && counter < 2 || letter == 'Z' && counter < 2)
                    {
                        outputString += '2';
                        counter += 1;
                    }
                    else
                    {
                        outputString += letter;
                    }
                }
                counter += 1;
            }
            return outputString;
        }

        /// <summary>
        /// Replaces certain characters with symbols. CNan be limited to a maximum number of replacements per word
        /// </summary>
        /// <param name="inputWord">the generated password</param>
        /// <returns></returns>
        private string replaceWithSymbol(string inputWord)
        {
            string outputString = "";
            int counter = 0;
            while (counter < 3)
            {
                foreach (char letter in inputWord)
                {

                    if (letter == 'c' && counter < 2 || letter == 'C' && counter < 2)
                    {
                        outputString += '(';
                        counter += 1;
                    }
                    else if (letter == 'a' && counter < 2 || letter == 'A' && counter < 2)
                    {
                        counter += 1;
                        outputString += '@';
                    }
                    else if (letter == 'e' && counter < 2 || letter == 'E' && counter < 2)
                    {
                        counter += 1;
                        outputString += '£';
                    }
                    else if (letter == 's' && counter < 2 || letter == 'S' && counter < 2)
                    {
                        outputString += '$';
                        counter += 1;
                    }
                    else if (letter == 'i' && counter < 2 || letter == 'I' && counter < 2)
                    {
                        outputString += '!';
                        counter += 1;
                    }
                    else
                    {
                        outputString += letter;
                    }
                }
                counter += 1;
            }
            return outputString;
        }

        /// <summary>
        /// Loads all the int/string values from a text file into a dictionary
        /// </summary>
        public void LoadDictionary()
        {
            string text = string.Empty;
            string filename = "wordList.txt";

            AssetManager assets = Android.App.Application.Context.Assets;
            using (StreamReader reader = new StreamReader(assets.Open(filename)))
            {
                string line = null;
                int number = 0;
                string listWord = "";

                while ((line = reader.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split('\t');
                    number = Int32.Parse(lineSplit[0]);
                    listWord = lineSplit[1];
                    words.Add(number, listWord);
                }
            }
        }

        /// <summary>
        /// Generates a random integer in the range 11111-66666
        /// </summary>
        /// <returns></returns>
        static int RandomIntGenerator()
        {
            int lowerBound = 11111;
            int upperBound = 66666;
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[4];
            provider.GetBytes(byteArray);
            var scaler = BitConverter.ToUInt32(byteArray, 0);
            var result = (int)(lowerBound + (upperBound - lowerBound) * (scaler / (double)uint.MaxValue));
            return result;
        }

        /// <summary>
        /// Changes the resulting text on the app
        /// </summary>
        private void presentResult()
        {
            //password = wordNum.ToString();
            outputResult.Text = password;
        }
    }
}