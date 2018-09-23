using UnityEngine;
using System.Linq;

public class MoveAndPinchZoom : MonoBehaviour
{
    [SerializeField]
    Camera m_Camera;

    const float ROTATE_SPEED = 0.1f;
    const float UP_DOWN_SPEED = 0.01f;
    const float ZOOM_SPEED = 0.05f;
    const float MAX_ZOOM_SIZE = 10f;
    float mMaxZoomOutSize;

    private void Start() {
        mMaxZoomOutSize = Camera.main.fieldOfView;
    }

    void Update()
    {
        int touchCount = Input.touches.Count(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
        if (touchCount == 1)
        {
            Touch t = Input.touches.First();
            switch (t.phase)
            {
                case TouchPhase.Moved:
                    float xDelta = t.deltaPosition.x * ROTATE_SPEED;
                    float yDelta = t.deltaPosition.y * UP_DOWN_SPEED;
                    m_Camera.transform.Rotate(0, xDelta, 0, Space.World);
                    m_Camera.transform.position += new Vector3(0, yDelta, 0);
                    break;
            }
        } else if (touchCount == 2) {
            Touch firstTouch = Input.touches.First();
            Touch secondTouch = Input.touches.Last();
            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float prevTouchDeltaMag = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;
            float diff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView + diff * ZOOM_SPEED, MAX_ZOOM_SIZE, mMaxZoomOutSize);
        }
    }
}
