using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainScreenUI : MonoBehaviour
{
    [SerializeField] ShipHealth shipHealth;

    private VisualElement speedScreen;
    private VisualElement shipStatsScreen;
    private VisualElement resourcesScreen;

    private Label lbl_shipSpeed;
    private Label lbl_shipHealth;
    private Label lbl_shipFuel;
    private Label lbl_laserCharge;

    const string k_speedScreen = "Speed";
    const string k_shipStatsScreen = "ShipStats";
    const string k_resourcesScreen = "Resouces";

    const string k_lbl_shipSpeed = "lbl_ShipSpeed_value";
    const string k_lbl_shipHealth = "lbl_ShipHealth_value";
    const string k_lbl_shipFuel = "lbl_ShipFuel_value";
    const string k_lbl_laserCharge = "lbl_laserCharge_value";

    void Awake()
    {
        VisualElement rootElement = GetComponent<UIDocument>().rootVisualElement;

        speedScreen = rootElement.Q(k_speedScreen);
        shipStatsScreen = rootElement.Q(k_shipStatsScreen);
        resourcesScreen = rootElement.Q(k_resourcesScreen);

        lbl_shipSpeed = rootElement.Q<Label>(k_lbl_shipSpeed);
        lbl_shipHealth = rootElement.Q<Label>(k_lbl_shipHealth);
        lbl_shipFuel = rootElement.Q<Label>(k_lbl_shipFuel);
        lbl_laserCharge = rootElement.Q<Label>(k_lbl_laserCharge);
    }

    private void Start()
    {
        GameManager.Instance.OnSpeedChanged += DisplaySpeed;
        shipHealth.OnChangeHealth += DisplayShipHealth;

        DisplaySpeed();
        DisplayShipHealth(shipHealth.GetTotalHealth());
    }

    private void DisplayShipHealth((float currentValue, float maxValue) value)
    {
        lbl_shipHealth.text = value.currentValue.ToString("0") + "/" + value.maxValue.ToString("0");
    }

    private void DisplaySpeed()
    {
        lbl_shipSpeed.text = GameManager.Instance.SpeedGoal.ToString("0") + " km/s";
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSpeedChanged -= DisplaySpeed;
    }
}
