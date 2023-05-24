using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidingBackground : MonoBehaviour
{
    [SerializeField] Sprite img;
    [SerializeField] Color colour;
    [SerializeField] float size = 3;
    [SerializeField] float speed = 0.025f;

    RectTransform rect;
    List<RectTransform> tiles = new List<RectTransform>();
    int destroyCount;

    int lastPosition;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        
        NewGroupRange(6, -6);
    }

    void FixedUpdate()
    {
        rect.anchoredPosition += Vector2.right * speed;

        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i] != null && Vector3.Distance(Vector3.zero, tiles[i].localPosition) > 2000)
            {
                destroyCount++;
                GameObject temp = tiles[i].gameObject;
                
                if (destroyCount >= 3)
                {
                    destroyCount = 0;
                    NewGroup(lastPosition - 1);
                    tiles.RemoveAt(i);
                }
                
                Destroy(temp);
            }
        }

    }

    void NewTile(Transform parent, Vector2 position)
    {
        RectTransform tile = new GameObject("Tile").AddComponent<RectTransform>();
        
        Image image = tile.gameObject.AddComponent<Image>();
        image.sprite = img;
        image.color = colour;

        tile.SetParent(parent);
        tile.localScale = Vector3.one * size;
        
        tile.localPosition = position * (size * 100);
        
        tiles.Add(tile);
    }

    void NewGroup(int position)
    {
        NewTile(transform, Vector2.zero + (Vector2.right * position));
        NewTile(transform, Vector2.up + (Vector2.right * position));
        NewTile(transform, Vector2.down + (Vector2.right * position));

        lastPosition = position;
    }
    
    void NewGroupRange(int from, int to)
    {
        for (int i = from; i > to; i--)
            NewGroup(i);
    }
}