using UnityEngine;

public class Firing : MonoBehaviour
{
    void Start()
    {
        InputManager.Instance.OnFireLeft_started += _ => FireLeft_Started();
        InputManager.Instance.OnFireLeft_canceled += _ => FireLeft_Canceled();
        InputManager.Instance.OnFireRight_started += _ => FireRight_Started();
        InputManager.Instance.OnFireRight_canceled += _ => FireRight_Canceled();
    }

    private void FireRight_Started()
    {
        Debug.Log("FireRight_Started");
    }

    private void FireRight_Canceled()
    {
        Debug.Log("FireRight_Canceled");
    }

    private void FireLeft_Started()
    {
        Debug.Log("FireLeft_Started");
    }

    private void FireLeft_Canceled()
    {
        Debug.Log("FireLeft_Canceled");
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnFireLeft_started -= _ => FireLeft_Started();
        InputManager.Instance.OnFireLeft_canceled -= _ => FireLeft_Canceled();
        InputManager.Instance.OnFireRight_started -= _ => FireRight_Started();
        InputManager.Instance.OnFireRight_canceled -= _ => FireRight_Canceled();
    }
}
