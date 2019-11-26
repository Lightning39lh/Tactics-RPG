using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleController : StateMachine
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    public Tile currentTile { get { return board.GetTile(pos); } }
    public AbilityMenuPanelController abilityMenuPanelController;
    public Turn turn = new Turn();
    public List<Unit> units = new List<Unit>();
    public StatPanelController statPanelController;
    public IEnumerator round;
    public HitSuccessIndicator hitSuccessIndicator;
    public BattleMessageController battleMessageController;
    public ComputerPlayer cpu;
    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Campaign1")
            ChangeState<InitCampaign1>();
        else if (scene.name == "Campaign2")
            ChangeState<InitCampaign2>();
        else if (scene.name == "Campaign3") 
            ChangeState<InitCampaign3>();
        else
            ChangeState<InitCampaign1>();
    }
}