using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhaleInteractionUI_Boat : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject whaleInteractionPanel;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        Button _closeButton = closeButton.GetComponent<Button>();
        _closeButton.onClick.AddListener(OnClick);
    }

    private void OnClick() { uiController.CloseInteractionPanel(GameState.Lower, PlayerControlMode.Boat);  }
    public GameObject GetWhaleInteractionPanel() { return whaleInteractionPanel; }
}
