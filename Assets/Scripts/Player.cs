using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Mode mode;
    [SerializeField] private BoatController boatPartyController;
    [SerializeField] private MotherShip shipController;

    private void Update()
    {
        if(mode  == Mode.Ship)
        {
            boatPartyController.setIsEnabled(false);
            shipController.setIsEnabled(true);
        }else if(mode  == Mode.Boat)
        {
            boatPartyController.setIsEnabled(true);
            shipController.setIsEnabled(false);
        }
    }
}

public enum Mode
{
    Ship,
    Boat
}