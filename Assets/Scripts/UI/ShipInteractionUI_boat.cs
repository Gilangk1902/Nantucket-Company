using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipInteractionUI_boat : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private UIController uiController;
    [SerializeField] private GameObject shipInteractionPanel;

    [Header("Buttons")]
    [SerializeField] private Button heaveBoatsButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        Button _closeButton = closeButton.GetComponent<Button>();
        _closeButton.onClick.AddListener(CloseOnClick);

        Button _heaveBoatsButton = heaveBoatsButton.GetComponent<Button>();
        _heaveBoatsButton.onClick.AddListener(HeaveBoatOnClick);
    }

    private void HeaveBoatOnClick()
    {
        uiController.CloseInteractionPanel(GameState.Ship, PlayerControlMode.Ship);
    }

    private void CloseOnClick() { uiController.CloseInteractionPanel(GameState.Lower, PlayerControlMode.Boat); }

    public GameObject GetShipInteractionPanel() { return this.shipInteractionPanel; }
}
