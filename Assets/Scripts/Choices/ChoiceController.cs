using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    [SerializeField] TMP_Text description, north, west, east, south, nodeName;

    string currentPath;
    NodesArray nodes;
    Inventory inv;
    List<int> itemIndexQueue;

    void Start()
    {
        nodes = GetComponent<JSONToNodesArray>().Generate();
        inv = FindObjectOfType<Inventory>();
        itemIndexQueue = new List<int>();
        
        SetUI("0");
    }

    public void MakeChoice(string direction)
    {
        if (currentPath.Equals("0"))
        {
            SetUI(direction);
        }
        else
        {
            SetUI(currentPath + "_" + direction);
        }
    }

    void SetUI(string target)
    {
        print(target);
        
        if (inv.items == null)
        {
            inv.SetItemsArray();
        }

        Node temp = nodes.GetNode(target);

        if (temp != null)
        {
            for (int i = 0; i < inv.items.Length; i++)
            {
                if (temp.NodeNum.Equals(inv.items[i].Origin))
                {
                    itemIndexQueue.Add(i);
                    Invoke(nameof(AddToInventory), 16);
                }
                else if (temp.NodeNum.Equals(inv.items[i].Target))
                {
                    // TODO
                }
            }
            
            description.text = temp.Description;
            north.text = temp.North;
            south.text = "<wiggle a=0.08>" + temp.South + "</wiggle>";
            west.text = temp.West;
            east.text = temp.East;
            nodeName.text = temp.NodeName.ToUpper();
            
            currentPath = target;
        }

        inv.UpdateInventory(currentPath);
        
        FindObjectOfType<TextTimeline>().Reset();
    }

    void AddToInventory()
    {
        inv.AddToInventory(itemIndexQueue[0]);
        itemIndexQueue.RemoveAt(0);
    }
}