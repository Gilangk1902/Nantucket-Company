using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipInteractionUI_Ship : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject shipInteractionPanel;

    [Header("Buttons")]
    [SerializeField] private Button lowerBoatsButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        Button _closeButton = closeButton.GetComponent<Button>();
        _closeButton.onClick.AddListener(CloseOnClick);

        Button _lowerBoatsButton = lowerBoatsButton.GetComponent<Button>();
        _lowerBoatsButton.onClick.AddListener(LowerBoatOnClick);
    }

    private void LowerBoatOnClick() {
        uiController.CloseInteractionPanel(GameState.Lower, PlayerControlMode.Boat);
    }

    private void CloseOnClick(){ uiController.CloseInteractionPanel(GameState.Ship, PlayerControlMode.Ship); }

    public GameObject GetShipInteractionPanel() { return this.shipInteractionPanel; }
}
