using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputType : MonoBehaviour
{
    void Update()
    {
        int[] values;

        values = (int[])System.Enum.GetValues(typeof(KeyCode));

        for (int i = 0; i < values.Length; i++)
        {
            //Debug.Log((KeyCode)values[i]);

            if (Input.GetKeyDown((KeyCode)values[i]) && ((values[i] > 96 && values[i] < 122) || (values[i] > 256 && values[i] < 265)))
            {
                gameObject.GetComponent<TextMeshProUGUI>().text += (KeyCode)values[i];
            }
        }
    }
}
