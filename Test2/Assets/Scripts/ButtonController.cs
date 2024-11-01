using UnityEngine;
using UnityEngine.UI;

public class ButtonController:MonoBehaviour
{
    [SerializeField] private Button ShopOpen;
    private void Start()
    {
        ShopOpen.onClick.AddListener(() => 
        { 
            GameController.instance.GetShop().SetActive(true);
            GameController.instance.GetShop().GetComponent<ShopController>().CreateAllLots();

            GameController.instance.GetUserInterface().SetActive(false);

        });
    }
}

