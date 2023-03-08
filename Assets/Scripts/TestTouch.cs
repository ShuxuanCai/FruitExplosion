using UnityEngine;

public class TestTouch : MonoBehaviour
{
    private InputManager inputManager;

    private Camera cameraMain;

    void Start()
    {

    }

    private void Awake()
    {
        inputManager = InputManager.Instance;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
        //inputManager.OnStartTouch += DrawRay;
    }

    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
        //inputManager.OnEndTouch -= DrawRay;
    }

    private void Move(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        transform.position = worldCoordinates;
    }

    //private void DrawRay(Vector2 screenPosition, float time)
    //{
    //    Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
    //    Ray ray = cameraMain.ScreenPointToRay(screenCoordinates);
    //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
    //}
}
