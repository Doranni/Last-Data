using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class DebugUI : MonoBehaviour
{
    [SerializeField] private Health health_cockpit, health_cockpitExtension, health_coreChassy,
        health_twinChassy_R, health_twinChassy_L, health_wings, health_boosterPlug, health_booster, health_gun_L, health_gun_R;

    private VisualElement debugScreen;
    private Label lbl_shipSpeed,
        lbl_health_cockpit, lbl_health_cockpitExtension, lbl_health_coreChassy, 
        lbl_health_twinChassy_R, lbl_health_twinChassy_L, lbl_health_wings, 
        lbl_health_boosterPlug, lbl_health_booster, lbl_health_gun_L, lbl_health_gun_R;

    const string k_debugScreen = "DebugScreen";
    const string k_lbl_shipSpeed = "lbl_Speed_value";
    const string k_lbl_health_cockpit = "lbl_CockpitHealth_value";
    const string k_lbl_health_cockpitExtension = "lbl_CockpitExtensionHealth_value";
    const string k_lbl_health_coreChassy = "lbl_CoreChassyHealth_value";
    const string k_lbl_health_twinChassy_R = "lbl_TwinChassy_RHealth_value";
    const string k_lbl_health_twinChassy_L = "lbl_TwinChassy_LHealth_value";
    const string k_lbl_health_wings = "lbl_WingsHealth_value";
    const string k_lbl_health_boosterPlug = "lbl_BoosterPlugHealth_value";
    const string k_lbl_health_booster = "lbl_BoosterHealth_value";
    const string k_lbl_health_gun_L = "lbl_Gun_LHealth_value";
    const string k_lbl_health_gun_R = "lbl_Gun_RHealth_value";

    private void Awake()
    {
        VisualElement rootElement = GetComponent<UIDocument>().rootVisualElement;
        debugScreen = rootElement.Q(k_debugScreen);
        lbl_shipSpeed = rootElement.Q<Label>(k_lbl_shipSpeed);
        lbl_health_cockpit = rootElement.Q<Label>(k_lbl_health_cockpit);
        lbl_health_cockpitExtension = rootElement.Q<Label>(k_lbl_health_cockpitExtension);
        lbl_health_coreChassy = rootElement.Q<Label>(k_lbl_health_coreChassy);
        lbl_health_twinChassy_R = rootElement.Q<Label>(k_lbl_health_twinChassy_R);
        lbl_health_twinChassy_L = rootElement.Q<Label>(k_lbl_health_twinChassy_L);
        lbl_health_wings = rootElement.Q<Label>(k_lbl_health_wings);
        lbl_health_boosterPlug = rootElement.Q<Label>(k_lbl_health_boosterPlug);
        lbl_health_booster = rootElement.Q<Label>(k_lbl_health_booster);
        lbl_health_gun_L = rootElement.Q<Label>(k_lbl_health_gun_L);
        lbl_health_gun_R = rootElement.Q<Label>(k_lbl_health_gun_R);
    }

    private void Start()
    {
        health_cockpit.OnChangeHealth += DisplayHealth_Cockpit;
        health_cockpitExtension.OnChangeHealth += DisplayHealth_CockpitExtension;
        health_coreChassy.OnChangeHealth += DisplayHealth_CoreChassy;
        health_twinChassy_R.OnChangeHealth += DisplayHealth_TwinChassy_R;
        health_twinChassy_L.OnChangeHealth += DisplayHealth_TwinChassy_L;
        health_wings.OnChangeHealth += DisplayHealth_Wings;
        health_boosterPlug.OnChangeHealth += DisplayHealth_BoosterPlug;
        health_booster.OnChangeHealth += DisplayHealth_Booster;
        health_gun_L.OnChangeHealth += DisplayHealth_Gun_L;
        health_gun_R.OnChangeHealth += DisplayHealth_Gun_R;

        DisplayHealth_Cockpit((health_cockpit.CurrentHealth, health_cockpit.MaxHealth));
        DisplayHealth_CockpitExtension((health_cockpitExtension.CurrentHealth, health_cockpitExtension.MaxHealth));
        DisplayHealth_CoreChassy((health_coreChassy.CurrentHealth, health_coreChassy.MaxHealth));
        DisplayHealth_TwinChassy_R((health_twinChassy_R.CurrentHealth, health_twinChassy_R.MaxHealth));
        DisplayHealth_TwinChassy_L((health_twinChassy_L.CurrentHealth, health_twinChassy_L.MaxHealth));
        DisplayHealth_Wings((health_wings.CurrentHealth, health_wings.MaxHealth));
        DisplayHealth_BoosterPlug((health_boosterPlug.CurrentHealth, health_boosterPlug.MaxHealth));
        DisplayHealth_Booster((health_booster.CurrentHealth, health_booster.MaxHealth));
        DisplayHealth_Gun_L((health_gun_L.CurrentHealth, health_gun_L.MaxHealth));
        DisplayHealth_Gun_R((health_gun_R.CurrentHealth, health_gun_R.MaxHealth));
    }

    private void DisplayHealth_Gun_R((float currentHealth, float maxHealth) obj)
    {
        lbl_health_gun_R.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_Gun_L((float currentHealth, float maxHealth) obj)
    {
        lbl_health_gun_L.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_Booster((float currentHealth, float maxHealth) obj)
    {
        lbl_health_booster.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_BoosterPlug((float currentHealth, float maxHealth) obj)
    {
        lbl_health_boosterPlug.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_Wings((float currentHealth, float maxHealth) obj)
    {
        lbl_health_wings.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_TwinChassy_L((float currentHealth, float maxHealth) obj)
    {
        lbl_health_twinChassy_L.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_TwinChassy_R((float currentHealth, float maxHealth) obj)
    {
        lbl_health_twinChassy_R.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_CoreChassy((float currentHealth, float maxHealth) obj)
    {
        lbl_health_coreChassy.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_CockpitExtension((float currentHealth, float maxHealth) obj)
    {
        lbl_health_cockpitExtension.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    private void DisplayHealth_Cockpit((float currentHealth, float maxHealth) obj)
    {
        lbl_health_cockpit.text = obj.currentHealth.ToString("0.00") + "/" + obj.maxHealth.ToString("0.00");
    }

    void Update()
    {
        lbl_shipSpeed.text = GameManager.Instance.Speed.ToString("0.00");
    }

    private void OnDestroy()
    {
        health_cockpit.OnChangeHealth -= DisplayHealth_Cockpit;
        health_cockpitExtension.OnChangeHealth -= DisplayHealth_CockpitExtension;
        health_coreChassy.OnChangeHealth -= DisplayHealth_CoreChassy;
        health_twinChassy_R.OnChangeHealth -= DisplayHealth_TwinChassy_R;
        health_twinChassy_L.OnChangeHealth -= DisplayHealth_TwinChassy_L;
        health_wings.OnChangeHealth -= DisplayHealth_Wings;
        health_boosterPlug.OnChangeHealth -= DisplayHealth_BoosterPlug;
        health_booster.OnChangeHealth -= DisplayHealth_Booster;
        health_gun_L.OnChangeHealth -= DisplayHealth_Gun_L;
        health_gun_R.OnChangeHealth -= DisplayHealth_Gun_R;
    }
}
