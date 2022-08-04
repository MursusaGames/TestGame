using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private bool playerInit;
    [SerializeField] private Player _player;
    private Vector3 _movementDirection = new Vector3();
    private static InputSystem _instance;
    public static InputSystem Inctance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if(playerInit)  GetMoveDirection();
    }

    public void SetPlayer(Player player)
    {
        _player = player;
        playerInit = true;
    }

    private void GetMoveDirection()
    {
        _movementDirection.x = Input.GetAxis("Horizontal");
        _movementDirection.z = Input.GetAxis("Vertical");
        _movementDirection.y = 0;
        _player.CmdMovePlayer(_movementDirection);
        _player.MovePlayer(_movementDirection);
    }

}
