using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BoatPartyMovement : MonoBehaviour
{
    [SerializeField] BoatController controller;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject destinationHighlight;
    
    private void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0) && controller.getIsEnabled())
        {
            Vector3 pos = GetMouseWorldPosition();
            Debug.Log(pos);
            Move(pos);
        }
        HideDestinationHighlight();
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.point;

        return Vector3.zero;
    }

    private void Move(Vector3 destination)
    {
        destinationHighlight.transform.position = destination;
        destinationHighlight.SetActive(true);

        controller.GetBoatPartyManagement().ManageFormation(destination);

        //partyManagement.getPartyLeader().Move(partyManagement.getFormationSpot(1));
        controller.GetBoatPartyManagement().getPartyLeader().Move(destination);
        Debug.Log("Moving party leader" + controller.GetBoatPartyManagement().getPartyLeader().getCharacterName() + "to" + destination.ToString());

        int index = 2;
        foreach (Boat _character in controller.GetBoatPartyManagement().getSelectedPartyMember())
        {
            if(_character != controller.GetBoatPartyManagement().getPartyLeader())
            {
                //_character.Move(partyManagement.getFormationSpot(index));
                _character.Move(GetRandomizedDestination(destination));
                Debug.Log("Moving" + _character.getId() + "to its destination");
                index++;
            }
        }
    }

    private Vector3 GetRandomizedDestination(Vector3 leaderDestination, float radius = 10f)
    {
        // Pick random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // Offset on XZ plane
        float offsetX = Mathf.Cos(angle) * radius;
        float offsetZ = Mathf.Sin(angle) * radius;

        return leaderDestination + new Vector3(offsetX, 0f, offsetZ);
    }


    private void HideDestinationHighlight()
    {
        if(controller.GetBoatPartyManagement().getPartyLeader().HasReachedDestination())
        {
            destinationHighlight.SetActive(false);
        }
    }

}
