using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackFillProper : MonoBehaviour
{
    public float charNum;
    public GameObject fillPrefab;

    void Start() {
        float xPlacement = -8.7f;
        float yPlacement = 4.8f;

        for (int i = 0; i < (1475 / charNum) ; i++)
        {
            var newFill = Instantiate(fillPrefab);
            newFill.transform.SetParent(gameObject.transform);
            newFill.GetComponent<TextMeshProUGUI>().text = getRandomCharacter("all");
            newFill.GetComponent<TextMeshProUGUI>().fontSize = (0.5f * charNum);

            newFill.transform.position = new Vector2(xPlacement, yPlacement);
            xPlacement += (0.3f * charNum);

            if (newFill.transform.position.x > 8.7)
            {
                xPlacement = -8.7f;
                yPlacement -= (0.4f * charNum);
            }

            float distance = Vector2.Distance(newFill.transform.position, GameObject.FindGameObjectWithTag("Middle").transform.position);
            byte alpha = (byte)(distance * 25);
            Debug.Log(alpha);
            newFill.GetComponent<TextMeshProUGUI>().color = new Color32(10, 255, 10, alpha);
        }
    }

    string getRandomCharacter(string givenType)
    {
        var characters = "";

        switch (givenType)
        {
            case "alpha":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                break;
            case "num":
                characters = "0123456789";
                break;
            case "alphaNum":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                break;
            case "all":
                characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-=_+`''~[]{}:;,.?/|";
                break;
        }

        var randomNum = Random.Range(0, characters.Length - 1);
        return characters[randomNum].ToString();
    }
}
