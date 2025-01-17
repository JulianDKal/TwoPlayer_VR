using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int puzzleOneScore = 0;

    private void Start()
    {
        if(instance == null) instance = this;
        else Destroy(this.gameObject);
    }
}
