using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using archiver.Proccesing;

namespace archiver
{
    public static class HaffmanCode
    {

        public static List<string> BuildCode(List<string> splittedText, List<string> dictionary, Session session)
        {
            HuffmanTree tree = new HuffmanTree();
            tree.Build(splittedText);
            return tree.Encode(dictionary, session);
        } 
    }
}
