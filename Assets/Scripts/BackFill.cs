using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFill : MonoBehaviour
{
    public int charLength;
    public GameObject fillPrefab;
    public SpriteRenderer fadeSprite;

    void Start()
    {
        StartCoroutine(waitFade());

        //Utilities.typeText("backfill");

        float xPlacement = -8.5f;
        float yPlacement = 4.5f;

        for (int i = 0; i < charLength; i++)
        {
            var newFill = Instantiate(fillPrefab);
            var textObject = newFill.GetComponent<TextMeshProUGUI>();
            newFill.transform.SetParent(gameObject.transform);

            newFill.transform.position = new Vector2(xPlacement, yPlacement);
            xPlacement += 0.5f;

            if (i.Equals(34) || i.Equals(69) || i.Equals(104) || i.Equals(139) || i.Equals(174) || i.Equals(209) || i.Equals(244) || i.Equals(279) || i.Equals(314) || i.Equals(349) || i.Equals(384) || i.Equals(419) || i.Equals(454))
            {
                xPlacement = -8.5f;
                yPlacement -= 0.75f;
            }

            switch (i)
            {
                case 72:
                    newFill.tag = "STag";
                    break;
                case 73:
                    newFill.tag = "tTag";
                    break;
                case 74:
                    newFill.tag = "aTag";
                    break;
                case 75:
                    newFill.tag = "rTag";
                    break;
                case 76:
                    newFill.tag = "tTag";
                    break;
                case 77:
                    newFill.tag = "_Tag";
                    break;
                case 98:
                    newFill.tag = "ETag";
                    break;
                case 99:
                    newFill.tag = "xTag";
                    break;
                case 100:
                    newFill.tag = "iTag";
                    break;
                case 101:
                    newFill.tag = "tTag";
                    break;
                case 102:
                    newFill.tag = "_Tag";
                    break;
                case 398:
                    newFill.tag = "STag";
                    break;
                case 399:
                    newFill.tag = "eTag";
                    break;
                case 400:
                    newFill.tag = "tTag";
                    break;
                case 401:
                    newFill.tag = "tTag";
                    break;
                case 402:
                    newFill.tag = "iTag";
                    break;
                case 403:
                    newFill.tag = "nTag";
                    break;
                case 404:
                    newFill.tag = "gTag";
                    break;
                case 405:
                    newFill.tag = "sTag";
                    break;
                case 406:
                    newFill.tag = "_Tag";
                    break;
            }
        }
    }

    IEnumerator waitFade()
    {
        yield return new WaitForSeconds(6);

        for (float i = 0; i < 1.1f; i += 0.1f)
        {
            fadeSprite.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.075f);
        }
    }
}
