using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenLoading : MonoBehaviour, IUITransaction, IBackKeyHandle
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

    public void OnButtonShowMainMenu()
    {
        UIManger.Instance.MainMenu.Show();
    }

    public EnumScreenState ScreenState => EnumScreenState.Loading;

    public void OnBackKeyHandle()
    {
        Hide();
    }
}