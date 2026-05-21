using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MotherShip : MonoBehaviour
{
    [SerializeField] bool isEnabled;
    [SerializeField] private PlayerController controller;
    [SerializeField] private Ship ship;
    
    private void Update()
    {
        if (controller.GetPlayerControlMode() == PlayerControlMode.Ship) { setIsEnabled(true); }
        else { setIsEnabled(false); }
    }

    public Ship GetShip()
    {
        return this.ship;
    }

    public bool getIsEnabled()
    {
        return this.isEnabled;
    }

    public void setIsEnabled(bool isEnabled)
    {
        this.isEnabled = isEnabled;
    }
}
