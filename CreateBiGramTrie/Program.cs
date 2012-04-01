using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateBiGramTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            /*TrieNode t = new TrieNode('x', 555);
            List<TrieNode> c = new List<TrieNode> { t };
            TrieNode tt = new TrieNode('x', 5553);
            if (c.Contains(tt))
                return;*/
            CreateTrie.PrepareTrie("unigramProb.dat","unigramProb.xml");
        }
    }
}
