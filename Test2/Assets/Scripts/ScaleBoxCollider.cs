using UnityEngine;

public class ScaleBoxCollider : MonoBehaviour
{
    public float Scale_X = 1;
    public float Scale_Y = 1;
    private void Awake()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        gameObject.GetComponent<BoxCollider>().size = 
            new Vector2(rectTransform.rect.width* Scale_X, rectTransform.rect.height* Scale_Y);
    }
}
