using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 300f;
    [SerializeField] private float board = 4.7f;
    [SerializeField] private float _rotateSpeed = 2;
    [SerializeField] private float _turnSpeed = 2;
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    private SkinnedMeshRenderer mesh;
    private Rigidbody rg;
    private Animator anim;
    private PhotonView photonView;
    private bool isJump;
    public int score;
    private void Awake()
    {
        rg = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }
    void Start()
    {
        InputSystem.Inctance.SetPlayer(this);
        CameraSystem.Inctance.SetTarget(gameObject);
    }

    public void Jump(float lenght)
    {
        isJump = true;
        var currentPos = gameObject.transform.position;
        var newPos = currentPos + Camera.main.transform.forward * lenght;
        rg.MovePosition(newPos);
    }
    public void MovePlayer(Vector3 direction)
    {
        if (!photonView.IsMine) return;
        Vector3 offset = direction.normalized * speed * Time.deltaTime;
        
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
        rg.velocity = offset;
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
    public void ChangeColor()
    {
        mesh.materials[0] = red;
        ChangeTag();
        Invoke(nameof(ResetColorAndTag), PlayerSystem.Inctance.timeOffChangeColor);
    }

    private void ChangeTag()
    {
        gameObject.tag = "untagged";
    }
    private void ResetColorAndTag()
    {
        mesh.materials[0] = green;
        gameObject.tag = "Player";
    }
         
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isJump)
        {
            other.GetComponent<Player>().ChangeColor();
            score++;
        }
    }
}
