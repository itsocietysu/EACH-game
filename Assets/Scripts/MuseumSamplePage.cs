using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumSamplePage : MonoBehaviour {
    private string museumPageName;
    public Text museumName;
    public Text museumDesc;
    public Font font;
    public RawImage image;
    public Template_Scrollview scrollView;

    public void SetData(MuseumSampleButton button) {
        museumPageName = button.GetButtonName() + "_page";
        museumName.text = button.GetButtonName();
        museumDesc.text = button.GetDescription().text;
        image.texture = button.iconImage.texture;
    }
  /*  public void SetName(string name) {
        museumPageName = name + "_page";
        museumName.text = name;
    }
    public void SetDescription(string description) {
        museumDesc.text = description;
    }
    public void SetIcon(RawImage image) {
        //Texture2D tex = new Texture2D(1, 1);
        //tex.LoadImage(decodedBytes);
        iconImage = image;
    }*/
   /* public void Button_Click() {
        scrollView.ButtonClicked(this);
*/
   // }

}
