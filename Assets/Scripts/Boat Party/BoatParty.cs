using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatParty : MonoBehaviour
{
    [SerializeField] private BoatController controller;
    [SerializeField] private List<Boat> party;
    [SerializeField] private Boat leader;
    
    private void Update()
    {
        
    }

    public void SetBoatVisibility(bool value)
    {
        for (int i = 0; i < party.Count; i++)
        {
            if (party[i] != null) party[i].GetGameObject().SetActive(value);
            if (party[i] != null) party[i].GetGameObject().transform.localPosition = GetBoatDocks(i);
        }
    }

    public Vector3 GetBoatDocks(int index)
    {
        return controller.GetPlayerController().GetShipControrller().GetShip().GetBoatDockLocation(index);
        //return new Vector3(-10, 4.45f, -14);
    }

    public Boat GetLeader() { return leader; }
    public BoatController GetController() { return controller; }
    public List<Boat> GetBoats() { return party; }
}
