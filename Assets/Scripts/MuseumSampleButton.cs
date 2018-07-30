using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumSampleButton : MonoBehaviour {
    private string buttonName;
    //private string description;
    public Text buttonText;
    public Font font;
    public Image iconImage;
    public Template_Scrollview scrollView;

    public Text _description;

    public void SetName(string name) {
        buttonName = name + "_button";
        buttonText.text = name;
    }
    public void SetDesc(string description) {
        _description.text = description;
    }
   /*
    public void SetIcon(byte[] decodedBytes)
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(decodedBytes);
        iconImage.texture = tex;
    }
    */
    public IEnumerator SetIcon(string decodedBytes) {
        WWW www = new WWW(decodedBytes);
        yield return www;

        Texture2D texture = www.texture;
        iconImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
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
