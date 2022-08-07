using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float distanceBackToPlayer = 5;
    [SerializeField] private float distanceUpToPlayer = 5;
    private static CameraSystem _instance;
    private Transform target;
    
    public static CameraSystem Inctance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void SetTarget(GameObject player)
    {
        target = player.transform;
        cam.gameObject.transform.position = target.position + Vector3.back * distanceBackToPlayer+Vector3.up*distanceUpToPlayer;
        cam.gameObject.transform.SetParent(target);
        cam.transform.LookAt(target);
    }


    
}
