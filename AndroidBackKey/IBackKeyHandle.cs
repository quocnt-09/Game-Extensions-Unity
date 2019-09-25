using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUITransaction
{
    void Show();
    void Hide();
}
public interface IBackKeyHandle
{
    EnumScreenState ScreenState { get; }
    void OnBackKeyHandle();
}

public enum EnumScreenState
{
    None,
    Loading,
    MainMenu,
    Shop,
    Setting,
    Dialog,
}