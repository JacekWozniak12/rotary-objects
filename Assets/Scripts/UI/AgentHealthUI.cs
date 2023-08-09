using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgentHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private Image imageComponent;

    protected void On_Hide()
    {
        textComponent.enabled = false;
        imageComponent.enabled = false;
    }

    protected void On_Show()
    {
        textComponent.enabled = true;
        imageComponent.enabled = true;
        transform.LookAt(Camera.main.transform);
    }

    protected void On_Rotate()
    {
        transform.LookAt(Camera.main.transform);
    }

    protected void On_HealthChanged(int value)
    {
        textComponent.text = "" + value;

        // it should be better but time
        if (value == 3)
        {
            textComponent.color = Color.green;
        }

        if (value == 2)
        {
            textComponent.color = Color.yellow;
        }

        if (value == 1)
        {
            textComponent.color = Color.red;
        }
    }

    private void Awake()
    {
        var agent = GetComponentInParent<Agent>();
        agent.HealthChanged += On_HealthChanged;
        agent.Hide += On_Hide;
        agent.Show += On_Show;
        agent.Rotate += On_Rotate;
    }

    private void OnEnable()
    {
        transform.LookAt(Camera.main.transform);
    }
}