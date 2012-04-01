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
    class Serializer
    {
        public Serializer()
        {
        }

        static public void SerializeToXML(Trie obj,string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Trie));
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, obj);
            textWriter.Close();
        }

        static public bool DeserializeFromXML(out Trie obj,string path)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Trie>));
            TextReader textReader = new StreamReader(path);
            obj = (Trie)deserializer.Deserialize(textReader);
            textReader.Close();
            return true;
        }


        static public void SerializeObject(string filename, Trie objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        static public Trie DeSerializeObject(string filename)
        {
            Trie objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (Trie)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
