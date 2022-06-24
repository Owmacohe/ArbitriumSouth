using System;
using UnityEngine;

public class JSONToNodesArray : MonoBehaviour
{
    [SerializeField] TextAsset file;
    
    public NodesArray Generate() { return JsonUtility.FromJson<NodesArray>(file.text); }
    
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
}