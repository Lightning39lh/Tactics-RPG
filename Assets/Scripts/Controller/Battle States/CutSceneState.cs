using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutSceneState : BattleState
{
    ConversationController conversationController;
    ConversationData data;

    protected override void Awake()
    {
        base.Awake();
        conversationController = owner.GetComponentInChildren<ConversationController>();
        
    }
    public override void Enter()
    {
        base.Enter();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Campaign1")
        {
            if (IsBattleOver())
            {
                if (DidPlayerWin())
                    data = Resources.Load<ConversationData>("Conversations/OutroCampaign1Win");
                else
                    data = Resources.Load<ConversationData>("Conversations/OutroCampaignLose");
            }
            else
            {
                data = Resources.Load<ConversationData>("Conversations/IntroCampaign1");
            }
        }
        if (scene.name == "Campaign2")
        {
            if (IsBattleOver())
            {
                if (DidPlayerWin())
                    data = Resources.Load<ConversationData>("Conversations/OutroCampaign2Win");
                else
                    data = Resources.Load<ConversationData>("Conversations/OutroCampaignLose");
            }
            else
            {
                data = Resources.Load<ConversationData>("Conversations/IntroCampaign2");
            }
        }
        if (scene.name == "Campaign3")
        {
            if (IsBattleOver())
            {
                if (DidPlayerWin())
                    data = Resources.Load<ConversationData>("Conversations/OutroCampaign3Win");
                else
                    data = Resources.Load<ConversationData>("Conversations/OutroCampaignLose");
            }
            else
            {
                data = Resources.Load<ConversationData>("Conversations/IntroCampaign3");
            }
        }
        conversationController.Show(data);
    }
    public override void Exit()
	{
		base.Exit();
		if (data)
			Resources.UnloadAsset(data);
	}
protected override void AddListeners()
    {
        base.AddListeners();
        ConversationController.completeEvent += OnCompleteConversation;
    }
    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        ConversationController.completeEvent -= OnCompleteConversation;
    }
    protected override void OnFire(object sender, InfoEventArgs<int> e)
    {
        base.OnFire(sender, e);
        conversationController.Next();
    }
    void OnCompleteConversation(object sender, System.EventArgs e)
    {
        if (IsBattleOver())
            owner.ChangeState<EndBattleState>();
        else
            owner.ChangeState<SelectUnitState>();
    }
}