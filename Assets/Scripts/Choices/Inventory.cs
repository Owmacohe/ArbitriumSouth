using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] TextAsset itemList;
    [SerializeField] Sprite[] inventorySprites;
    
    [HideInInspector] public bool up;
    
    Transform layout;
    GameObject itemPrefab;
    [HideInInspector] public Item[] items;

    void Start()
    {
        layout = GetComponentInChildren<HorizontalLayoutGroup>().transform;
        itemPrefab = Resources.Load<GameObject>("Item");
    }

    void FixedUpdate()
    {
        if (up && layout.localPosition.y < -175)
        {
            layout.localPosition += Vector3.up * 2;
        }
        else if (!up && layout.localPosition.y > -200)
        {
            layout.localPosition -= Vector3.up;
        }
    }

    public void SetItemsArray()
    {
        string[] split = itemList.text.Split('\n');
        items = new Item[split.Length];

        for (int i = 0; i < split.Length; i++)
        {
            string[] splitSplit = split[i].Split(',');
            items[i] = new Item(splitSplit[0].Trim(), splitSplit[1].Trim(), splitSplit[2].Trim());
        }
    }

    public void AddToInventory(int index)
    {
        GameObject temp = Instantiate(itemPrefab, layout);
        temp.GetComponentsInChildren<Image>()[1].sprite = inventorySprites[index];
        temp.AddComponent<FadeIn>().wait = 0;
            
        GameObject button = temp.GetComponentInChildren<BoxCollider2D>().gameObject;
        //button.GetComponent<Button>().onClick.AddListener();
            
        button.transform.localPosition = new Vector3(
            -70 + (index * 20), 
            -50, 
            0
        );

        items[index].Collected = true;
        items[index].Object = temp;

        UpdateInventory(items[index].Origin);
    }

    public void UpdateInventory(string current)
    {
        foreach (Item i in items)
        {
            if (i.Collected)
            {
                int direction = NodesArray.GetClue(current, i.Target);
                string text = "";

                switch (direction)
                {
                    case 1:
                        text = "North";
                        break;
                    case 2:
                        text = "West";
                        break;
                    case 3:
                        text = "East";
                        break;
                    case 4:
                        text = "South";
                        break;
                    case -1:
                        text = "None";
                        break;
                }
        
                i.Object.GetComponentInChildren<TMP_Text>().text = text.ToUpper();
            }
        }
    }
}