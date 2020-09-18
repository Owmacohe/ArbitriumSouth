//***************************************************************************
//
//Design, programming, and art by: Owen Hellum
//Alpha "completed" as of 17/09/2020
//Visit Arbitrium South's itch.io page at: https://omch.itch.io/arbitrium
//
//***************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuHover : MonoBehaviour
{
    private BackFill fillScript; //access to the backboxes fill script

    public float glowDistance; // furthest distance that backboxes can light up around the cursor
    public int randNum; //probability for a backbox to light up when the cursor is moving (the lower the number, the higher the chance)
    public Color32 brightGreen; //standard bright green colour

    //variables for helpint to determine whether the cursor is moving or not
    private Vector3 mouseDelta = Vector3.zero, lastMousePosition = Vector3.zero;

    private void Start()
    {
        fillScript = gameObject.GetComponent<BackFill>();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        //sets the current delta (speed) of mouse, then resets the last position to the current position
        mouseDelta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        //if the backboxes are close to the cursor, if the RNG hits, and if the curor is moving, the backboxes light up
        for (int i = 0; i < fillScript.backBoxes.Length; i++)
        {
            float boxDistance = Vector2.Distance(fillScript.backBoxes[i].transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)); //distance from the given box to the mouse cursor

            if (boxDistance <= glowDistance && Random.Range(0, randNum) == 0 && mouseDelta != new Vector3(0, 0, 0))
            {
                fillScript.backBoxes[i].GetComponent<TextMeshProUGUI>().color = brightGreen;
            }

            //turn them back off when they're far away, or if the mouse stops moving
            if (boxDistance > glowDistance || mouseDelta == new Vector3(0, 0, 0))
            {
                fillScript.setBrightness(fillScript.backBoxes[i]);
            }
        }
    }
}
