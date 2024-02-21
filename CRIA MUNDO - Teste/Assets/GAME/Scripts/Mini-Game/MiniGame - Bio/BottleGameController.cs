using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGameController : MonoBehaviour
{
    public BottleController firstBottle;
    public BottleController secondBottle;

    public Camera bottleCamera;

    private void OnEnable()
    {
        bottleCamera.orthographic = true;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = bottleCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                if(hit.collider.GetComponent<BottleController>() != null)
                {
                    if(firstBottle == null)
                    {
                        firstBottle = hit.collider.GetComponent< BottleController>();
                    }
                    else
                    {
                        if (firstBottle == hit.collider.GetComponent<BottleController>())
                        {
                            firstBottle = null;
                        }
                        else
                        {
                            secondBottle=hit.collider.GetComponent<BottleController>();
                            firstBottle.bottleControllerRef = secondBottle;

                            firstBottle.UpdateTopColorValue();
                            secondBottle.UpdateTopColorValue();

                            if(secondBottle.FillBottleCheck(firstBottle.topColor) == true)
                            {
                                firstBottle.StartColorTransfer();
                                firstBottle = null;
                                secondBottle = null;
                            }
                            else
                            {
                                firstBottle = null;
                                secondBottle = null;
                            }
                        }
                    }
                }
            }
        }
    }
}
