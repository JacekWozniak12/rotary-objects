using UnityEngine;

/// <summary>
/// Defines the behaviour of the game, spawning etc
/// </summary>
public class GameLoopManager : Singleton<GameLoopManager>
{
    [SerializeField] private int[] agentAmounts;
    
    [field: SerializeField] public PoolingSystem Agents { get; private set; }
    [field: SerializeField] public PoolingSystem Projectiles { get; private set; }

    private bool game;
    private int currentAgentAmount;

    protected override void OnAwake()
    {
        GameUIManager.I.Populate(agentAmounts);
        GameUIManager.I.ShowChooseScreen();
    }

    public void StartNewGame(int amount)
    {
        currentAgentAmount = amount;
        Game = true;
        GameUIManager.I.ExitAmountScreen();
    }

    public void ShowChooseScreen()
    {
        GameUIManager.I.ExitMenuScreen();
    }

    public void ShowPostScreen()
    {
        GameUIManager.I.EnterMenuScreen();
    }

    protected void On_AgentDead()
    {
        if (agentAmounts <= 0)
        {
            Game = false;
            currentAgentAmount -= agentAmounts;
        }
    }
}