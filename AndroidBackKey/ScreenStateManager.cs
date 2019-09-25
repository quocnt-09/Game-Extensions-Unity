using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenStateManager : MonoBehaviour
{
    public static ScreenStateManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public EnumScreenState currentScreen = EnumScreenState.None;
    private Stack<IBackKeyHandle> stackScreens;
#if UNITY_EDITOR
    public EnumScreenState[] CheckScreen;
#endif

    public void Start()
    {
        currentScreen = EnumScreenState.None;
        Initialize();
    }

    private void Initialize()
    {
        stackScreens = new Stack<IBackKeyHandle>();
    }

    public void PushScreenState(IBackKeyHandle handle)
    {
        if (!stackScreens.Contains(handle))
        {
            stackScreens.Push(handle);
            currentScreen = handle.ScreenState;
        }

        SetTopScreen();
    }

    public void PopScreenState(IBackKeyHandle handle)
    {
        if (stackScreens.Contains(handle))
        {
            stackScreens.Pop();
        }

        SetTopScreen();
    }

    private IBackKeyHandle FindTopActiveMenu()
    {
        if (stackScreens.Count <= 0) return null;

        var index = 0;
        while (stackScreens.Count > 0)
        {
            var screen = stackScreens.ElementAt(index);
            var obj = screen as MonoBehaviour;
            if (obj != null && obj.gameObject.activeInHierarchy) return screen;
            index++;
        }

        return null;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            var screen = FindTopActiveMenu();
            if (screen != null && screen.ScreenState == currentScreen)
            {
                screen.OnBackKeyHandle();
                PopScreenState(screen);
            }
        }
    }

    void SetTopScreen()
    {
        CheckScreen = stackScreens.Select(k => k.ScreenState).ToArray();
        if (stackScreens.Count > 0)
        {
            currentScreen = stackScreens.ElementAt(0).ScreenState;
        }
        else
        {
            currentScreen = EnumScreenState.None;
        }
    }

   
}