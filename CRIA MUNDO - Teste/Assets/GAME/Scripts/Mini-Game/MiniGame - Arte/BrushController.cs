using UnityEngine;

public class BrushController : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
        mousePos.z = 0;
        transform.position = mousePos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Borracha"))
        {
            Debug.Log("PERDEUUUUU");
        }
    }
}
