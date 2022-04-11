﻿using System;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("Data")]
public class Data
{
    [XmlElement("name")]
    public string name;

    [XmlElement("age")]
    public int age;

    [XmlElement("location")]
    public string location;
}
