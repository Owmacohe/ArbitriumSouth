using System;

[System.Serializable]
public class NodesArray
{
    public Node[] Nodes;

    public Node GetNode(string target)
    {
        foreach (Node i in Nodes)
        {
            if (i.NodeNum.Equals(target))
            {
                return i;
            }
        }

        return null;
    }

    public static int GetClue(string start, string end)
    {
        if (start.Equals("0"))
        {
            return int.Parse(end.Substring(0, 1));
        }
        
        if (start.Length < end.Length)
        {
            string[] startSplit = start.Split('_');
            string[] endSplit = end.Split('_');

            for (int i = 0; i < endSplit.Length; i++)
            {
                if (i < startSplit.Length)
                {
                    if (startSplit[i] != endSplit[i])
                    {
                        return -1;
                    }
                }
                else
                {
                    return int.Parse(endSplit[i]);
                }
            }
        }

        return -1;
    }
}