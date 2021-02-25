#region main bulk of app

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PassGenerator
{

    public partial class MainPage : ContentPage
    {
        Dictionary<int, string> words = new Dictionary<int, string>();
        int wordNum = 4;
        string password = "";
        public static double final = 0;
        public MainPage()
        {
            InitializeComponent();
            Copy.IsEnabled = false;
            strength.IsEnabled = false;
            LoadDictionary();
        }

        public void ButtonClick(object sender, EventArgs args)
        {
            password = "";
            wordCount();
            createWord();
            presentResult();
            Copy.IsEnabled = true;
            strength.IsEnabled = true;
        }

        /// <summary>
        /// Adds the password to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void copyPassword(object sender, EventArgs args)
        {
            Clipboard.SetTextAsync(password);
            if (Clipboard.HasText)
            {
                var text = Clipboard.GetTextAsync();
                DisplayAlert("Success", string.Format("Your generated passphrase has been copied to the clipboard", text), "OK");
            }
        }

        public async void testStrength(object sender, EventArgs e)
        {
            final = getStrengthValue();
            await Navigation.PushAsync(new Strength());
        }

        public double getStrengthValue()
        {
            int total = 0;
            int total2 = 0;

            total += NumberOfCharacters(password);

            total += CountUppercaseLetters(password);

            total += CountLowercaseLetters(password);

            total += CountNumbers(password);

            total += CountSymbols(password);

            total += CountMiddleSymbols(password);

            total += MeetsAllRequirements(password);

            total2 += onlyLetters(password);

            total2 += onlyNumbers(password);

            total2 += checkRepeatCharacters(password);

            total2 += ConsecutiveUpperLetters(password);

            total2 += ConsecutiveLowerLetters(password);

            total2 += ConsecutiveNumbers(password);

            total2 += SequentialLetters(password);

            var finalTotal = total - total2;

            var final = scaledScore(finalTotal);

            return final;
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
            int counter = 1;
            while (outputString.Length != inputWord.Length)
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

            while (outputString.Length != inputWord.Length)
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

            var stream = Android.App.Application.Context.Assets.Open("wordList.txt");

            using (var readtextfile = new StreamReader(stream))
            {
                string line = null;
                int number = 0;
                string listWord = "";

                while ((line = readtextfile.ReadLine()) != null)
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
            outputResult.Text = password;
        }

#endregion

        #region Strength Check

        /*
         * 
         * 
         * Password strength checking goes below here
         * 
         * 
         */


        public double scaledScore(int total)
        {
            var Value = total / 2.5;
            if (Value > 100)
            {
                return 100;
            }
            else
            {
                return Value;
            }
        }

        public int NumberOfCharacters(string password)
        {
            if (password.Length * 4 > 200)
            {
                return 100;
            }
            else
            {
                return (password.Length * 4);
            }
        }

        public int CountUppercaseLetters(string password)
        {
            int count = 0;
            foreach (char i in password)
            {
                if (char.IsUpper(i)) count += 1;
            }

            if (count == 0)
            {
                return 0;
            }
            else
            {
                if (((password.Length - count) * 2) > 400)
                {
                    return 50;
                }
                else
                {
                    return (password.Length - count) * 2;
                }
            }
        }

        public int CountLowercaseLetters(string password)
        {
            int count = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsLower(password[i])) count++;
            }

            if (count == 0)
            {
                return 0;
            }
            else
            {
                if (((password.Length - count) * 2) > 100)
                {
                    return 100;
                }
                else
                {
                    return (password.Length - count) * 2;
                }
            }
        }

        public int CountNumbers(string password)
        {
            int count = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsDigit(password[i])) count++;
            }

            if (count * 4 > 50)
            {
                return 50;
            }
            else
            {
                return count * 4;
            }
        }

        public int CountSymbols(string password)
        {
            int count = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsLetterOrDigit(password[i])) count++;
            }

            if (((password.Length - count) * 6) > 50)
            {
                return 50;
            }
            else
            {
                return (password.Length - count) * 6;
            }
        }

        public int CountMiddleSymbols(string password)
        {
            var symbols = 0;
            int count = 0;
            for (int i = 1; i < password.Length - 1; i++)
            {
                if (char.IsLetterOrDigit(password[i])) symbols++;
            }

            for (int i = 1; i < password.Length - 1; i++)
            {
                if (char.IsDigit(password[i])) count++;
            }
            var total = (password.Length - 2) - symbols;
            total += count;

            if (total * 2 > 50)
            {
                return 0;
            }
            else
            {
                return total * 2;
            }
        }

        public int MeetsAllRequirements(string password)
        {
            int total = 0;
            if (CountUppercaseLetters(password) > 0)
            {
                total += 1;
            }
            if (CountLowercaseLetters(password) > 0)
            {
                total += 1;
            }
            if (CountNumbers(password) > 0)
            {
                total += 1;
            }
            if (CountSymbols(password) > 0)
            {
                total += 1;
            }
            if (password.Length > 7)
            {
                total += 1;
            }

            if (total <= 3)
            {
                return 0;
            }
            else
            {
                return total * 2;
            }
        }

        public int onlyLetters(string password)
        {
            if (((CountUppercaseLetters(password) / 2) + (CountLowercaseLetters(password) / 2)) == password.Length)
            {
                if (password.Length > 200)
                {
                    return 200;
                }
                else
                {
                    return password.Length;
                }
            }
            else
            {
                return 0;
            }
        }

        public int onlyNumbers(string password)
        {
            if ((CountNumbers(password) / 4) == password.Length)
            {
                if (password.Length > 200)
                {
                    return 200;
                }
                else
                {
                    return password.Length;
                }
            }
            else
            {
                return 0;
            }
        }

        public int checkRepeatCharacters(string password)
        {
            HashSet<char> characters = new HashSet<char>();
            foreach (char i in password)
            {
                characters.Add(i);
            }

            int originalTotal = password.Length;
            int newTotal = characters.Count;

            int Result = originalTotal - newTotal;

            return Result > 50 ? 50 : Result;
        }

        public int ConsecutiveUpperLetters(string password)
        {
            int dupes = 0;
            for (int i = 0; i < password.Length - 1; i++)
            {
                if ((Char.IsUpper(password[i + 1])) && (Char.IsUpper(password[i]) == true))
                {
                    dupes += 1;
                }
            }

            if (dupes * 2 > 50)
            {
                return 50;
            }
            else
            {
                return dupes * 2;
            }
        }

        public int ConsecutiveLowerLetters(string password)
        {
            int dupes = 0;
            for (int i = 0; i < password.Length - 1; i++)
            {
                if ((Char.IsLower(password[i + 1])) && (Char.IsLower(password[i]) == true))
                {
                    dupes += 1;
                }
            }

            if (dupes * 2 > 50)
            {
                return 50;
            }
            else
            {
                return dupes * 2;
            }
        }

        public int ConsecutiveNumbers(string password)
        {
            int dupes = 0;
            for (int i = 0; i < password.Length - 1; i++)
            {
                if ((Char.IsDigit(password[i + 1])) && (Char.IsDigit(password[i]) == true))
                {
                    dupes += 1;
                }
            }

            if (dupes * 2 > 50)
            {
                return 50;
            }
            else
            {
                return dupes * 2;
            }
        }

        public int SequentialLetters(string password)
        {
            int dupes = 0;
            if (password.Length < 2)
            {
                return 0;
            }
            else
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(password);

                for (int i = 0; i < password.Length - 1; i++)
                {
                    var tmp1 = asciiBytes[i];
                    var tmp2 = asciiBytes[i + 1];

                    if (tmp1 + 1 == (tmp2))
                    {
                        dupes += 1;
                    }
                }

                if (dupes * 3 > 50)
                {
                    return 50;
                }
                else
                {
                    return dupes * 3;
                }
            }

        }
    }
}

#endregion