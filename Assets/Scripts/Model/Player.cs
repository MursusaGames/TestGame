using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private float speed;
    [SerializeField] private float _rotateSpeed = 2;
    [SerializeField] private float _turnSpeed = 2;
    private Rigidbody rg;
    private Animator anim;
    private float board = 4.7f;

    private void Awake()
    {
        rg = gameObject.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (isClient&&isLocalPlayer) RegistrationInInputSystem();
        if (isServer) speed = 300f;
    }

    private void RegistrationInInputSystem()
    {
        InputSystem.Inctance.SetPlayer(this);
    }

    [Command]
    public void CmdMovePlayer(Vector3 direction)
    {
        Vector3 offset = direction.normalized * speed * Time.deltaTime;
        rg.velocity = offset;
        RotateToDirections(direction);
        if (direction.normalized*speed == Vector3.zero)
        {
            anim.SetBool("run", false);
            anim.SetBool("idle", true);
        }
        else
        {
            anim.SetBool("idle", false);
            anim.SetBool("run", true);
        }
        var playerPos = transform.position;
        if (playerPos.x > board) playerPos.x = board;
        if (playerPos.x < -board) playerPos.x = -board;
        if (playerPos.z > board) playerPos.z = board;
        if (playerPos.z < -board) playerPos.z = -board;
        transform.position = playerPos;
    }

    public void MovePlayer(Vector3 direction)
    {
        Vector3 offset = direction.normalized * speed * Time.deltaTime;
        rg.velocity = offset;
        RotateToDirections(direction);
        if (direction.normalized * speed == Vector3.zero)
        {
            anim.SetBool("run", false);
            anim.SetBool("idle", true);
        }
        else
        {
            anim.SetBool("idle", false);
            anim.SetBool("run", true);
        }
        var playerPos = transform.position;
        if (playerPos.x > board) playerPos.x = board;
        if (playerPos.x < -board) playerPos.x = -board;
        if (playerPos.z > board) playerPos.z = board;
        if (playerPos.z < -board) playerPos.z = -board;
        transform.position = playerPos;
    }
    private void RotateToDirections(Vector3 direction)
    {
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, _rotateSpeed, 0);
        Quaternion rotation = Quaternion.LookRotation(newDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
    }
}
