using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewGameButtonUI : MonoBehaviour
{
    [SerializeField] private Button buttonComponent;

    private void Awake()
    {
        buttonComponent.onClick.AddListener(On_Click);
    }

    private void On_Click()
    {
        GameLoopManager.I.ShowChooseScreen();
    }
}