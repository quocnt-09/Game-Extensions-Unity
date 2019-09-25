using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenShop : MonoBehaviour, IUITransaction, IBackKeyHandle
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

    public void OnButtonDialog()
    {
        UIManger.Instance.Dialog.Show();
    }

    public EnumScreenState ScreenState => EnumScreenState.Shop;

    public void OnBackKeyHandle()
    {
        Hide();
    }
}