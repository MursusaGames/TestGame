using System.Collections;

using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public int timeOffChangeColor = 3;
    private static PlayerSystem _instance;
    public static PlayerSystem Inctance => _instance;
    
    

    private void Awake()
    {
        _instance = this;
    }   
    
}
