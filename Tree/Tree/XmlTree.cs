using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Tree {
	public class XmlTree {

		private static Node<Record> LoadElement(XElement elem) {
            Record data = new Record();
            IEnumerable<XAttribute> attList = from at in elem.Attributes() select at;
            foreach (XAttribute att in attList)
                data[att.Name.ToString()] = att.Value;
            return new Node<Record>(data, elem.Elements("node").Select(e => LoadElement(e)));
		}
		public static Node<Record> Load(string file) {
			XDocument xml = XDocument.Load(file);
			if (xml.Root.Name != "node") return null;
			return LoadElement(xml.Root);
		}

	}
    public class Record {
        private Dictionary<string, string> dict;
        private string stringToReturn;

        public Record()
        {
            dict = new Dictionary<string,string>();
            stringToReturn = "";
        }
      

        public string this[string index]    {
            get
            {
                return dict[index];
            }

            set
            {
                dict[index] = value;
            }
        }
        
        public override string ToString() {            
            foreach (KeyValuePair<string, string> kv in dict){
                stringToReturn += kv.Key + " = " + kv.Value + "                  ";
            }

            return stringToReturn;
        }

        public IEnumerable<XAttribute> Keys
        {
            get
            {
                List<XAttribute> list = new List<XAttribute>();
                foreach (KeyValuePair<string, string> kv in dict)
                    list.Add(new XAttribute(kv.Key, kv.Value));
                return list;
            }
        }
    }


}

