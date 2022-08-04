using UnityEngine;

public class UiSystem : MonoBehaviour
{
    private static UiSystem _instance;
    public static UiSystem Inctance => _instance;

    private void Awake()
    {
        _instance = this;
    }
}
