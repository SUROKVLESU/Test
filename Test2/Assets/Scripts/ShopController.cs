using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR;
public class ShopController:MonoBehaviour
{
    [SerializeField] private GameObject PrefabOfferWindow;
    [SerializeField] private GameObject PrefabResourceIcon;

    private RectTransform[] AllLots;

    private GameObject[] Lots = null;
    private const int CountCardsRow = 3;
    private const int CountLines = 2;
    private float Height = 0;
    private const float IntervalBetweenLots = 25;
    private const float ScrollingSpeed = 4;

    private RectTransform CreateLot(int index)
    {
        if(Height<1) Height = PrefabOfferWindow.transform.GetChild(3).GetComponent<RectTransform>().rect.height / CountLines;
        GameObject Lot = Instantiate(PrefabOfferWindow,transform.GetChild(2));
        Lot.transform.GetChild(0).GetComponent<Text>().text = GameManager.instance.GetLots()[index].Heading;
        Lot.transform.GetChild(1).GetComponent<Text>().text = GameManager.instance.GetLots()[index].Description;
        Lot.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.instance.GetLots()[index].SpriteLot;
        Lot.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => 
        {
            Debug.Log("Спасибо за приобретение лота: "+ GameManager.instance.GetLots()[index].Heading);
        });
        if (GameManager.instance.GetLots()[index].IsDiscount)
        {
            Lot.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = 
                GameManager.instance.GetLots()[index].DiscountPrice.ToString()+"Руб.";
            Lot.transform.GetChild(4).GetChild(1).GetChild(0).GetComponent<Text>().text =
                Mathf.Round((GameManager.instance.GetLots()[index].Price - GameManager.instance.GetLots()[index].DiscountPrice) 
                /(GameManager.instance.GetLots()[index].Price/100)).ToString()+"%";

        }
        else
        {
            Lot.transform.GetChild(4).GetChild(0).GetComponent<Text>().text =
                GameManager.instance.GetLots()[index].Price.ToString() + "Руб.";
            Lot.transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
        }
        ReceiveRandomCards(GameManager.instance.GetLots()[index]);
        void ReceiveRandomCards(LotForSale lots)
        {
            Lots = new GameObject[lots.Resources.Length];
            int ystart;
            float xstart;
            int countString = (int)Math.Ceiling((float)lots.Resources.Length / CountCardsRow);
            if (countString == 1) ystart = 0;
            else if (countString % 2 == 0) ystart = countString / 2;
            else ystart = countString / 2 + 1;
            xstart = (CountCardsRow - (lots.Resources.Length - CountCardsRow * (countString - 1))) / 2f;
            for (int i = 0, xi = 0, yi = ystart; i < lots.Resources.Length; i++)
            {
                Lots[i] = Instantiate(PrefabResourceIcon, Lot.transform.GetChild(3).transform);
                Lots[i].GetComponent<Image>().sprite = GameManager.instance.GetSpriteResource(lots.Resources[i].GetTypeResource());
                Lots[i].transform.GetChild(0).GetComponent<Text>().text = lots.Resources[i].GetCount().ToString();
                if (i < CountCardsRow * (lots.Resources.Length / CountCardsRow))
                {
                    Lots[i].transform.localPosition = new Vector2
                        (Height * (xi - (CountCardsRow / 2)), (Height / 2) * yi);
                }
                else
                {
                    Lots[i].transform.localPosition = new Vector2
                        (Height * (xi - (CountCardsRow / 2) + xstart), (Height / 2) * yi);
                }
                xi++;
                if ((i + 1) % CountCardsRow == 0)
                {
                    xi = 0;
                    yi -= 2;
                }
            }
        }
        RectTransform rectTransform = Lot.GetComponent<RectTransform>();
        rectTransform.localScale = new Vector2(GameManager.instance.InterfaceMultiplier, GameManager.instance.InterfaceMultiplier);
        return rectTransform;
    }
    public void CreateAllLots()
    {
        AllLots = new RectTransform[GameManager.instance.GetLots().Length];
        for (int i = 0; i < AllLots.Length; i++)
        {
            AllLots[i] = CreateLot(i);
        }
        AllLots[0].localPosition = new Vector2(-Screen.currentResolution.width/4,0);
        for (int i = 1; i < AllLots.Length; i++)
        {
            AllLots[i].localPosition = AllLots[i-1].localPosition 
                + new Vector3((AllLots[i].rect.width+ IntervalBetweenLots)*GameManager.instance.InterfaceMultiplier, 0,0);
        }
        LotsRectTransform = transform.GetChild(2).GetComponent<RectTransform>();
        LotsRectTransform.localPosition = new Vector2();
    }
    private Vector2 OldPosition;
    private RectTransform LotsRectTransform;
    private void OnMouseDown()
    {
        OldPosition = Input.mousePosition;
    }
    private void OnMouseDrag()
    {
        if (LotsRectTransform.localPosition.x+ (-OldPosition.x + Input.mousePosition.x) * Time.deltaTime * ScrollingSpeed > 0)
            return;
        if (LotsRectTransform.localPosition.x + (-OldPosition.x + Input.mousePosition.x) * Time.deltaTime * ScrollingSpeed <
            -((GameManager.instance.GetLots().Length - 1) * (AllLots[0].rect.width + IntervalBetweenLots * GameManager.instance.InterfaceMultiplier)) * GameManager.instance.InterfaceMultiplier / 2) return;
        LotsRectTransform.localPosition += new Vector3((-OldPosition.x + Input.mousePosition.x) * Time.deltaTime * ScrollingSpeed, 0, 0);
    }
}

