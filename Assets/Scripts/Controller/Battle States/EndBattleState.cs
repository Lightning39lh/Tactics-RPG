using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndBattleState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Campaign1")
            SceneManager.LoadScene("Campaign2");
        else if (scene.name == "Campaign2")
            SceneManager.LoadScene("Campaign3");
        else if (scene.name == "Campaign2") //si es el 3 o el principio
            SceneManager.LoadScene("Campaign1");
        else
            SceneManager.LoadScene("Campaign1");

    }
}
