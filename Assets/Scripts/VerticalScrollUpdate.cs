using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class VerticalScrollUpdate : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [Tooltip("Set starting page index - starting from 0")]
    public int startingPage = 0;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;
    public GameObject pageToClose;

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

    //------------------------------------------------------------------------
    void Start()
    {
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _lerp = false;

        // init
   //     SetPagePositions();
    //    SetPage(startingPage);
       // InitPageSelection();
      //  SetPageSelection(startingPage);
    }

    //------------------------------------------------------------------------
   /* void Update()
    {
        // if moving to target position
        if (_lerp)
        {
            Debug.Log("HAHAHA");
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
            {
                Debug.Log("HAHAHA1");
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }
        }
    }*/

    //------------------------------------------------------------------------
  /*  private void SetPagePositions()
    {
        int width = 0;
        int height = 0;
        int offsetX = 0;
        int offsetY = 0;
        int containerWidth = 0;
        int containerHeight = 0;


        height = (int)_scrollRectRect.rect.height;
        offsetY = height / 2;
        containerHeight = height * _pageCount;
        _fastSwipeThresholdMaxLimit = height;

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        // delete any previous settings
        _pagePositions.Clear();

        // iterate through all container childern and set their positions
        for (int i = 0; i < _pageCount; i++) {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            childPosition = new Vector2(0f, -(i * height - containerHeight / 2 + offsetY));
            child.anchoredPosition = childPosition;
            _pagePositions.Add(-childPosition);
        }
    }

    //------------------------------------------------------------------------
    private void SetPage(int aPageIndex)
    {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
        _container.anchoredPosition = _pagePositions[aPageIndex];
        _currentPage = aPageIndex;
    }
*/
    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData)
    {
        // if currently lerping, then stop it as user is draging
        _lerp = false;
        // not dragging yet
        _dragging = false;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData)
    {
        // how much was container's content dragged
        float difference;

        difference = -(_startPosition.y - _container.anchoredPosition.y);

        // test for fast swipe - swipe that moves only +/-1 item

        /*  if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime && 
              Mathf.Abs(difference) > fastSwipeThresholdDistance &&
              Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit) {*/
        if (difference > 300)
        {
       //     _lerp = true;
        }
            else if (difference < -300)
                Debug.Log("What??? Reload!");

        _dragging = false;
    }

    /*void Update() {
        if (_lerp)
        {
            int width = (int)_scrollRectRect.rect.width;
            int height = (int)_scrollRectRect.rect.height;
            Debug.Log("What??? Supply!");
            _lerpTo = new Vector2(0f, (2 * height));
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
          //  if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
           // {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
                Debug.Log("???");
                for (int i = 0; i < pageToClose.transform.childCount; i++)
                {
                    var child = pageToClose.transform.GetChild(i).gameObject;
                    if (child != null)
                        child.SetActive(false);
                }
                //pageToClose.;
           // }
        }
    }*/
    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData)
    {
        if (!_dragging)
        {
            // dragging started
            _dragging = true;
            // save time - unscaled so pausing with Time.scale should not affect it
            _timeStamp = Time.unscaledTime;
            // save current position of cointainer
            _startPosition = _container.anchoredPosition;
        }

    }
}
