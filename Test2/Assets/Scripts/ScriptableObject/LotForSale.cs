using UnityEngine;
[CreateAssetMenu(fileName = "LotForSale")]
public class LotForSale: ScriptableObject
{
    [TextArea]
    public string Heading;
    [TextArea]
    public string Description;
    public float Price;
    public bool IsDiscount;
    public float DiscountPrice;
    public Resource[] Resources;
    public Sprite SpriteLot;
}

