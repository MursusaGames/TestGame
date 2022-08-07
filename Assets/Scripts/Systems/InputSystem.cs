using UnityEngine;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private float jumpLenght = 3f;
    [SerializeField] private float timeToNewJump = 3f;
    private bool playerInit;
    [SerializeField] private Player _player;
    private Vector3 _movementDirection = new Vector3();
    private static InputSystem _instance;
    public static InputSystem Inctance => _instance;
    [SerializeField] private InputField playerNameInput;
    private bool isJump;
    private float timer;
    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (playerInit)
        {
            GetMoveDirection();
            if(Input.GetMouseButtonDown(0)&& !isJump)
            {
                _player.Jump(jumpLenght);
                isJump = true;
            }
            if (isJump)
            {
                timer += Time.deltaTime;
                if (timer >= timeToNewJump)
                {
                    isJump = false;
                    timer = 0;
                }
                    
            }
        }
            
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
        _player.MovePlayer(_movementDirection);
    }
    
   
}
