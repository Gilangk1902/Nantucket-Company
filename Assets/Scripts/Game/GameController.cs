using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private StateHandler stateHandler;
    
    public StateHandler GetStateHandler() { return stateHandler; }
}
