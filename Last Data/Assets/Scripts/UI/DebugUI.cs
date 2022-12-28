using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class DebugUI : MonoBehaviour
{
    [SerializeField] private Health health_cockpit, health_cockpitExtension, health_coreChassy,
        health_twinChassy_R, health_twinChassy_L, health_wings, health_boosterPlug, health_booster, health_gun_L, health_gun_R;

    [SerializeField] private Firing firing;

    private VisualElement debugScreen;
    private Label lbl_shipSpeed,
        lbl_health_cockpit, lbl_health_cockpitExtension, lbl_health_coreChassy, 
        lbl_health_twinChassy_R, lbl_health_twinChassy_L, lbl_health_wings, 
        lbl_health_boosterPlug, lbl_health_booster, lbl_health_gun_L, lbl_health_gun_R;

    private Label lbl_tLeft_health, lbl_tLeft_isSet, lbl_tLeft_astController, lbl_tLeft_collider,
        lbl_tRight_health, lbl_tRight_isSet, lbl_tRight_astController, lbl_tRight_collider;

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

    const string k_lbl_tLeft_health = "lbl_TLeft_Health_value";
    const string k_lbl_tLeft_isSet = "lbl_TLeft_IsSet_value";
    const string k_lbl_tLeft_astController = "lbl_TLeft_AstController_value";
    const string k_lbl_tLeft_collider = "lbl_TLeft_Collider_value";
    const string k_lbl_tRight_health = "lbl_TRight_Health_value";
    const string k_lbl_tRight_isSet = "lbl_TRight_IsSet_value";
    const string k_lbl_tRight_astController = "lbl_TRight_AstController_value";
    const string k_lbl_tRight_collider = "lbl_TRight_Collider_value";

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

        lbl_tLeft_health = rootElement.Q<Label>(k_lbl_tLeft_health);
        lbl_tLeft_isSet = rootElement.Q<Label>(k_lbl_tLeft_isSet);
        lbl_tLeft_astController = rootElement.Q<Label>(k_lbl_tLeft_astController);
        lbl_tLeft_collider = rootElement.Q<Label>(k_lbl_tLeft_collider);
        lbl_tRight_health = rootElement.Q<Label>(k_lbl_tRight_health);
        lbl_tRight_isSet = rootElement.Q<Label>(k_lbl_tRight_isSet);
        lbl_tRight_astController = rootElement.Q<Label>(k_lbl_tRight_astController);
        lbl_tRight_collider = rootElement.Q<Label>(k_lbl_tRight_collider);
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

        lbl_tLeft_isSet.text = firing.TargetLeft.isSet.ToString();
        if(firing.TargetLeft.astController != null)
        {
            lbl_tLeft_astController.text = firing.TargetLeft.astController.ToString();
            lbl_tLeft_health.text = firing.TargetLeft.astController.GetHealth().currentHealth.ToString("0.00") +
            "/" + firing.TargetLeft.astController.GetHealth().maxHealth.ToString("0.00");
        }
        else
        {
            lbl_tLeft_astController.text = "null";
            lbl_tLeft_health.text = "0/0";
        }
        if (firing.TargetLeft.collider != null)
        {
            lbl_tLeft_collider.text = firing.TargetLeft.collider.ToString();
        }
        else
        {
            lbl_tLeft_collider.text = "null";
        }

        lbl_tRight_isSet.text = firing.TargetRight.isSet.ToString();
        if (firing.TargetRight.astController != null)
        {
            lbl_tRight_astController.text = firing.TargetRight.astController.ToString();
            lbl_tRight_health.text = firing.TargetRight.astController.GetHealth().currentHealth.ToString("0.00") +
            "/" + firing.TargetRight.astController.GetHealth().maxHealth.ToString("0.00");
        }
        else
        {
            lbl_tRight_astController.text = "null";
            lbl_tRight_health.text = "0/0";
        }  
        if (firing.TargetRight.collider != null)
        {
            lbl_tRight_collider.text = firing.TargetRight.collider.ToString();
        }
        else
        {
            lbl_tRight_collider.text = "null";
        }

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
