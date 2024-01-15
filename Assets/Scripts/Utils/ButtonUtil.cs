using UnityEngine.UI;

public static class ButtonUtil
{
    public static void EnableButton(Button[] buttons)
    {
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

    public static void DisableButton(Button[] buttons)
    {
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }
    }
}
