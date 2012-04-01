using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace CreateBiGramTrie
{
    class TrieNode : IEquatable<TrieNode>
    {
        public char word;
        public float prob;
        public Trie nextNode;

        public TrieNode(char c, float prob)
        {
            this.word = c;
            this.prob = prob;
            this.nextNode = null;
        }

        public bool isTerminate()
        {
            return nextNode==null;
        }

        public bool Equals(TrieNode node)
        {
            if (this.word == node.word)
            {
                return true;
            }
            else
                return false;
        }
    }
    
    class Trie_mod
    {
        public Dictionary<char, TrieNode> root = new Dictionary<char, TrieNode>();
        //List<TrieNode> root = new List<TrieNode>();
        char EndOfString = '\0';
        
        public Trie_mod()
        {
        }

        /*public Trie(SerializationInfo info, StreamingContext ctxt)
        {
            this.root = (Dictionary<char, Trie>)info.GetValue("root", typeof(Dictionary<char, Trie>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("root", this.root);
        }*/



        public bool isRoot(char c)
        {
            //TrieNode dummyNode = new TrieNode(c, 0);
            return root.ContainsKey(c);
        }
        public Trie GetAllChild(char c)
        {
            if (!isRoot(c))
                return null;
            else
                //return null;
                return root[c].nextNode;
        }
        public void InsertString(string s,float prob)
        {
            if (s.Length == 0)
            {
                if (!isRoot('\0'))
                    root['\0'] = null;
            }
            else
            {
                if(isRoot(s[0])) //if first character already in Trie level
                {
                    //root[s[0]].InsertString(s.Substring(1),prob);
                }
                else
                {
                    root[s[0]] = new TrieNode(s[0],0);
                    //root[s[0]].InsertString(s.Substring(1),prob);
                }
            }
        }
        public bool ContainStringInTrie(string s)
        {
            if (s.Length == 0)
            {
                return isRoot('\0');
            }
            else
            {
                if (!isRoot(s[0]))
                    return false;
                else
                    return true;// root[s[0]].ContainStringInTrie(s.Substring(1));
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
            if(!isRoot(s[0]))
                return null;
            else
            {
                if(s.Length==1)
                    return null;//root[s[0]];
                else
                    return null;// root[s[0]].GetSubT(s.Substring(1));
            }
        }
        public Dictionary<char,Trie>.KeyCollection GetAllRootSymbol()
        {
            return null;// root.Keys;
        }
        private List<string> DFS(Trie t)
        {
            List<string> ans = new List<string>();
            /*foreach (char w in t.root.Keys)
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
            }*/
            return ans;
        }
    }
}
