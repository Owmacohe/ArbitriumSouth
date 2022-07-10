using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChoiceController : MonoBehaviour
{
    /*
    
    ,
    {
        "NodeNum": "",
        "NodeName": "",
        "Description": "",
        "North": "",
        "West": "",
        "East": "",
        "South": ""
    }
    
    <style=Alt><wiggle a=0.08></wiggle></style>
    
    */
    
    [SerializeField] TextAsset file;
    [SerializeField] TMP_Text description, north, west, east, south, nodeName;

    string currentPath;
    NodesArray nodes;
    Inventory inv;
    List<int> itemIndexQueue;

    void Start()
    {
        nodes = JsonToNodesArray();
        inv = FindObjectOfType<Inventory>();
        itemIndexQueue = new List<int>();
        
        SetUI("0");
    }
    
    public NodesArray JsonToNodesArray() { return JsonUtility.FromJson<NodesArray>(file.text); }

    public void MakeChoice(string direction)
    {
        for (int i = 0; i < itemIndexQueue.Count; i++)
        {
            StartCoroutine(AddToInventory(true));
        }
        
        if (currentPath.Equals("0"))
        {
            SetUI(direction);
        }
        else
        {
            SetUI(currentPath + "_" + direction);
        }

        StopHovering();
    }

    void SetUI(string target)
    {
        Node temp = nodes.GetNode(target);

        if (temp != null)
        {
            if (inv.items == null)
            {
                inv.SetItemsArray();
            }

            for (int i = 0; i < inv.items.Length; i++)
            {
                if (temp.NodeNum.Equals(inv.items[i].Origin))
                {
                    itemIndexQueue.Add(i);
                    StartCoroutine(AddToInventory(false));
                }
                else if (temp.NodeNum.Equals(inv.items[i].Target))
                {
                    // TODO
                }
            }
            
            description.text = SearchAndFormat(temp.Description, false);
            north.text = SearchAndFormat(temp.North, false);
            south.text = SearchAndFormat("<wiggle a=0.08>" + temp.South + "</wiggle>", true);
            west.text = SearchAndFormat(temp.West, false);
            east.text = SearchAndFormat(temp.East, false);
            nodeName.text = SearchAndFormat(temp.NodeName.ToUpper(), false);
            
            currentPath = target;

            inv.UpdateInventory(currentPath);
        
            FindObjectOfType<TextTimeline>().Reset();
        }
    }

    IEnumerator AddToInventory(bool immediate)
    {
        if (!immediate)
        {
            yield return new WaitForSeconds(16);
        }

        inv.AddToInventory(itemIndexQueue[0]);
        itemIndexQueue.RemoveAt(0);
    }

    void StopHovering()
    {
        foreach (ButtonHover i in FindObjectsOfType<ButtonHover>())
        {
            i.StopHovering();
        }
    }
    
    public static string SearchAndFormat(string str, bool isSouth)
    {
        string[] keywords = { "she", "her", "hers" };
        
        string[] split = str.Split(' ');
        string temp = "";

        foreach (string i in split)
        {
            string word = i;

            if (word.Length >= 3)
            {
                int len = i.Length;
                
                if (!char.IsLetter(i[^1]))
                {
                    len--;
                }
            
                if (word.Length <= 5 && keywords.Contains(i.ToLower().Substring(0, len)))
                {
                    if (!char.IsLetter(i[^1]))
                    {
                        word = word.Substring(0, len);
                    }

                    if (!isSouth)
                    {
                        word = "<style=sheher><shake a=0.08>" + word + "</shake></style>";   
                    }
                    else
                    {
                        word = "</wiggle><style=sheher><shake a=0.08>" + word + "</shake></style><wiggle a=0.08>";
                    }
                
                    if (!char.IsLetter(i[^1]))
                    {
                        word += i[^1];
                    }
                }   
            }

            temp += word + " ";
        }

        return temp.Substring(0, temp.Length - 1);
    }
}