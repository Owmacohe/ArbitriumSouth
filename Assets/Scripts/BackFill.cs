using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFill : MonoBehaviour
{
    public float typeSpeed;
    public int charLength;
    public GameObject fillPrefab;

    public static bool fillsAdded = false;

    void Start()
    {
        //Utilities.typeText("backfill");

        StartCoroutine(writeWait(typeSpeed));
    }

    IEnumerator writeWait(float speed)
    {
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

            if (i >= charLength - 1)
            {
                fillsAdded = true;
            }

            yield return new WaitForSeconds(speed);
        }
    }
}
