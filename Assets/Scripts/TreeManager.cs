using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour {

    [UnityEngine.SerializeField]
    private ItemDatabase itemDB;

    [UnityEngine.SerializeField]
    private string path = "/Technology/Software Tech Tree.xml";

    private GameObject[] treeItems;
    private GameObject[] treeColumns;

    public GameObject treeItemPrefab;

    void Start()
    {
        treeColumns = GameObject.FindGameObjectsWithTag("Tree Column");
        Array.Reverse(treeColumns);
    }

    public void Load()
    {
        // Load the XML file and save it in the itemDB
        itemDB = XMLManager.instance.Load(path);

        treeItems = new GameObject[itemDB.list.Count];

        // For each ItemEntry
        foreach (ItemEntry item in itemDB.list)
        {
            // Load the icon for the tree item // TODO: make this work!
            Sprite icon = Resources.Load<Sprite>(item.iconPath);
            // TODO: add placeholder icon

            // Instantiate a tree item object
            GameObject itemObject = GameObject.Instantiate(treeItemPrefab) as GameObject;
            itemObject.name = item.name;
            itemObject.GetComponentInChildren<Text>().text = item.name;
            itemObject.GetComponentsInChildren<Image>()[1].sprite = icon;
            treeItems[itemDB.list.IndexOf(item)] = itemObject;

            int index = 0;

            // Get the 'highest' parent's index
            foreach (string parent in item.parents)
            {
                // If the itemEntry has "Start" as a parent then the tree index will be 0
                if (parent == "Start")
                {
                    Debug.Log("We made it!");
                    index = 0;
                    break;
                }

                // Get the tree column that the parent is in
                GameObject treeColumn = findItemObject(parent).GetComponent<RectTransform>().parent.gameObject;

                // Remove "Tree Column " from the name to be left with the index of the Tree Column
                int i = int.Parse(treeColumn.name.Replace("Tree Column ", ""));
                if (i >= index) index = i + 1;
            }

            // Set the itemObject's parent in the hierarchy
            itemObject.transform.parent = treeColumns[index].transform;

            //TODO: Generate connections
        }
    }

    public void Save()
    {
        // Save the itemDB to the XML file at path
        XMLManager.instance.Save(itemDB, path);
    }

    private GameObject findItemObject(string itemName)
    {
        foreach (GameObject itemObject in treeItems)
        {
            if (itemObject.name == itemName) return itemObject;
        }

        return null;
    }
}
