using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CreateBiGramTrie
{
    [Serializable()]
    public class Trie
    {
        const double dummyProb = 8888;
        double prob = dummyProb;
        Trie nextBigram = null;
        bool isTerminate = false;

        public Dictionary<char, Trie> root = new Dictionary<char, Trie>();

        public Trie()
        {
        }

        public Trie(SerializationInfo info, StreamingContext ctxt)
        {
            this.root = (Dictionary<char, Trie>)info.GetValue("root", typeof(Dictionary<char, Trie>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("root", this.root);
        }

        public Trie(double prob, Trie nextBigram, bool isTerminate)
        {
            this.prob = prob;
            this.nextBigram = nextBigram;
            this.isTerminate = isTerminate;
        }

        public bool isRoot(char c)
        {
            return root.ContainsKey(c);
        }
        public Trie GetAllChild(char c)
        {
            if (!isRoot(c))
                return null;
            else
                return root[c];
        }
        public void InsertString(string s, double prob, Trie nextBigram)
        {
            if (s.Length == 0)
            {
                if (!isRoot('\0'))
                    root['\0'] = new Trie(prob,nextBigram,true);
            }
            else
            {
                if (isRoot(s[0]))
                {
                    root[s[0]].InsertString(s.Substring(1),prob,nextBigram);
                }
                else
                {
                    root[s[0]] = new Trie();
                    root[s[0]].InsertString(s.Substring(1), prob, nextBigram);
                }
            }
        }
        public bool ContainStringInTrie(string s, out double prob,out Trie biGram)
        {
            if (s.Length == 0)
            {
                if (isRoot('\0'))
                {
                    prob = root['\0'].prob;
                    biGram = root['\0'].nextBigram;
                    return true;
                }
                else
                {
                    prob = dummyProb;
                    biGram = null;
                    return false;
                }
            }
            else
            {
                if (!isRoot(s[0]))
                {
                    prob = dummyProb;
                    biGram = null;
                    return false;
                }
                else
                    return root[s[0]].ContainStringInTrie(s.Substring(1),out prob,out biGram);
            }
        }
        public List<string> SimilarWord(string s)
        {
            Trie subT = GetSubT(s);
            if (subT == null)
                return new List<string>();
            List<string> w = DFS(subT);
            List<string> ans = new List<string>();
            foreach (string tmpW in w)
            {
                if (s + tmpW != s)          //If you want similar word please comment this contion
                    ans.Add(s + tmpW);
            }
            return ans;
        }
        private Trie GetSubT(string s)
        {
            if (!isRoot(s[0]))
                return null;
            else
            {
                if (s.Length == 1)
                    return root[s[0]];
                else
                    return root[s[0]].GetSubT(s.Substring(1));
            }
        }
        public Dictionary<char, Trie>.KeyCollection GetAllRootSymbol()
        {
            return root.Keys;
        }
        private List<string> DFS(Trie t)
        {
            List<string> ans = new List<string>();
            foreach (char w in t.root.Keys)
            {
                if (w == '\0')
                {
                    if (ans.Contains(""))
                        continue;
                    else
                        ans.Add("");
                }
                else
                {
                    List<string> tmpAns = DFS(t.GetAllChild(w));
                    foreach (string currSuffix in tmpAns)
                    {
                        ans.Add(w + currSuffix);
                    }
                }
            }
            return ans;
        }
    }
}
