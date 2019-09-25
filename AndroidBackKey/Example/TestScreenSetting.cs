using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreenSetting : MonoBehaviour, IUITransaction, IBackKeyHandle
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

    public EnumScreenState ScreenState => EnumScreenState.Setting;

    public void OnBackKeyHandle()
    {
        Hide();
    }
}