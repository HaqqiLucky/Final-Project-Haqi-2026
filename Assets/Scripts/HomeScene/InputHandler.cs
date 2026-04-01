using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [Header("Kebutuhan Plot")]
    private Camera _mainCamera;
    [SerializeField] private HomeSceneControl home;

    [Header("Boolean")]
    public bool HouseClicked = false;
    public bool BaloonClicked = false;

    [Header("GameObject")]
    [SerializeField] private GameObject House;
    [SerializeField] private GameObject Baloon;
    //[SerializeField] private GameObject Puzzle;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        string objectName = rayHit.collider.gameObject.name;

        if (objectName == "House")
        {
            HouseClicked = true;
            home.OpenChangeBaloon();
        }
        if (objectName == "Baloon")
        {
            BaloonClicked = true;
            home.PuzzleMenuBalloonOnClick();
        }
    }
}
