using System.Collections.Generic;
using archiver.MultiArchiving;

namespace archiver.Huffman
{
    public static class HaffmanCode
    {
        public static List<string> BuildCode(List<string> splittedText, List<string> dictionary, Session session)
        {
            HuffmanTree hTree = new HuffmanTree();
            hTree.Build(splittedText);
            return hTree.Encode(dictionary, session);
        } 
    }
}
