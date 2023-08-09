using System;
using UnityEngine;

/// <summary>
/// Defines the behaviour of the game, spawning etc
/// </summary>
public class GameLoopManager : Singleton<GameLoopManager>
{
    public Action GameFinished;

    [field: Header("Pooling")]
    [field: SerializeField] public PoolingSystem Agents { get; private set; }
    [field: SerializeField] public PoolingSystem Projectiles { get; private set; }

    [Header("Settings")]
    [SerializeField] private Vector2 BottomLeft, TopRight;
    [SerializeField] private int[] agentAmounts;

    private bool game;
    private int currentAgentAmount;

    protected override void OnAwake()
    {
        GameUIManager.I.Populate(agentAmounts);
        ShowChooseScreen();
    }

    public void StartNewGame(int amount)
    {
        if (Agents.GetAmount() > amount + 100)
        {
            Agents.RemovePoolObjects(100);
        }

        if (Agents.GetAmount() < amount)
        {
            Agents.AddPoolObjects(amount - Agents.GetAmount());
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

            var agent = (Agent) obj;
            agent.Death += On_AgentDead;

            obj.Spawn();
        }

        currentAgentAmount = amount;
        game = true;
        GameUIManager.I.ExitAmountScreen();
    }

    public void SetObjectWithinMargins(Transform transform)
    {
        transform.position = new Vector3(
                    UnityEngine.Random.Range(BottomLeft.x, TopRight.x), 
                    0, 
                    UnityEngine.Random.Range(BottomLeft.y, TopRight.y));
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

        if (currentAgentAmount <= 1)
        {
            game = false;
            GameFinished?.Invoke();
            ShowPostScreen();
        }
    }
}