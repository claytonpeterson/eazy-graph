using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("Data")]
public class TestingOutData
{
    [XmlElement("name")]
    public string name;

    [XmlElement("age")]
    public int age;

    [XmlElement("location")]
    public string location;

    public Dictionary<string, string> lookup = new Dictionary<string, string>();
}
