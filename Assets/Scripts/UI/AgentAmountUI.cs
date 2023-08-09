using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgentAmountUI : MonoBehaviour
{
    [SerializeField] private Button buttonComponent;
    [SerializeField] private TMP_Text textComponent;

    [Header("Dynamic")]
    [SerializeField] private int amount;

    private void Awake()
    {
        buttonComponent.onClick.AddListener(On_Click);
    }

    public void Init(int value)
    {
        amount = value;
        textComponent.text = value;
    }

    private void On_Click()
    {
        GameLoopManager.I.StartNewGame(amount);
    }
}