using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    [SerializeField] private Canvas AmountUICanvas;
    [SerializeField] private Canvas NewGameCanvas;

    [SerializeField] private RectTransform amountTransform;
    [SerializeField] private AgentAmountUI agentAmountUI;

    public void Populate(int[] amounts)
    {
        foreach (var amount in amounts)
        {
            AgentAmountUI current = Instantiate(agentAmountUI);
            current.transform.SetParent(amountTransform, false);
            current.Init(amount);
        }
    }

    public void EnterMenuScreen()
    {
        NewGameCanvas.gameObject.SetActive(true);
    }

    public void ExitMenuScreen()
    {
        NewGameCanvas.gameObject.SetActive(false);
        AmountUICanvas.gameObject.SetActive(true);
    }

    public void ExitAmountScreen()
    {
        NewGameCanvas.gameObject.SetActive(false);
        AmountUICanvas.gameObject.SetActive(false);
    }
}