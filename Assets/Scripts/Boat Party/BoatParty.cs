using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatParty : MonoBehaviour
{
    [SerializeField] private List<Boat> party;
    [SerializeField] private Boat leader;

    public Boat GetLeader() { return leader; }
}
