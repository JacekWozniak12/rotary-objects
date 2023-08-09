using System;
using UnityEngine;

/// <summary>
/// Defines the behaviour of the game, spawning etc
/// </summary>
public class GameLoopManager : Singleton<GameLoopManager>
{
    [SerializeField] private int[] agentAmounts;

    [field: Header("Pooling")]
    [field: SerializeField] public PoolingSystem Agents { get; private set; }
    [field: SerializeField] public PoolingSystem Projectiles { get; private set; }

    [Header("Settings")]
    [SerializeField] private Vector2 BottomLeft, TopRight;

    private bool game;
    private int currentAgentAmount;

    protected override void OnAwake()
    {
        GameUIManager.I.Populate(agentAmounts);
        ShowChooseScreen();
    }

    public void StartNewGame(int amount)
    {
        if (Agents.AllAmount > amount + 100)
        {
            Agents.RemovePoolObjects(100);
        }

        if (Agents.AllAmount < amount)
        {
            Agents.AddPoolObjects(amount - Agents.AllAmount);
        }

        for (int i = 0; i < amount; i++)
        {
            var obj = Agents.GetObject();
            
            obj.gameObject.transform.SetPositionAndRotation(
                new Vector3(
                    UnityEngine.Random.Range(BottomLeft.x, TopRight.x), 
                    0, 
                    UnityEngine.Random.Range(BottomLeft.y, TopRight.y)),
                Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0)
                );

            obj.Spawn();
        }

        currentAgentAmount = amount;
        game = true;
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
        currentAgentAmount -= 1;

        if (currentAgentAmount <= 0)
        {
            game = false;
        }
    }
}