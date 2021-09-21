using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenLand : MonoBehaviour
{
    // Start is called before the first frame update
    bool IsAppear = false;
    SpriteRenderer Renderer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullete")
        {
            if (!IsAppear)
            {               
                StartCoroutine(TurnAppear());
            }
            else if (IsAppear)
            {
                StartCoroutine(TurnDisAppear());
            }
        }
    }

    IEnumerator TurnAppear()
    {
        while(Renderer.color.a < 255)
        {
            Renderer.color += new Color(0, 0, 0, 5);
            if (Renderer.color.a >= 255)
            {
                IsAppear = true;
                Renderer.color = new Color(Renderer.color.r,Renderer.color.g,Renderer.color.b,255f);
            }
            yield return new WaitForFixedUpdate();

        }
    }

    IEnumerator TurnDisAppear()
    {
        while (Renderer.color.a > 0f)
        {
            Renderer.color -= new Color(0, 0, 0, 5);
            if (Renderer.color.a <= 0)
            {
                IsAppear = false;
                Renderer.color = new Color(Renderer.color.r, Renderer.color.g, Renderer.color.b, 0f);
            }
            yield return new WaitForFixedUpdate();

        }
    }

    private void Start()
    {
        Renderer = gameObject.GetComponent<SpriteRenderer>();
    }

}
