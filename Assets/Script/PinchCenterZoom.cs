using UnityEngine;

public class PinchCenterZoom : MonoBehaviour {

    const float ZOOM_SPEED = 0.05f;
    float mMaxZoomOutSize;
    float mMaxZoomInSize;

    void Start () {
        mMaxZoomOutSize = Camera.main.fieldOfView;
        mMaxZoomInSize = 10f;
    }

    void Update () {
        if (Input.touchCount == 2)
        {
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);
            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float prevTouchDeltaMag = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;
            float diff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + diff * ZOOM_SPEED, mMaxZoomInSize, mMaxZoomOutSize);
        }
    }
}
