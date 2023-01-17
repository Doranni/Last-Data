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
    private Label lbl_charge;
    private Label lbl_metals;
    private Label lbl_uranium;
    private Label lbl_rawData;

    const string k_speedScreen = "Speed";
    const string k_shipStatsScreen = "ShipStats";
    const string k_resourcesScreen = "Resouces";

    const string k_lbl_shipSpeed = "lbl_ShipSpeed_value";
    const string k_lbl_shipHealth = "lbl_ShipHealth_value";
    const string k_lbl_shipFuel = "lbl_ShipFuel_value";
    const string k_lbl_charge = "lbl_charge_value";
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
        lbl_charge = rootElement.Q<Label>(k_lbl_charge);
        lbl_metals = rootElement.Q<Label>(k_lbl_metals);
        lbl_uranium = rootElement.Q<Label>(k_lbl_uranium);
        lbl_rawData = rootElement.Q<Label>(k_lbl_rawData);
    }

    private void Start()
    {
        GameManager.Instance.OnSpeedChanged += DisplaySpeed;
        shipHealth.OnChangeHealth += DisplayShipHealth;
        ShipStorage.Instance.OnMetalsChanged += DisplayMetals;
        ShipStorage.Instance.OnUraniumChanges += DisplayUranium;
        ShipStorage.Instance.OnRawDataChanged += DisplayRawData;

        DisplaySpeed();
        DisplayShipHealth(shipHealth.GetTotalHealth());
        DisplayMetals(ShipStorage.Instance.ShowResAmount(ResourceType.metals));
        DisplayUranium(ShipStorage.Instance.ShowResAmount(ResourceType.uranium));
        DisplayRawData(ShipStorage.Instance.ShowResAmount(ResourceType.rawData));
    }

    private void Update()
    {
        DisplayFuel(ShipStorage.Instance.ShowResAmount(ResourceType.fuel));
        DisplayLaserCharge(ShipStorage.Instance.ShowResAmount(ResourceType.charge));
    }

    private void DisplayShipHealth((float currentValue, float maxValue) value)
    {
        lbl_shipHealth.text = value.currentValue.ToString("0") + "/" + value.maxValue.ToString("0");
    }

    private void DisplaySpeed()
    {
        lbl_shipSpeed.text = GameManager.Instance.SpeedToDisplay.ToString("0") + " km/s";
    }

    private void DisplayFuel((float currentAmount, float capacity) value)
    {
        lbl_shipFuel.text = value.currentAmount.ToString("0") + "/" + value.capacity.ToString("0");
    }

    private void DisplayLaserCharge((float currentAmount, float capacity) value)
    {
        lbl_charge.text = value.currentAmount.ToString("0") + "/" + value.capacity.ToString("0");
    }

    private void DisplayMetals((float currentAmount, float capacity) value)
    {
        lbl_metals.text = value.currentAmount.ToString("0") + "/" + value.capacity.ToString("0");
    }

    private void DisplayUranium((float currentAmount, float capacity) value)
    {
        lbl_uranium.text = value.currentAmount.ToString("0") + "/" + value.capacity.ToString("0");
    }

    private void DisplayRawData((float currentAmount, float capacity) value)
    {
        lbl_rawData.text = value.currentAmount.ToString("0") + "/" + value.capacity.ToString("0");
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnSpeedChanged -= DisplaySpeed;
        shipHealth.OnChangeHealth -= DisplayShipHealth;
        ShipStorage.Instance.OnMetalsChanged -= DisplayMetals;
        ShipStorage.Instance.OnUraniumChanges -= DisplayUranium;
        ShipStorage.Instance.OnRawDataChanged -= DisplayRawData;
    }
}
