using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFill : MonoBehaviour
{
    public int charLength;
    public GameObject fillPrefab;
    public TMP_FontAsset titleFont;

    void Start()
    {
        //Utilities.typeText("backfill");

        float xPlacement = -8.5f;
        float yPlacement = 4.5f;

        for (int i = 0; i < charLength; i++)
        {
            var newFill = Instantiate(fillPrefab);
            newFill.transform.SetParent(gameObject.transform);

            newFill.transform.position = new Vector2(xPlacement, yPlacement);
            xPlacement += 0.5f;

            if (i.Equals(34) || i.Equals(69) || i.Equals(104) || i.Equals(139) || i.Equals(174) || i.Equals(209) || i.Equals(244) || i.Equals(279) || i.Equals(314) || i.Equals(349) || i.Equals(384) || i.Equals(419) || i.Equals(454))
            {
                xPlacement = -8.5f;
                yPlacement -= 0.75f;
            }

            if (i.Equals(75) || i.Equals(76) || i.Equals(77) || i.Equals(78) || i.Equals(79))
            {
                newFill.GetComponent<TextMeshProUGUI>().font = titleFont;
            }

            switch (i)
            {
                case 75:
                    newFill.GetComponent<TextMeshProUGUI>().text = "S";
                    break;
                case 76:
                    newFill.GetComponent<TextMeshProUGUI>().text = "T";
                    break;
                case 77:
                    newFill.GetComponent<TextMeshProUGUI>().text = "A";
                    break;
                case 78:
                    newFill.GetComponent<TextMeshProUGUI>().text = "R";
                    break;
                case 79:
                    newFill.GetComponent<TextMeshProUGUI>().text = "T";
                    break;
            }
        }
    }
}
