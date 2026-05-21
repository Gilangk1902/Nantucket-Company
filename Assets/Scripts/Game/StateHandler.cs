using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    private void Update()
    {
        
    }

    public GameState GetGameState() { return gameState; }
    public void SetGameState(GameState gameState) { this.gameState = gameState; }
}

public enum GameState
{
    UI,
    Ship,
    Lower,
    Battle,
}
