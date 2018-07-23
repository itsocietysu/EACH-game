using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedSampleButton : MonoBehaviour
{
    private string buttonName;
    public Text buttonText;
    public Font font;
    public RawImage iconImage;
    public Template_Scrollview scrollView;

    public void SetName(string name)
    {
        buttonName = name + "_button";
        buttonText.text = name;
    }
    public void SetIcon(byte[] decodedBytes)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(decodedBytes);
        iconImage.texture = tex;
    }
    public string GetButtonName()
    {
        return buttonText.text;
    }
   /* public void Button_Click()
    {
        scrollView.ButtonClicked(this);

    }*/
}
