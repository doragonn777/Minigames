using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUI : BaseUI
{
    public void GoGameSelect()
    {
        GameStateManager.Instance.ChangeState(GameState.GameSelect);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
