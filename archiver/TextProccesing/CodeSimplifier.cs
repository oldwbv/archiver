using System;
using System.Collections;
using System.Collections.Generic;
using archiver.MultiArchiving;

namespace archiver.TextProccesing
{
    public static class CodeSimplifier
    {
        public static List<string> BuildCode(List<string> dictionary, Session session)
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
            session.AverageElementLength = codeLength;
            return encodedDictionary;
        }
    }
}