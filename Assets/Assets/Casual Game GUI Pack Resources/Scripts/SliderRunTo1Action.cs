using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderRunTo1Action : MonoBehaviour
{

    public bool b = true;
    public Slider slider;
    public float speed = 0.5f;

    public Animator animm;

    float time = 0f;

    void Start()
    {

        slider = GetComponent<Slider>();
    }

    void Update()
    {
        if (b)
        {
            time += Time.deltaTime * speed;
            slider.value = time;

            if (time > 1)
            {
                b = false;
                time = 0;

                animm.gameObject.SetActive((true));
                animm.Play("Circle wave");
            }
        }
    }


}
