using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GenerateBackgroundText : MonoBehaviour
{
    public enum Layouts { Full, Empty, Entry, Prologue, Main, FullMain, Epilogue }
    public Layouts layout;
    [SerializeField] TMP_StyleSheet sheet;

    [Header("Characters")]
    [Range(1, 350)] [SerializeField] float cellSize = 10;
    [Range(0, 800)] public int changesPerFrame = 1;
    
    [Header("Colours")]
    [SerializeField] Color mainColour;
    [SerializeField] Color fadeColour;
    [Range(1.5f, 4.5f)] [SerializeField] float falloff = 4;
    [SerializeField] float maximumDarkness = 0.95f;

    RectTransform rectTransform;
    GridLayoutGroup grid;
    List<TMP_Text> characters;
    Dictionary<TMP_Text, Vector2> screenPositions;
    
    int rowNum, columnNum;
    bool hasLayoutBeenLoaded;
    Dictionary<TMP_Text, Vector2> gridPositions;
    List<TMP_Text> chunkCharacters;

    void Start()
    {
        CreateContainers();
        SetCharacters();
        WaitSetColours(layout);
    }

    public void CreateContainers()
    {
        if (!GetComponent<GridLayoutGroup>())
        {
            grid = gameObject.AddComponent<GridLayoutGroup>();
        }
        
        rectTransform = GetComponent<RectTransform>();
        screenPositions = new Dictionary<TMP_Text, Vector2>();
        characters = new List<TMP_Text>();
        chunkCharacters = new List<TMP_Text>();
        gridPositions = new Dictionary<TMP_Text, Vector2>();
    }

    void FixedUpdate()
    {
        if (hasLayoutBeenLoaded && characters.Count > 0)
        {
            for (int i = 0; i < changesPerFrame; i++)
            {
                if (Random.Range(0f, 1f) <= 0.2f)
                {
                    TMP_Text temp = characters[Random.Range(0, characters.Count)];
                
                    if (Random.Range(0, 101) == 0)
                    {
                        temp.text = "<shake a=0.2>" + RandomChar.Get() + "</shake>";
                        temp.textStyle = sheet.GetStyle("Alt");
                        StartCoroutine(StopShaking(temp));
                    }
                    else
                    {
                        temp.text = RandomChar.Get().ToString();
                    }
                }
            }
        }
    }

    IEnumerator StopShaking(TMP_Text text)
    {
        yield return new WaitForSeconds(1);

        text.text = RandomChar.Get().ToString();
        text.textStyle = sheet.GetStyle("Normal");
    }

    public void SetCharacters()
    {
        grid.cellSize = Vector2.one * cellSize;
        grid.childAlignment = TextAnchor.MiddleCenter;

        grid.spacing = Vector2.one * ((Screen.width / 2f) * 0.001f);

        int numCharacters = (int) ((Screen.width * Screen.height) / 100f);
        
        GameObject character = Resources.Load<GameObject>("Character");

        for (int i = 0; i < numCharacters; i++)
        {
            TMP_Text temp;
            
            if (i < characters.Count)
            {
                temp = characters[i];
            }
            else
            {
                temp = Instantiate(character, transform).GetComponentInChildren<TMP_Text>();
                characters.Add(temp);
            }
            
            temp.fontSize = cellSize;
        }
    }

    public void WaitSetColours(Layouts layout)
    {
        this.layout = layout;
        
        Invoke(nameof(SetColours), 0.01f);
    }

    void SetColours()
    {
        foreach (TMP_Text i in chunkCharacters)
        {
            characters.Add(i);
            i.text = RandomChar.Get().ToString();
        }
        
        if (!hasLayoutBeenLoaded)
        {
            List<TMP_Text> toBeRemoved = new List<TMP_Text>();
        
            foreach (TMP_Text j in characters)
            {
                if (!screenPositions.Keys.Contains(j))
                {
                    screenPositions.Add(j, j.transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition);
                }
            
                if (screenPositions[j].y < -rectTransform.rect.height)
                {
                    toBeRemoved.Add(j);
                }
                else
                {
                    j.text = RandomChar.Get().ToString();
                    
                    float distance = Vector2.Distance(new Vector2(rectTransform.rect.width, -rectTransform.rect.height) / 2f, screenPositions[j]);
                                        
                    float lerpAmount = distance / (falloff * 100f);

                    if (lerpAmount > maximumDarkness)
                    {
                        lerpAmount = maximumDarkness;
                    }
            
                    j.color = Color.Lerp(mainColour, fadeColour, lerpAmount);
                }
            }

            foreach (TMP_Text k in toBeRemoved)
            {
                characters.Remove(k);
                Destroy(k.transform.parent.gameObject);
            }

            GetLayoutWidthAndHeight();
        }
        
        chunkCharacters = new List<TMP_Text>();
        
        if (layout.Equals(Layouts.Empty))
        {
            RemoveChunkFromCentre(new Vector2(200, 200));
        }
        else if (layout.Equals(Layouts.Entry))
        {
            RemoveChunkFromCentre(new Vector2(60, 29));
        }
        else if (layout.Equals(Layouts.Prologue))
        {
            RemoveChunkFromCentre(new Vector2(60, 200));
        }
        else if (layout.Equals(Layouts.Main))
        {
            RemoveChunkFromCentre(new Vector2(25, 15));

            Vector2 optionSize = new Vector2(14, 6);
            RemoveChunk(new Vector2(columnNum / 2, (rowNum / 2) - 14), optionSize);
            RemoveChunk(new Vector2(columnNum / 2, (rowNum / 2) + 14), optionSize);
            RemoveChunk(new Vector2((columnNum / 2) + 23, rowNum / 2), optionSize);
            RemoveChunk(new Vector2((columnNum / 2) - 23, rowNum / 2), optionSize);
            
            RemoveChunk(new Vector2(columnNum, 0), new Vector2(optionSize.x * 3, optionSize.y));
        }
        else if (layout.Equals(Layouts.FullMain))
        {
            RemoveChunkFromCentre(new Vector2(25, 15));
        }
        else if (layout.Equals(Layouts.Epilogue))
        {
            Vector2 chunkSize = new Vector2(26, 32);
            RemoveChunk(new Vector2((columnNum / 2) + 20, rowNum / 2), chunkSize);
            RemoveChunk(new Vector2((columnNum / 2) - 20, rowNum / 2), chunkSize);
        }
    }
    
    void GetLayoutWidthAndHeight()
    {
        Vector2 initialPos = characters[0].transform.parent.gameObject.GetComponent<RectTransform>().anchoredPosition;

        float currentColumn = initialPos.x;
        float currentRow = initialPos.y;

        columnNum = 1;
        rowNum = 1;
        
        Transform offset = new GameObject("Offset").transform;
        offset.SetParent(transform);
        
        foreach (TMP_Text i in characters)
        {
            GameObject parent = i.transform.parent.gameObject;
            Vector2 pos = parent.GetComponent<RectTransform>().anchoredPosition;
            
            if (pos.x > currentColumn)
            {
                currentColumn = pos.x;
                columnNum++;
            }

            if (pos.y < currentRow)
            {
                currentRow = pos.y;
                rowNum++;

                currentColumn = pos.x;
                columnNum = 1;
            }
            
            gridPositions.Add(i, new Vector2(columnNum, rowNum));
            
            parent.transform.SetParent(offset);
        }
        
        offset.localPosition = Vector3.up * 4;
        offset.localScale *= 0.8f;

        hasLayoutBeenLoaded = true;
    }

    void RemoveChunkFromCentre(Vector2 bounds)
    {
        RemoveChunk(
            new Vector2(columnNum / 2, rowNum / 2),
            bounds
        );
    }

    void RemoveChunk(Vector2 origin, Vector2 bounds)
    {
        List<TMP_Text> toBeRemoved = new List<TMP_Text>();

        origin = new Vector2(
            origin.x - (bounds.x / 2) + 1,
            origin.y - (bounds.y / 2) + 1
        );
        
        foreach (TMP_Text i in characters)
        {
            if (gridPositions[i].x >= origin.x && gridPositions[i].x <= (origin.x + bounds.x) &&
                gridPositions[i].y >= origin.y && gridPositions[i].y <= (origin.y + bounds.y))
            {
                toBeRemoved.Add(i);
                chunkCharacters.Add(i);
            }
        }
        
        foreach (TMP_Text j in toBeRemoved)
        {
            characters.Remove(j);
            j.text = "";
        }
        
        foreach (TMP_Text k in chunkCharacters)
        {
            k.text = "";
        }
    }
}
