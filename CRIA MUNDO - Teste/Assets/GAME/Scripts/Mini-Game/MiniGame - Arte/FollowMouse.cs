using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Transform pincel;
    public float speed = 5f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pincel.position, speed * Time.deltaTime);
    }
}
