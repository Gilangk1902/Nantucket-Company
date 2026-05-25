using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] Game game;
    [SerializeField] private BoatController boatPartyController;
    [SerializeField] private ShipController shipController;
    [SerializeField] private Camera _camera;
    [SerializeField] private UIController uiController;

    [Header("Control")]
    [SerializeField] private PlayerControlMode mode;
    [SerializeField] private LayerMask clickAbleLayer; 

    private GameState GetGameState(){ return game.GetGameController().GetStateHandler().GetGameState(); }
    private bool BoatNearShip() { return boatPartyController.BoatNearShip(); }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            GetObjectClicked();
        }
    }

    private void GetObjectClicked()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickAbleLayer))
        {
            if (hit.collider.CompareTag("Whale"))
            {
                Debug.Log("hit whale");
                uiController.ShowWhaleInteractionPanel(mode, Input.mousePosition);
            }
            if (hit.collider.CompareTag("Ship"))
            {
                Debug.Log("hit ship");
                uiController.ShowShipInteractionPanel(mode, Input.mousePosition);
            }
        }
    }

    private void HeaveBoats()
    {
        game.GetGameController().GetStateHandler().SetGameState(GameState.Ship);
        SetPlayerControlMode(PlayerControlMode.Ship);
    }

    private void LowerBoats()
    {
        Debug.Log("Hit Whale");
        game.GetGameController().GetStateHandler().SetGameState(GameState.Lower);
        SetPlayerControlMode(PlayerControlMode.Boat);
    }


    public PlayerControlMode GetPlayerControlMode(){return mode;}
    public void SetPlayerControlMode(PlayerControlMode mode) { this.mode = mode; }
    public Game GetGame() { return game;}
    public ShipController GetShipControrller() { return shipController;}
    public BoatController GetBoatController() { return boatPartyController; }
}

public enum PlayerControlMode
{
    Ship,
    Boat,
    UI
}