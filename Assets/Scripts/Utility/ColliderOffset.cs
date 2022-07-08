using UnityEngine;

public class ColliderOffset : MonoBehaviour
{
    RectTransform colliderTransform, parentTransform, offsetTransform;

    void Start()
    {
        colliderTransform = GetComponent<RectTransform>();
        parentTransform = colliderTransform.parent.gameObject.GetComponent<RectTransform>();
        offsetTransform = parentTransform.parent.gameObject.GetComponent<RectTransform>();
        
        Vector2 pos = offsetTransform.TransformPoint(parentTransform.anchoredPosition);
        colliderTransform.localPosition = new Vector3(pos.x * 18f, pos.y * 28f);
    }
    
    void FixedUpdate()
    {
        Vector2 pos = offsetTransform.TransformPoint(parentTransform.anchoredPosition);
        colliderTransform.localPosition = new Vector3(pos.x * 18f, pos.y * 28f);
    }
}