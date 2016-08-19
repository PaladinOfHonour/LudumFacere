using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour 
{
    // singleton pattern
    public static XMLManager instance;

    void Awake()
    {
        instance = this;
    }

    // Save a list of items in an XML file
    public void Save(ItemDatabase itemDB, string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets" + path, FileMode.Create);
        serializer.Serialize(stream, itemDB);
        stream.Close();
    }

    // Load a list of items from an XML file
    public ItemDatabase Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/StreamingAssets" + path, FileMode.Open);
        ItemDatabase itemDB = serializer.Deserialize(stream) as ItemDatabase;
        stream.Close();
        return itemDB;
    }
}

// ItemEntry class
[System.Serializable]
public class ItemEntry
{
    public string name;
    public string description;
    public TechType type;
    public bool hasIntegratedGraphics;
    public string bitCapacity;
    public List<string> parents;
}

// ItemDatabase class
[System.Serializable]
public class ItemDatabase 
{
    public List<ItemEntry> list = new List<ItemEntry>();
}

// Technology Type Enumerator
public enum TechType
{
    Processor,
    GraphicsProcessor,
    Memory,
    HardDrive,
    SSD,
    Input,
    Monitor,
    Technology,
    Software
}
