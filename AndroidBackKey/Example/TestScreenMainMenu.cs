using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenMainMenu : MonoBehaviour, IUITransaction, IBackKeyHandle
{
    public bool isHide = true;

    private void Start()
    {
        if (isHide)
        {
            gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        ScreenStateManager.Instance.PushScreenState(this);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        ScreenStateManager.Instance.PopScreenState(this);
        gameObject.SetActive(false);
    }

    public void OnButtonShowShop()
    {
        UIManger.Instance.Shop.Show();
    }

    public void OnButtonShowSetting()
    {
        UIManger.Instance.Setting.Show();
    }

    public EnumScreenState ScreenState => EnumScreenState.MainMenu;

    public void OnBackKeyHandle()
    {
        Hide();
    }
}