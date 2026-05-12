using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] private bool isEnabled;
    [SerializeField] private BoatParty boatParty;
    [SerializeField] private BoatPartyManagement management;
    [SerializeField] private BoatPartyMovement movement;

    public BoatPartyManagement GetBoatPartyManagement()
    {
        return this.management;
    }

    public BoatParty GetBoatParty()
    {
        return this.boatParty;
    }

    public bool getIsEnabled()
    {
        return this.isEnabled;
    }

    public void setIsEnabled(bool value)
    {
        this.isEnabled = value;
    }
}
