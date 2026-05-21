using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private PlayerController playerController;
    
    [Header("Boat Mode Interaction")]
    //[SerializeField] private ShipInteractionUI shipInteractionUI_boat;
    [SerializeField] private WhaleInteractionUI_Boat whaleInteractionUI_boat;
    
    [Header("Ship Mode Interaction")]
    [SerializeField] private ShipInteractionUI_Ship shipInteractionUI_ship;
    //[SerializeField] private WhaleInteractionUI whaleInteractionUI_ship;
    public void ShowWhaleInteractionPanel(PlayerControlMode mode, Vector3 mousePosition)
    {
        if(mode == PlayerControlMode.Boat)
        {
            playerController.GetGame().GetGameController().GetStateHandler().SetGameState(GameState.UI);
            playerController.SetPlayerControlMode(PlayerControlMode.UI);

            whaleInteractionUI_boat.GetWhaleInteractionPanel().SetActive(true);
            Vector3 offset = new Vector3(50f, -50f, 0);
            whaleInteractionUI_boat.GetWhaleInteractionPanel().transform.position = mousePosition + offset;

            shipInteractionUI_ship.GetShipInteractionPanel().SetActive(false);
        }
        else if(mode == PlayerControlMode.Ship)
        {

        }
    }

    public void ShowShipInteractionPanel(PlayerControlMode mode, Vector3 mousePosition)
    {
        if(mode == PlayerControlMode.Ship) { 
            playerController.GetGame().GetGameController().GetStateHandler().SetGameState(GameState.UI);
            playerController.SetPlayerControlMode(PlayerControlMode.UI);
            
            shipInteractionUI_ship.GetShipInteractionPanel().SetActive(true);
            Vector3 offset = new Vector3(50f, -50f, 0);
            shipInteractionUI_ship.GetShipInteractionPanel().transform.position = mousePosition + offset;

            whaleInteractionUI_boat.GetWhaleInteractionPanel().SetActive(false);
        }
        else if(mode == PlayerControlMode.Boat)
        {

        }
    }

    public void CloseInteractionPanel(GameState state, PlayerControlMode mode)
    {
        playerController.GetGame().GetGameController().GetStateHandler().SetGameState(state);
        playerController.SetPlayerControlMode(mode);

        whaleInteractionUI_boat.GetWhaleInteractionPanel().SetActive(false);
        shipInteractionUI_ship.GetShipInteractionPanel().SetActive(false);
    }
}
