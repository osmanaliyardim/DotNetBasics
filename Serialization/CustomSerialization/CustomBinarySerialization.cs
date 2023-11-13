using System.Runtime.Serialization;

namespace CustomSerialization
{
    [Serializable]
    public class CustomBinarySerialization : ISerializable
    {
        public string Property1 { get { return _Property1; } }
        private readonly string _Property1;
        public int Property2 { get { return _Property2; } }
        private readonly int _Property2;

        public CustomBinarySerialization(string property1, int property2)
        {
            if (property1 == null)
            {
                throw new ArgumentNullException("Properties are null");
            }

            _Property1 = property1;
            _Property2 = property2;
        }

        protected CustomBinarySerialization(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            _Property1 = info.GetString("Property1");
            _Property2 = info.GetInt32("Property2");
        }

        protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Property1", Property1);
            info.AddValue("Property2", Property2);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            GetObjectData(info, context);
        }

        public override string ToString()
        {
            return $"Property1: {_Property1} Property2: {_Property2}";
        }
    }
}