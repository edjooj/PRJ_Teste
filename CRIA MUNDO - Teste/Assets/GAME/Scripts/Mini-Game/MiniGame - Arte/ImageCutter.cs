using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImageCutter : MonoBehaviour
{
    public GameObject mask;
    public Transform maskParent;

    public Camera cam;
    private Vector3 lastMousePosition;
    private List<GameObject> instantiatedMasks;

    [Header("Grid")]
    private int gridRows = 10;
    private int gridColumns = 10;
    private bool[,] grid;
    public SpriteRenderer backgroundImage;
    public TMP_Text porcentagemTxt;
    public TMP_Text tempoTxt;

    private float totalRevealedArea;

    [Header("Hud")]
    public Cronometro cronometro;
    public GameObject winPanel;
    private bool acabouOJogo = false;

    private void OnEnable()
    {
        acabouOJogo = false;
        if (maskParent == null)
        {
            maskParent = GameObject.Find("ImageCutter").transform;
        }

        lastMousePosition = Vector3.zero;
        instantiatedMasks = new List<GameObject>();

        grid = new bool[gridRows, gridColumns];
        totalRevealedArea = 0;  


    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            if (mousePos != lastMousePosition)
            {
                Vector3 pos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
                pos.z = 0;
                GameObject newMask = Instantiate(mask, pos, Quaternion.identity, maskParent);
                instantiatedMasks.Add(newMask);

                Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

                worldPosition.z = backgroundImage.transform.position.z;
                AddMaskAtPosition(worldPosition);
            }

            lastMousePosition = mousePos;
        }

        if(totalRevealedArea >= 100 && !acabouOJogo)
        {
            acabouOJogo = true;
            cronometro.cronometroAtivo = false;
            winPanel.SetActive(true);
            tempoTxt.text = cronometro.minutos.ToString("00") + ":" + cronometro.segundos.ToString("00");
        }
    }

    private void AddMaskAtPosition(Vector3 position)
    {
        int gridX = Mathf.FloorToInt(position.x / backgroundImage.sprite.bounds.size.x * gridColumns);
        int gridY = Mathf.FloorToInt(position.y / backgroundImage.sprite.bounds.size.y * gridRows);

        if (gridX >= 0 && gridX < gridColumns && gridY >= 0 && gridY < gridRows && !grid[gridX, gridY])
        {
            GameObject newMask = Instantiate(mask, position, Quaternion.identity, maskParent);
            instantiatedMasks.Add(newMask);
            grid[gridX, gridY] = true;

            totalRevealedArea += CalculateMaskArea(newMask);
            UpdateRevealedPercentage();
        }
    }


    private float CalculateMaskArea(GameObject maskInstance)
    {
        SpriteMask maskComponent = maskInstance.GetComponent<SpriteMask>();
        if (maskComponent != null && maskComponent.sprite != null)
        {
            
            Vector2 size = maskComponent.sprite.bounds.size;
            size.x *= maskInstance.transform.localScale.x;
            size.y *= maskInstance.transform.localScale.y;
            return size.x * size.y; 
        }
        else
        {
            return 0f;
        }
    }


    private void UpdateRevealedPercentage()
    {
        float totalArea = gridRows * gridColumns;
        float revealedPercentage = (totalRevealedArea / totalArea) * 100;
        porcentagemTxt.text = revealedPercentage.ToString();
    }

    public void ClearMasks()
    {
        foreach (GameObject mask in instantiatedMasks)
        {
            Destroy(mask);
        } 
        instantiatedMasks.Clear();
    }
}
