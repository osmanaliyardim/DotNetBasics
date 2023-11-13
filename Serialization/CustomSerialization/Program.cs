using System.Runtime.Serialization.Formatters.Binary;
using CustomSerialization;

CustomBinarySerialization customBinarySerialization = new CustomBinarySerialization("test1", 10);

using (FileStream fs = new FileStream("CustomSerialization.bin", FileMode.Create))
{
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(fs, customBinarySerialization);
    Console.WriteLine($"Serialized: {customBinarySerialization}");
}

using (FileStream fs = new FileStream("CustomSerialization.bin", FileMode.Open))
{
    BinaryFormatter formatter = new BinaryFormatter();
    CustomBinarySerialization deserializedInstance = (CustomBinarySerialization)formatter.Deserialize(fs);
    Console.WriteLine($"Deserialized: {deserializedInstance}");
}

Console.ReadLine();