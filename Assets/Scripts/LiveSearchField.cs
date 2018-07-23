using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LiveSearchField : MonoBehaviour
{
    public GameObject buttonPanel;
    public string pageType;
    private InputField input;

    void Start() {
        input = gameObject.GetComponent<InputField>();
      //  var se = new InputField.SubmitEvent();
    //    se.AddListener(ValueChanged(input.text));
      //  input.onEndEdit = se;

        //or simply use the line below, 
        //input.onEndEdit.AddListener(SubmitName);  // This also works
    }
    public void ValueChange(string val) {
          switch (pageType) {
            case "museum": {
                foreach (GameObject button in Template_Scrollview.musButtonList) {
                    MuseumSampleButton musButton = button.GetComponent<MuseumSampleButton>();
                    if (!musButton.GetButtonName().ToLower().Contains(input.text.ToLower())) {
                        //Debug.Log("CONTAINS");
                        button.SetActive(false);
                    }
                    else {
                        // Debug.Log("DOES_NOT_CONTAIN");
                        button.SetActive(true);
                    }
                }
                break;
                }
            case "feed": {
                foreach (GameObject button in Template_Scrollview.feedButtonList) {
                    FeedSampleButton feedButton = button.GetComponent<FeedSampleButton>();
                    if (!feedButton.GetButtonName().ToLower().Contains(input.text.ToLower())) {
                        //Debug.Log("CONTAINS");
                        button.SetActive(false);
                    }
                    else {
                        // Debug.Log("DOES_NOT_CONTAIN");
                        button.SetActive(true);
                    }
                }
                break;
                }
        }
    }
    private void SubmitName(string arg0) {
        Debug.Log(arg0);
    }
}
