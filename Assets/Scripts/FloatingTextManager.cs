using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private FloatingText GetFloatingText()
    {
        FloatingText floatingTxt = floatingTexts.Find(t => !t.active);
        if (floatingTxt == null) 
        {
            floatingTxt = new FloatingText();
            floatingTxt.go = Instantiate(textPrefab);
            floatingTxt.go.transform.SetParent(textContainer.transform);
            floatingTxt.txt = floatingTxt.go.GetComponent<Text>();
            floatingTexts.Add(floatingTxt);
        }

        return floatingTxt;
    }

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();
        floatingText.txt.text = msg;
        floatingText.txt.color = color;
        floatingText.txt.fontSize = fontSize;
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);

        floatingText.motion = motion; 
        floatingText.duration = duration;

        floatingText.Show();
    }
}
