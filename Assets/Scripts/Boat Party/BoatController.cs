using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [Header("Required Attributes")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private bool isEnabled;
    
    [SerializeField] private BoatParty boatParty;
    [SerializeField] private BoatPartyManagement management;
    [SerializeField] private BoatPartyMovement movement;

    private void Update()
    {
        if(controller.GetPlayerControlMode() == PlayerControlMode.Boat) { 
            setIsEnabled(true);
        }
        else { 
            setIsEnabled(false);
        }
    }

    public bool BoatNearShip()
    {
        int count = 0;
        foreach(Boat boat in boatParty.GetBoats())
        {
            if(boat != null)
            {
                Vector2 currentPos2D = new Vector2(boat.transform.localPosition.x, boat.transform.localPosition.z);
                Vector2 zero2D = Vector2.zero;

                float distance = Vector2.Distance(currentPos2D, zero2D);

                if(distance <= 18f) { count++; }
            }
        }
        if (count >= 1) return true;
        else return false;
    }

    public BoatPartyManagement GetBoatPartyManagement(){ return this.management; }
    public BoatParty GetBoatParty() { return this.boatParty; }
    public PlayerController GetPlayerController() { return this.controller; }
    public bool getIsEnabled() { return this.isEnabled; }
    public void setIsEnabled(bool value) { this.isEnabled = value; }

}
