using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleJSON;

public class Template_Scrollview : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    #region Variables
    string path;
    string jsonString;
    public GameObject buttonTemplate;
    public GameObject musPageTemplate;
    public GameObject sideMenu;
    public GameObject loader;
    public Transform contentPanel;
    public string groupName;

    public static List<GameObject> musButtonList = new List<GameObject>();
    public static/*private*/ List<GameObject> feedButtonList = new List<GameObject>();

   // [Tooltip("Set starting page index - starting from 0")]
   // public int startingPage = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;
    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int _fastSwipeThresholdMaxLimit;

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;

    // number of pages in container
    private int _pageCount;
    private int _currentPage;

    // whether lerping is in progress and target lerp position
    private bool _lerp;
    private Vector2 _lerpTo;

    // target position of every page
    private List<Vector2> _pagePositions = new List<Vector2>();

    // in draggging, when dragging started and where it started
    private bool _dragging;
    private float _timeStamp;
    private Vector2 _startPosition;

    // for showing small page icons
    private int _previousPageSelectionIndex;

    private float _updateTrackLength = 300.0f;
    #endregion

    #region Main Methors

    private void Start() {
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _lerp = false;
        StartCoroutine(RefreshPage());
    }

    #region Enumerators
    IEnumerator RefreshPage() {
        switch (groupName) {
            case "museum": {
                WWW wwwPath = new WWW("http://each.itsociety.su:4201/each/museum/all");
                yield return wwwPath;
                string jsonNewString = wwwPath.text;
                var node = JSON.Parse(jsonNewString);
                Debug.Log("Whyyyyy");
                RemoveMuseumButtons();
                AddMuseumButtons(node);
                break;
                }
            case "feed": {
                WWW wwwPath = new WWW("http://each.itsociety.su:4201/each/feed/all");
                yield return wwwPath;
                string jsonNewString = wwwPath.text;
                Debug.Log(jsonNewString);
                var node = JSON.Parse(jsonNewString);
                RemoveFeedButtons();
                AddFeedButtons(node);
                break;
            }
            default:
                break;
        }
    }
    #endregion

    void AddMuseumButtons(JSONNode n) {
        for (int i = 0; i < n.Count; ++i) {
            string tmp = n[i]["name"].Value;
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);
            MuseumSampleButton museumButton = newButton.GetComponent<MuseumSampleButton>();
            museumButton.SetName(tmp);
            StartCoroutine(museumButton.SetIcon(n[i]["image"][0]["url"].Value));
            museumButton.SetDesc(n[i]["desc"].Value);
            newButton.transform.SetParent(buttonTemplate.transform.parent);
            musButtonList.Add(newButton);
        }
    }
    void AddFeedButtons(JSONNode n) {
        for (int i = 0; i < n.Count; ++i) {
            string tmp = n[i]["title"].Value;
            Debug.Log(tmp);
            GameObject newButton = Instantiate(buttonTemplate) as GameObject;
            newButton.SetActive(true);
            FeedSampleButton museumButton = newButton.GetComponent<FeedSampleButton>();
            museumButton.SetName(tmp);
            Debug.Log(n[i]["image"][0]["url"].Value);
            StartCoroutine(museumButton.SetIcon(n[i]["image"][0]["url"].Value));
            newButton.transform.SetParent(buttonTemplate.transform.parent);
            feedButtonList.Add(newButton);
        }

    }
    void RemoveMuseumButtons() {
        foreach (GameObject item in musButtonList)
            Destroy(item);
        musButtonList.Clear();
    }
    void RemoveFeedButtons() {
        foreach (GameObject item in feedButtonList)
            Destroy(item);
        feedButtonList.Clear();
    }
    public void ButtonClicked(MuseumSampleButton button) {
        Debug.Log(button.name + " button clicked.");
        switch (groupName) {
            case "museum": {
                MuseumSamplePage museumSamplePage = sideMenu.GetComponent<MuseumSamplePage>();
                museumSamplePage.SetData(button);
                break;
            }
            case "feed": {
                break;
            }
        }

    }
    #endregion

    #region Helper Methods
    public void OnBeginDrag(PointerEventData aEventData) {
        // if currently lerping, then stop it as user is draging
        _lerp = false;
        // not dragging yet
        _dragging = false;
    }

    public void OnEndDrag(PointerEventData aEventData) {
        float difference;

        difference = -(_startPosition.y - _container.anchoredPosition.y);

        // test for fast swipe - swipe that moves only +/-1 item

        /*  if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime && 
              Mathf.Abs(difference) > fastSwipeThresholdDistance &&
              Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit) {*/
        if (Time.unscaledTime - _timeStamp > fastSwipeThresholdTime) {
            if (difference > _updateTrackLength) {
                Debug.Log("What??? Supply!");
                /////
            }
            else if (difference < -_updateTrackLength) {
                Debug.Log("What??? Reload!");
                if (groupName == "museum") {
                    Debug.Log("Removed");
                    StartCoroutine(RefreshPage());
                    Debug.Log("Refreshed");
                }
                else if (groupName == "feed") {
                    Debug.Log("Removed");
                    StartCoroutine(RefreshPage());
                    Debug.Log("Refreshed");
                }
               // StartCoroutine(FadeLoad());
            }
        }

        _dragging = false;
    }

    public void OnDrag(PointerEventData aEventData) {
        if (!_dragging) {
            // dragging started
            _dragging = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            _timeStamp = Time.unscaledTime;
            // save current position of cointainer
            _startPosition = _container.anchoredPosition;
        }
        if (Math.Abs(_startPosition.y - _container.anchoredPosition.y) > _updateTrackLength / 1.0) {
           // Debug.Log("HAHAHA");
           // StartCoroutine(FadeLoad());
        }

    }
    #endregion
}

[Serializable]
public class Museum {
    public string name;
    public string desc;
    public string icon;
}
public class Feed {
    public int eid;
    public string title;
    public string text;
    public string image; 
    public string url; 
}
