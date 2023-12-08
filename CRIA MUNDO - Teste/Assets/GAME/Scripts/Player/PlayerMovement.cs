using Photon.Pun;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public CharacterController controller;
    public float speed = 5.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    private Vector3 velocity;
    private bool isGrounded;

    public TMPro.TextMeshPro nickname;

    private void Awake()
    {
        if(!photonView.IsMine) { return; }
        DontDestroyOnLoad(this);

        nickname.text = CORE.instance.connection.userName;
    }

    void Update()
    {
        if (!photonView.IsMine) { return; }
        // Checa se o personagem está no chão
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * speed * Time.deltaTime);

        // Tratamento do salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplica a gravidade no personagem
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
