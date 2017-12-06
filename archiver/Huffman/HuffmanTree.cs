using System.Collections;
using System.Collections.Generic;
using System.Linq;
using archiver.MultiArchiving;
using archiver.TextProccesing;

namespace archiver.Huffman
{
    class HuffmanTree
    {
        public List<HuffmanNode> nodes = new List<HuffmanNode>();
        public HuffmanNode Root { get; set; }

        public Dictionary<string, int> Frequencies = new Dictionary<string, int>();

        public Dictionary<string, double> d = new Dictionary<string, double>();

        public void Build(List<string> source)
        {
            foreach (string s in source)
            {
                if (!Frequencies.ContainsKey(s))
                {
                    Frequencies.Add(s, 0);
                }

                Frequencies[s]++;
            }

            foreach (string s in source)
            {
                double a = Frequencies[s] / (double)(source.Count);
                if (!d.ContainsKey(s))
                {
                    d.Add(s, a);
                }
            }
            
            foreach (KeyValuePair<string, int> value in Frequencies)
            {
                nodes.Add(new HuffmanNode() { Value = value.Key, Frequency = value.Value });
            }

            while (nodes.Count > 1)
            {
                List<HuffmanNode> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<HuffmanNode>();
                
                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<HuffmanNode> taken = orderedNodes.Take(2).ToList<HuffmanNode>();

                    // Create a parent HuffmanNode by combining the frequencies
                    HuffmanNode parent = new HuffmanNode()
                    {
                        Value = "*",
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                Root = nodes.FirstOrDefault();
                
            }

        }

        
        public List<string> Encode(List<string> dictionary, Session session)
        {
            double b = 0;
           // double c = 0;
            var result = new List<string>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                List<bool> encodedSymbol = Root.Traverse(dictionary[i], new List<bool>());
                result.Add(StringManipulator.BitToString(new BitArray(encodedSymbol.ToArray())));

                b += result[i].Length * d[dictionary[i]];
                //c += d[dictionary[i]];
            }
            session.AverageElementLength = b;
            return result;
        }

        public string Decode(BitArray bits)
        {
            HuffmanNode current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Value;
                    current = this.Root;
                }
            }

            return decoded;
        }

        public bool IsLeaf(HuffmanNode huffmanNode)
        {
            return (huffmanNode.Left == null && huffmanNode.Right == null);
        }
    }
}
