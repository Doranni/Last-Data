using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class DebugUI : MonoBehaviour
{
    private VisualElement debugScreen;
    private Label label_shipSpeed;

    const string k_debugScreen = "DebugScreen";
    const string k_label_shipSpeed = "lbl_Speed_value";

    private void Awake()
    {
        VisualElement rootElement = GetComponent<UIDocument>().rootVisualElement;
        debugScreen = rootElement.Q(k_debugScreen);
        label_shipSpeed = rootElement.Q<Label>(k_label_shipSpeed);
    }

    void Update()
    {
        label_shipSpeed.text = GameManager.Instance.Speed.ToString("0.00");
    }
}
