using System;
using System.IO ;
using System.Xml ;
using System.Xml.Serialization;
using System.Web ;

namespace WComm
{
	
	public class XmlSerializer
	{
		public XmlSerializer()
		{
			
		}
        public static string Serialize(object _object)
		{
			System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(_object.GetType ());
			StringWriter writer= new StringWriter ();

			serializer.Serialize(writer, _object);

			return writer.ToString ();

		}

        public static Object Deserialize(Type type,string s)
        {
            //return null;
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
            StringReader reader = new StringReader(s);

            return serializer.Deserialize(reader);
        }
	
	}
}
