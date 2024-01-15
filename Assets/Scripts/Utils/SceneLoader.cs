using UnityEngine.SceneManagement;

public class SceneLoader
{
    string[] StateToSceneNameTable =
    {                   //�Ή�����GameState
        "Title",        //Title
        "GameSelect",   //GameSelect
        "InGame",       //InGame
        "ExitGame"      //ExitGame
    };

    string[] GameToSceneNameTable =
    {
        "Janken"
    };

    public void LoadScene(GameState state)
    {
        SceneManager.LoadScene(StateToSceneNameTable[(int)state]);
    }

    public void LoadScene(Games game)
    {
        SceneManager.LoadScene(GameToSceneNameTable[(int)game]);
    }
}
