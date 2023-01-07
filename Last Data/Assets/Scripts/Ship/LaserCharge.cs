using UnityEngine;

public class LaserCharge : MonoBehaviour
{
    [SerializeField] private float chargeCapasity;
    private float currentCharge;

    public float CurrentCharge => currentCharge;
    public float ChargeCapasity => chargeCapasity;

    private void Awake()
    {
        currentCharge = chargeCapasity;
    }

    public void ChangeCharge(float value)
    {
        currentCharge = Mathf.Clamp(currentCharge + value, 0, chargeCapasity);
    }
}
