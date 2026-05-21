using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private PlayerController playerController;


    public GameController GetGameController() { return gameController; }
    public PlayerController GetPlayerController() {  return playerController; }
    public GameState GetGameState(){ return GetGameController().GetStateHandler().GetGameState(); }
}
