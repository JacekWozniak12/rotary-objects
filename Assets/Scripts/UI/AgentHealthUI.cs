using UnityEngine;
using TMPro;

public class AgentHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;

    protected void On_HealthChanged(int value)
    {
        textComponent.text = ""+value;
    }
}