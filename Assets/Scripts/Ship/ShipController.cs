using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MotherShip : MonoBehaviour
{
    [SerializeField] bool isEnabled;
    [SerializeField] private Ship ship;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            MoveShip();
        }
    }
    private void MoveShip()
    {

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
