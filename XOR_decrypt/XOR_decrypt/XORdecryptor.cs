using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XOR_decrypt
{
    class XORdecryptor
    {
        private List<byte> cipherBytes = new List<byte>();
        private string[] cipherArray;
        private List<char> PasswordChars = new List<char>();
        private List<string> Dictionary = new List<string>();

        private Stopwatch stopwatch = new Stopwatch();

        public int bestScore = -2147483648;
        public string bestAnswer = "";
        public string bestPassword = "";
        public TimeSpan bestTime;

        private string debugOut = "";

        public XORdecryptor()
        { }

        public void DecryptFile(string file)
        {
            //Valid characters that can be in the password
            PasswordChars.Add('a');
            PasswordChars.Add('b');
            PasswordChars.Add('c');
            PasswordChars.Add('d');
            PasswordChars.Add('e');
            PasswordChars.Add('f');
            PasswordChars.Add('g');
            PasswordChars.Add('h');
            PasswordChars.Add('i');
            PasswordChars.Add('j');
            PasswordChars.Add('k');
            PasswordChars.Add('l');
            PasswordChars.Add('m');
            PasswordChars.Add('n');
            PasswordChars.Add('o');
            PasswordChars.Add('p');
            PasswordChars.Add('q');
            PasswordChars.Add('r');
            PasswordChars.Add('s');
            PasswordChars.Add('t');
            PasswordChars.Add('u');
            PasswordChars.Add('v');
            PasswordChars.Add('w');
            PasswordChars.Add('x');
            PasswordChars.Add('y');
            PasswordChars.Add('z');

            //Populate the dictionary
            //The most common letters and 2-3 letter combinations
            Dictionary.Add("e");
            Dictionary.Add("a");
            Dictionary.Add("t");
            Dictionary.Add("th");
            Dictionary.Add("ee");
            Dictionary.Add("oo");
            Dictionary.Add("qu");
            Dictionary.Add("ea");
            Dictionary.Add("ss");
            Dictionary.Add("the");
            Dictionary.Add("and");
            Dictionary.Add("est");
            Dictionary.Add("ent");
            Dictionary.Add("ent");
            Dictionary.Add("ing");

            //Put the contents of the file into a an array of bytes.
            cipherArray = new StreamReader(file).ReadToEnd().Split(',');

            foreach (string number in cipherArray)
            {
                cipherBytes.Add(byte.Parse(number));
            }

            decrypt();
        }

        private void decrypt()
        {
            char[] password = new char[3];
            byte[] decryptAttempt = new byte[cipherBytes.Count];

            stopwatch.Start();
            //Brute-Force
            foreach (char char1 in PasswordChars)
            {
                foreach (char char2 in PasswordChars)
                {
                    foreach (char char3 in PasswordChars)
                    {
                        password[0] = char1;
                        password[1] = char2;
                        password[2] = char3;

                        XOR(cipherBytes, password, ref decryptAttempt);
                        int newScore = heuristic(decryptAttempt);

                        if (newScore > bestScore)
                        {
                            bestScore = newScore;
                            bestAnswer = Encoding.ASCII.GetString(decryptAttempt, 0, decryptAttempt.Length);
                            bestPassword = new string(password);
                        }
                    }
                }
            }

            stopwatch.Stop();
            bestTime = stopwatch.Elapsed;
        }

        private void XOR(List<byte> input, char[] password, ref byte[] result)
        {
            byte[] payload = input.ToArray();
            int n = 0;
            for (int i = 0; i < payload.Length; i++)
            {
                result[i] = Convert.ToByte(payload[i] ^ password[n]);
                n++;
                if (n == 3) { n = 0; }
            }
        }


        private int heuristic(byte[] input)
        {
            string inputString = System.Text.Encoding.ASCII.GetString(input);
            int score = 0;
            string[] words = inputString.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                foreach (string sequence in Dictionary)
                {
                    if (words[i].IndexOf(sequence) != -1)
                    {
                        score = score + (int)Math.Pow(sequence.Length, 2);
                    }
                }
            }
            return score;
        }

    }
}
