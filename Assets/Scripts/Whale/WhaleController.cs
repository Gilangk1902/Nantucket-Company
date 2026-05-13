using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleController : MonoBehaviour
{
    [SerializeField] private Whale whale;

    public Whale GetWhale()
    {
        return this.whale;
    }
}
