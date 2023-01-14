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
    private Label lbl_metals;
    private Label lbl_uranium;
    private Label lbl_rawData;

    const string k_speedScreen = "Speed";
    const string k_shipStatsScreen = "ShipStats";
    const string k_resourcesScreen = "Resouces";

    const string k_lbl_shipSpeed = "lbl_ShipSpeed_value";
    const string k_lbl_shipHealth = "lbl_ShipHealth_value";
    const string k_lbl_shipFuel = "lbl_ShipFuel_value";
    const string k_lbl_laserCharge = "lbl_laserCharge_value";
    const string k_lbl_metals = "lbl_metals_value";
    const string k_lbl_uranium = "lbl_uranium_value";
    const string k_lbl_rawData = "lbl_rawData_value";

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
        lbl_metals = rootElement.Q<Label>(k_lbl_metals);
        lbl_uranium = rootElement.Q<Label>(k_lbl_uranium);
        lbl_rawData = rootElement.Q<Label>(k_lbl_rawData);
    }

    private void Start()
    {
        GameManager.Instance.OnSpeedChanged += DisplaySpeed;
        shipHealth.OnChangeHealth += DisplayShipHealth;
        ShipStorage.Instance.Metals.OnAmountChanged += DisplayMetals;
        ShipStorage.Instance.Metals.OnCapacityChanged += DisplayMetals;
        ShipStorage.Instance.Uranium.OnAmountChanged += DisplayUranium;
        ShipStorage.Instance.Uranium.OnCapacityChanged += DisplayUranium;
        ShipStorage.Instance.RawData.OnAmountChanged += DisplayRawData;
        ShipStorage.Instance.RawData.OnCapacityChanged += DisplayRawData;

        DisplaySpeed();
        DisplayShipHealth(shipHealth.GetTotalHealth());
        DisplayMetals();
        DisplayUranium();
        DisplayRawData();
    }

    private void Update()
    {
        DisplayFuel();
        DisplayLaserCharge();
    }

    private void DisplayShipHealth((float currentValue, float maxValue) value)
    {
        lbl_shipHealth.text = value.currentValue.ToString("0") + "/" + value.maxValue.ToString("0");
    }

    private void DisplaySpeed()
    {
        lbl_shipSpeed.text = GameManager.Instance.SpeedGoal.ToString("0") + " km/s";
    }

    private void DisplayFuel()
    {
        lbl_shipFuel.text = ShipStorage.Instance.Fuel.CurrentAmount.ToString("0") + "/" 
            + ShipStorage.Instance.Fuel.Capacity.ToString("0");
    }

    private void DisplayLaserCharge()
    {
        lbl_laserCharge.text = ShipStorage.Instance.LaserCharge.CurrentAmount.ToString("0") + "/" 
            + ShipStorage.Instance.LaserCharge.Capacity.ToString("0");
    }

    private void DisplayMetals()
    {
        lbl_metals.text = ShipStorage.Instance.Metals.CurrentAmount.ToString("0") + "/"
            + ShipStorage.Instance.Metals.Capacity.ToString("0");
    }

    private void DisplayUranium()
    {
        lbl_uranium.text = ShipStorage.Instance.Uranium.CurrentAmount.ToString("0") + "/"
            + ShipStorage.Instance.Uranium.Capacity.ToString("0");
    }

    private void DisplayRawData()
    {
        lbl_rawData.text = ShipStorage.Instance.RawData.CurrentAmount.ToString("0") + "/"
            + ShipStorage.Instance.RawData.Capacity.ToString("0");
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSpeedChanged -= DisplaySpeed;
        shipHealth.OnChangeHealth -= DisplayShipHealth;
        ShipStorage.Instance.Metals.OnAmountChanged -= DisplayMetals;
        ShipStorage.Instance.Metals.OnCapacityChanged -= DisplayMetals;
        ShipStorage.Instance.Uranium.OnAmountChanged -= DisplayUranium;
        ShipStorage.Instance.Uranium.OnCapacityChanged -= DisplayUranium;
        ShipStorage.Instance.RawData.OnAmountChanged -= DisplayRawData;
        ShipStorage.Instance.RawData.OnCapacityChanged -= DisplayRawData;
    }
}
