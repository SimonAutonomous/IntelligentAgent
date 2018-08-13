using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using System.Reflection;


namespace ObjectSerializerLibrary
{
    public static class ObjectXmlSerializer
    {
        private static List<Type> ObtainSerializableTypes(Assembly assembly)
        {
            var types = from t in assembly.GetTypes()
                        where (t.GetCustomAttributes(typeof(DataContractAttribute), false)).Length > 0
                        select t;

            return types.ToList<Type>();
        }

        public static Object ObtainSerializedObject(string fileName, Type type)
        {
            List<Type> knownTypes = ObtainSerializableTypes(type.Assembly);
            DataContractSerializer dcs = new DataContractSerializer(type, knownTypes);
            Stream stream = File.Open(fileName, FileMode.Open);
            Object obj = dcs.ReadObject(stream);
            stream.Close();
            return obj;
        }

        public static void SerializeObject(string fileName, Object obj)
        {
            Assembly assembly = obj.GetType().Assembly;
            Type type = obj.GetType();
            List<Type> knownTypes = ObtainSerializableTypes(assembly);
            DataContractSerializer dcs = new DataContractSerializer(type, knownTypes);
            Stream stream = File.Open(fileName, FileMode.Create);
            dcs.WriteObject(stream, obj);
            stream.Close();
        }

        // 20170105: This method should be used when any of the serialized types are NOT
        //           included in the various IPA libraries.
        //           This happens, for example, if one writes a new type (placed under
        //           the application in question), derived from one of the types in the IPA.
        public static object ObtainSerializedObject(string fileName, Type type, List<Type> additionalTypeList)
        {
            List<Type> knownTypes = ObtainSerializableTypes(type.Assembly);
            knownTypes.AddRange(additionalTypeList);
            DataContractSerializer dcs = new DataContractSerializer(type, knownTypes);
            Stream stream = File.Open(fileName, FileMode.Open);
            Object obj = dcs.ReadObject(stream);
            stream.Close();
            return obj;
        }

        // 20170105: This method should be used when any of the serialized types are NOT
        //           included in the various IPA libraries.
        //           This happens, for example, if one writes a new type (placed under
        //           the application in question), derived from one of the types in the IPA.
        public static void SerializeObject(string fileName, Object obj, List<Type> additionalTypeList)
        {
            Assembly assembly = obj.GetType().Assembly;
            Type type = obj.GetType();
            List<Type> knownTypes = ObtainSerializableTypes(assembly);
            knownTypes.AddRange(additionalTypeList);
            DataContractSerializer dcs = new DataContractSerializer(type, knownTypes);
            Stream stream = File.Open(fileName, FileMode.Create);
            dcs.WriteObject(stream, obj);
            stream.Close();
        }
    }
}
