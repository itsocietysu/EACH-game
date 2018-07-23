using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumSampleButton : MonoBehaviour {
    private string buttonName;
    //private string description;
    public Text buttonText;
    public Font font;
    public RawImage iconImage;
    public Template_Scrollview scrollView;

    public Text _description;

    public void SetName(string name) {
        buttonName = name + "_button";
        buttonText.text = name;
    }
    public void SetDesc(string description) {
        _description.text = description;
    }
    public void SetIcon(byte[] decodedBytes)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(decodedBytes);
        iconImage.texture = tex;
    }
    public void Button_Click() {
        scrollView.ButtonClicked(this);

    }
    public string GetButtonName() {
        return buttonText.text;
    }
    public Text GetDescription() {
        return _description;
    }
}
