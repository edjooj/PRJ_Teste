using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    public Color[] bottleColors; // Cores que vão iniciar na garrafa
    public SpriteRenderer bottleMaskSR; // Mascara da garrafa que será aplicada as cores

    public AnimationCurve scaleAndRotationMultiplierCurve;
    public AnimationCurve fillAmountCurve;

    public AnimationCurve RotationSpeedMultiplier;


    public float[] fillAmounts;
    public float[] rotationvalues;

    public int rotationIndex = 0;

    [Range(0,4)]
    public int numberOfColorsInBottle = 4;

    public Color topColor;
    public int numberOfTopColorLayer = 1;

    public BottleController bottleControllerRef;
    public int numberOfColorsToTransfer = 0;

    [Header("Video Final 04")]
    public Transform leftRotationPoint;
    public Transform rightRotationPoint;
    private Transform chosenRotationPoint;

    private float directionMultiplier = 1;

    Vector3 originalPosition;
    Vector3 startPosition;
    Vector3 endPosition;

    public LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        bottleMaskSR.material.SetFloat("_FillAmount", fillAmounts[numberOfColorsInBottle]); //Ao iniciar o jogo vai fazer o FillAmount do material seja igual ao numero de cor

        originalPosition = transform.position;

        UpdateColorOnShader(); //Define as cores conforme as cores do bottleColors

        UpdateTopColorValue();
    }

    public void StartColorTransfer()
    {
        ChooseRotationPointAndDirection();

        numberOfColorsToTransfer = Mathf.Min(numberOfTopColorLayer, 4 - bottleControllerRef.numberOfColorsInBottle);
        for (int i = 0; i < numberOfColorsToTransfer; i++)
        {
            bottleControllerRef.bottleColors[bottleControllerRef.numberOfColorsInBottle + i] = topColor;
        }
        bottleControllerRef.UpdateColorOnShader();

        CalculateRotationIndex(4 - bottleControllerRef.numberOfColorsInBottle);

        transform.GetComponent<SpriteRenderer>().sortingOrder += 2;
        bottleMaskSR.sortingOrder += 2;


        StartCoroutine(MoveBottle());
    }

    IEnumerator MoveBottle()
    {
        startPosition = transform.position;
        if(chosenRotationPoint == leftRotationPoint)
        {
            endPosition = bottleControllerRef.rightRotationPoint.position;
        }
        else
        {
            endPosition = bottleControllerRef.leftRotationPoint.position;
        }

        float t = 0;

        while(t <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        StartCoroutine(RotateBottle());
    }

    IEnumerator MoveBottleBack()
    {
        startPosition = transform.position;
        endPosition = originalPosition;

        float t = 0;

        while (t <= 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            t += Time.deltaTime * 2;

            yield return new WaitForEndOfFrame();
        }

        transform.position = endPosition;

        transform.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        bottleMaskSR.sortingOrder -= 2;
    }




    void UpdateColorOnShader()
    {
        bottleMaskSR.material.SetColor("_C1", bottleColors[0]);
        bottleMaskSR.material.SetColor("_C2", bottleColors[1]);
        bottleMaskSR.material.SetColor("_C3", bottleColors[2]);
        bottleMaskSR.material.SetColor("_C4", bottleColors[3]);

    }

    public float timeToRotate = 1.0f; //Tempo de rotação da garrafa

    IEnumerator RotateBottle()
    {
        Debug.Log("Indo até a garrafa");
        float t = 0;
        float lerValue = 0;
        float angleValue = 0 ;

        float lastAngleValue = 0;

        while (t < timeToRotate)
        {

            lerValue = t / timeToRotate;
            angleValue = Mathf.Lerp(0, directionMultiplier * rotationvalues[rotationIndex], lerValue);


            transform.RotateAround(chosenRotationPoint.position, new Vector3(0,0,1).normalized, lastAngleValue - angleValue);


            bottleMaskSR.material.SetFloat("_ScaleAndRotation", scaleAndRotationMultiplierCurve.Evaluate(angleValue));

            if (fillAmounts[numberOfColorsInBottle] > fillAmountCurve.Evaluate(angleValue) + 0.005f)
            {
                Debug.Log("Passando o lioquido");
                bottleMaskSR.material.SetFloat("_FillAmount", fillAmountCurve.Evaluate(angleValue));

                bottleControllerRef.FillUp(fillAmountCurve.Evaluate(lastAngleValue) - fillAmountCurve.Evaluate(angleValue));
            }



            t += Time.deltaTime * RotationSpeedMultiplier.Evaluate(angleValue);
            lastAngleValue = angleValue;

            yield return new WaitForEndOfFrame();
        }
        angleValue = directionMultiplier * rotationvalues[rotationIndex];
        bottleMaskSR.material.SetFloat("_ScaleAndRotation", scaleAndRotationMultiplierCurve.Evaluate(angleValue));
        bottleMaskSR.material.SetFloat("_FillAmount", fillAmountCurve.Evaluate(angleValue));

        numberOfColorsInBottle -= numberOfColorsToTransfer;
        bottleControllerRef.numberOfColorsInBottle += numberOfColorsToTransfer;

        CalculateRotationIndex(4 - bottleControllerRef.numberOfColorsInBottle);

        lineRenderer.enabled = false;
        StartCoroutine(RotateBottleBack());

    }

    IEnumerator RotateBottleBack()
    {
        Debug.Log("Voltando da garrafa");
        float t = 0;
        float lerValue;
        float angleValue;

        float lastAngleValue = directionMultiplier * rotationvalues[rotationIndex];

        while (t < timeToRotate)
        {
            lerValue = t / timeToRotate;
            angleValue = Mathf.Lerp(directionMultiplier * rotationvalues[rotationIndex], 0, lerValue);


            transform.RotateAround(chosenRotationPoint.position, new Vector3(0, 0, 1).normalized, lastAngleValue - angleValue);

            bottleMaskSR.material.SetFloat("_ScaleAndRotation", scaleAndRotationMultiplierCurve.Evaluate(angleValue));

            lastAngleValue = angleValue;


            t += Time.deltaTime * RotationSpeedMultiplier.Evaluate(angleValue);

            yield return new WaitForEndOfFrame();
        }
        UpdateTopColorValue();

        angleValue = 0;
        transform.eulerAngles = new Vector3(0, 0, angleValue);
        bottleMaskSR.material.SetFloat("_ScaleAndRotation", scaleAndRotationMultiplierCurve.Evaluate(angleValue));

        StartCoroutine(MoveBottleBack());

    }

    bool ColorsAreEqual(Color color1, Color color2, float tolerance = 0.01f)
    {
        return Mathf.Abs(color1.r - color2.r) < tolerance &&
               Mathf.Abs(color1.g - color2.g) < tolerance &&
               Mathf.Abs(color1.b - color2.b) < tolerance &&
               Mathf.Abs(color1.a - color2.a) < tolerance;
    }


    public void UpdateTopColorValue()
    {
        if(numberOfColorsInBottle != 0)
        {
            numberOfTopColorLayer = 1;

            topColor = bottleColors[numberOfColorsInBottle - 1];

            if (numberOfColorsInBottle == 4)
            {
                if (ColorsAreEqual(bottleColors[3], bottleColors[2]))
                {
                    numberOfTopColorLayer = 2;
                    if (ColorsAreEqual(bottleColors[2], bottleColors[1]))
                    {
                        numberOfTopColorLayer = 3;
                        if (ColorsAreEqual(bottleColors[1], bottleColors[0]))
                        {
                            numberOfTopColorLayer = 4;
                        }
                    }
                }
            }
            else if (numberOfColorsInBottle == 3)
            {
                if (ColorsAreEqual(bottleColors[2], bottleColors[1]))
                {
                    numberOfTopColorLayer = 2;
                    if (ColorsAreEqual(bottleColors[1], bottleColors[0]))
                    {
                        numberOfTopColorLayer = 3;
                    }
                }
            }
            else if (numberOfColorsInBottle == 2)
            {
                if (ColorsAreEqual(bottleColors[1], bottleColors[0]))
                {
                    numberOfTopColorLayer = 2;
                }
            }

            rotationIndex = 3 - (numberOfColorsInBottle - numberOfTopColorLayer);
        }
    }

    public bool FillBottleCheck(Color colorToCheck)
    {
        if (numberOfColorsInBottle == 0)
        {
            return true;
        }
        else if (numberOfColorsInBottle == 4)
        {
            return false;
        }
        else
        {
            return ColorsAreEqual(topColor, colorToCheck);
        }
    }


    private void CalculateRotationIndex(int numberOfEmptySpacesInSecondBottle)
    {
        rotationIndex=3 - (numberOfColorsInBottle - Mathf.Min(numberOfEmptySpacesInSecondBottle, numberOfTopColorLayer));
    }

    private void FillUp(float fillAmountToAdd)
    {
        bottleMaskSR.material.SetFloat("_FillAmount", bottleMaskSR.material.GetFloat("_FillAmount") + fillAmountToAdd);
    }

    private void ChooseRotationPointAndDirection()
    {
        if(transform.position.x > bottleControllerRef.transform.position.x)
        {
            chosenRotationPoint = leftRotationPoint;
            directionMultiplier = -1;
        }
        else
        {
            chosenRotationPoint = rightRotationPoint;
            directionMultiplier = 1;
        }

    }



}
