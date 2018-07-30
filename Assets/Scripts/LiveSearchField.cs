using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LiveSearchField : MonoBehaviour {
    public GameObject buttonPanel;
    public string pageType;
    private InputField input;

    void Start() {
        input = gameObject.GetComponent<InputField>();
    }

    public void ValueChange(string val) {
          switch (pageType) {
            case "museum": {
                foreach (GameObject button in Template_Scrollview.musButtonList) {
                    MuseumSampleButton musButton = button.GetComponent<MuseumSampleButton>();
                    if (!musButton.GetButtonName().ToLower().Contains(input.text.ToLower())) {
                        button.SetActive(false);
                    }
                    else {
                        button.SetActive(true);
                    }
                }
                break;
                }
            case "feed": {
                foreach (GameObject button in Template_Scrollview.feedButtonList) {
                    FeedSampleButton feedButton = button.GetComponent<FeedSampleButton>();
                    if (!feedButton.GetButtonName().ToLower().Contains(input.text.ToLower())) {
                        button.SetActive(false);
                    }
                    else {
                        button.SetActive(true);
                    }
                }
                break;
                }
        }
    }
}
