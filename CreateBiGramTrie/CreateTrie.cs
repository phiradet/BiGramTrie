using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CreateBiGramTrie
{
    static class CreateTrie
    {
        struct bigramData
        {
            public Dictionary<string, double> biProb;
        }
        private static Dictionary<string, double> allWord_EN = new Dictionary<string, double>();
        private static Dictionary<string, bigramData> allWord_EN_Bigram = new Dictionary<string, bigramData>();
        private static Dictionary<string, double> allWord_TH = new Dictionary<string, double>();

        public static void PrepareTrie(string EnIn,string EnOut)
        {
            //LoadAllWord_TH(ThIn);
            LoadAllWord_EN(EnIn);
            LoadAllWord_EN_Bigram("bigramProb.dat");

            Trie en = new Trie();
            Trie th = new Trie();

            Console.WriteLine("start trie EN");
            foreach (KeyValuePair<string, double> entry in allWord_EN)
            {
                string word = entry.Key;
                double prob = entry.Value;
                Dictionary<string, double> biProb;
                if (allWord_EN_Bigram.ContainsKey(word))
                {
                    biProb = allWord_EN_Bigram[word].biProb;
                    Trie biGramTri = new Trie();
                    foreach (KeyValuePair<string, double> entryBigram in biProb)
                    {
                        string wordBi = entryBigram.Key;
                        double probBi = entryBigram.Value;
                        biGramTri.InsertString(wordBi, probBi,null);
                        //if (word == "hello")
                        //    Console.Read();
                    }
                    en.InsertString(word, prob, biGramTri);
                   
                   
                }
                else
                {
                    en.InsertString(word, prob, null);
                }
                
                
            }
            double tmpProb,tmpBiProb;
            Trie biGramTrie,h;
            en.ContainStringInTrie("hello", out tmpProb, out biGramTrie);
            biGramTrie.ContainStringInTrie("know", out tmpBiProb, out h);

            Debug.WriteLine(string.Format("prob {0}:{1}:{2}", "hello", tmpProb, tmpBiProb));
            /*Debug.WriteLine("start trie TH");
            for (int i = 0; i < allWord_TH.Count; i++)
            {
                th.InsertString(allWord_TH[i]);
            }*/

            //Debug.WriteLine(String.Format("en={0} th={1}",allWord_EN.Count,allWord_TH.Count));
            Debug.WriteLine(String.Format("en={0}", allWord_EN.Count));

            //Serializer serializer = new Serializer();
            Debug.WriteLine("start serial EN");
            Serializer.SerializeObject(EnOut, en);
            //Debug.WriteLine("start serial TH");
            //serializer.SerializeObject(ThOut, th);
            //Debug.WriteLine("finish serial TH");
            Console.Read();
        }

        static void LoadAllWord_EN_Bigram(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            string currWord = "";
            string currFront = "";
            double currProb = 0.0f;
            for (int i = 0; i < lines.Length; i++)
            {
                currWord = lines[i].Trim().Split('\t')[0];
                currFront = lines[i].Trim().Split('\t')[1];
                currProb = double.Parse(lines[i].Trim().Split('\t')[2]);
                if (!allWord_EN_Bigram.ContainsKey(currWord))
                {
                    bigramData tmp = new bigramData();
                    tmp.biProb = new Dictionary<string,double>();
                    allWord_EN_Bigram.Add(currWord,tmp);
                }
                allWord_EN_Bigram[currWord].biProb.Add(currFront,currProb);
            }
        }
        static void LoadAllWord_EN(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            string currWord = "";
            double currProb = 0.0f;
            for (int i = 0; i < lines.Length; i++)
            {
                currWord = lines[i].Trim().Split('\t')[0];
                currProb = double.Parse(lines[i].Trim().Split('\t')[1]);
                allWord_EN.Add(currWord,currProb);
            }
            //Console.WriteLine("finished merger all:{0}", allWord.Capacity);
        }



        /*static void LoadAllWord_TH(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path, Encoding.UTF8);
            string currWord = "";
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().Length > 0)
                {
                    currWord = lines[i].Trim();
                    allWord_TH.Add(currWord);
                }
            }
        }*/
    }
}
