using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        float multiplier = Screen.currentResolution.width / 1920;
        if (multiplier < Screen.currentResolution.height / 1080) InterfaceMultiplier = multiplier;
        else InterfaceMultiplier = Screen.currentResolution.height / 1080;
    }
    [SerializeField] private SpriteArray[] SpriteArray;
    public Sprite GetSpriteResource(Resources type)
    {
        foreach (var sprite in SpriteArray)
        {
            if (sprite.TypeResource == type) return sprite.Sprites;
        }
        return null;
    }
    [SerializeField] private LotForSale[] Lots;
    public LotForSale[] GetLots() { return Lots; }
    public float InterfaceMultiplier;
}
