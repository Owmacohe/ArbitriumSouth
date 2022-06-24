using UnityEngine;

public class Item
{
    public string Name { get; }
    public GameObject Object { get; set; }
    public string Origin { get; }
    public string Target { get; }
    public bool Collected { get; set; } // TODO: may not need this

    public Item(string name, string origin, string target)
    {
        Name = name;
        Origin = origin;
        Target = target;
    }
}