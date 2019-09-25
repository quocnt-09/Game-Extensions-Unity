using UnityEngine;

public class UIManger : MonoBehaviour
{
    public static UIManger Instance;

    private void Awake()
    {
        Instance = this;
    }

    public TestScreenMainMenu MainMenu;
    public TestScreenSetting Setting;
    public TestScreenShop Shop;
    public TestScreenDialog Dialog;
}