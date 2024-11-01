using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
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
    }
    [SerializeField] private GameObject UserInterface;
    public GameObject GetUserInterface() {return UserInterface;}
    [SerializeField] private GameObject Shop;
    public GameObject GetShop() {return Shop;}
}
