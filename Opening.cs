using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public GameObject openingStory;
    public GameObject startBtn;
    public GameObject music;

    public Text OpeningRemarks;//開場白
    public Text OpeningRemarks2;
    private string Sentence;

    private void Start()
    {
        StartCoroutine(TypingSentence());
        Invoke("Set", 26f);

    }

    public void Set()
    {
        openingStory.SetActive(false);
        startBtn.SetActive(true);
    }

    public void setMus()
    {
        music.SetActive(true);
    }

    IEnumerator TypingSentence()
    {
        Sentence = OpeningRemarks.text;
        OpeningRemarks.text = "";
        foreach(char letter in Sentence.ToCharArray())
        {
            OpeningRemarks.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        
        Sentence = OpeningRemarks2.text;
        OpeningRemarks.text = "";
        foreach (char letter in Sentence.ToCharArray())
        {
            OpeningRemarks.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
