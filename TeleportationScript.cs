using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportationScript : MonoBehaviour
{
    
    [SerializeField] private LayerMask layer;
    [SerializeField] private int TimeBeforeTp;
    float fillTime;
    public Transform PlayerCam;
    [SerializeField] private GameObject PlayerObj;
    bool isLooking = false;

    public Canvas SliderCanva;
    public Image slider;

    public Image FadeImage;

    private void Start()
    {
        fillTime = TimeBeforeTp;
    }

    void Update()
    {
        Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.TransformDirection(Vector3.forward), out RaycastHit hit, 70, layer);
        //Debug.DrawLine(PlayerCam.transform.position, hit.point, Color.red);
        if ( hit.collider != null && !isLooking)
        {
            isLooking = true;
            StartCoroutine(WaitForSectondsAndFade(TimeBeforeTp));
            StartCoroutine(WaitForSectondsAndTp(TimeBeforeTp + 2, hit.transform.GetChild(0)));

        }
        else if(hit.collider == null && isLooking)
        {
            isLooking = false;
            StopAllCoroutines();
            StartCoroutine(FadeBlackOutSquare(false, 3));
            slider.fillAmount = 0;
        }

        if (isLooking)
        {
            FillTheSlider();
        }
    }

    private IEnumerator WaitForSectondsAndFade(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        print("WaitAndPrint " + Time.time);
        StartCoroutine(FadeBlackOutSquare());
    }

    private IEnumerator WaitForSectondsAndTp(float waitTime, Transform pos)
    {
        yield return new WaitForSeconds(waitTime);

        PlayerObj.transform.position = pos.position;
        PlayerObj.transform.rotation = pos.rotation;
    }

    private void FillTheSlider()
    {
        slider.fillAmount += (float)1/ TimeBeforeTp * Time.deltaTime; 
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Color objectColor = FadeImage.color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (FadeImage.color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                FadeImage.color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (FadeImage.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                FadeImage.color = objectColor;
                yield return null;
            }
        }
    }
}
