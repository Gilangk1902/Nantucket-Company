using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPartyManagement : MonoBehaviour
{
    //[SerializeField] BoatParty party;
    [SerializeField] private BoatController controller;
    [SerializeField] private List<Boat> selectedPartyBoat = new List<Boat>();

    private void ManageSelectedPartyMember(Boat character)
    {
        if(selectedPartyBoat.Count <= 0)
        {
            selectedPartyBoat.Add(character);
            return;
        }
        
        foreach(Boat _character in selectedPartyBoat)
        {
            if(_character.getId() == character.getId())
            {
                selectedPartyBoat.Remove(_character);
            }
        }

        selectedPartyBoat.Add(character);
    }

    [SerializeField] private float rowSpacing = 2f;
    [SerializeField] private float colSpacing = 2f;
    [SerializeField] private Camera cam;

    private readonly Vector3[] slots = new Vector3[4];

    public void ManageFormation(Vector3 pivot)
    {
        // Vector3 can’t be null, so no null check here
        Vector3 f = GetFlatForward();
        Vector3 r = Vector3.Cross(Vector3.up, f);

        // 1 (front), 2 (left), 3 (right), 4 (back)
        slots[0] = pivot + f * rowSpacing;      // spot 1
        slots[1] = pivot - r * colSpacing;      // spot 2
        slots[2] = pivot + r * colSpacing;      // spot 3
        slots[3] = pivot - f * rowSpacing;      // spot 4
    }

    public Vector3 getFormationSpot(int number)
    {
        number = Mathf.Clamp(number, 1, 4);
        return slots[number - 1];
    }



    private Vector3 GetFlatForward()
    {
        if (cam == null) cam = Camera.main;
        Vector3 f = cam != null ? cam.transform.forward : Vector3.forward;
        f.y = 0f; // keep on XZ plane
        return f.normalized;
    }

    public List<Boat> getSelectedPartyMember()
    {
        return this.selectedPartyBoat;
    }

    public Boat getPartyLeader()
    {
        return controller.GetBoatParty().GetLeader();
    }
}
