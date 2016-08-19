using UnityEngine;
using System.Collections;

public class TreeManager : MonoBehaviour {

    [UnityEngine.SerializeField]
    private ItemDatabase itemDB;

    [UnityEngine.SerializeField]
    private string path = "/Technology/Software Tech Tree.xml";

    public void Load()
    {
        itemDB = XMLManager.instance.Load(path);
    }

    public void Save()
    {
        XMLManager.instance.Save(itemDB, path);
    }
}
