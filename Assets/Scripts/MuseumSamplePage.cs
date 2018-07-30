using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumSamplePage : MonoBehaviour {
    private string museumPageName;
    public Text museumName;
    public Text museumDesc;
    public Font font;
    public Image image;
    public Template_Scrollview scrollView;

    public void SetData(MuseumSampleButton button) {
        museumPageName = button.GetButtonName() + "_page";
        museumName.text = button.GetButtonName();
        museumDesc.text = button.GetDescription().text;
        image.sprite = button.iconImage.sprite;
    }
}
