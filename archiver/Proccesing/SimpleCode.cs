using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace archiver
{
    public static  class SimpleCode
    {
        public static List<string> BuildCode(List<string> dictionary)
        {
            List<string> encodedDictionary = new List<string>();
            int codeLength = (int) Math.Ceiling(Math.Log(dictionary.Count, 2));
            for (int j = 0; j < dictionary.Count; j++)
            {
                var code = new BitArray(BitConverter.GetBytes(j));
                var stringCode = StringManipulator.BitToString(code, codeLength);
                
                if (!encodedDictionary.Contains(stringCode))
                {
                    encodedDictionary.Add(stringCode);
                }
            }
            MessageBox.Show("Средняя длина кодового слова: " + codeLength.ToString());
            return encodedDictionary;
        }
    }
}