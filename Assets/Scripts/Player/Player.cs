using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Game game;
    [SerializeField] private PlayerControlMode mode;
    [SerializeField] private BoatController boatPartyController;
    [SerializeField] private MotherShip shipController;

    private GameState GetGameState(){ return game.GetGameController().GetStateHandler().GetGameState(); }
    private bool BoatNearShip() { return boatPartyController.BoatNearShip(); }
    private void Update()
    {
        Debug.Log(BoatNearShip());
        if (Input.GetKeyUp(KeyCode.Alpha1) && GetGameState() != GameState.Ship && BoatNearShip())
        {
            game.GetGameController().GetStateHandler().SetGameState(GameState.Ship);
            SetPlayerControlMode(PlayerControlMode.Ship);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2) && GetGameState() != GameState.Lower)
        {
            game.GetGameController().GetStateHandler().SetGameState(GameState.Lower);
            SetPlayerControlMode(PlayerControlMode.Boat);
        }
    }

    public PlayerControlMode GetPlayerControlMode(){return mode;}
    public void SetPlayerControlMode(PlayerControlMode mode) { this.mode = mode; }
    public Game GetGame() { return game;}
    public MotherShip GetShipControrller() { return shipController;}
}

public enum PlayerControlMode
{
    Ship,
    Boat
}