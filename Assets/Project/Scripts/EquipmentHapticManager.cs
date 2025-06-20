using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class EquipmentHapticManager : MonoBehaviour
{
    public enum EquipType {weapon, shield}
    public EquipType equipType;

    [SerializeField]
    private HapticImpulsePlayer hapticPlayer;

    [Range(0, 1)]
    public float amplitude;
    public float duration;
    [Tooltip("진동 주파수 (0 = default)")]
    public float frequency = 0f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy") && equipType == EquipType.weapon)
        {
            //Debug.Log("진동동동col");
            // 충돌 세기에 따라 진동 세기 조절 가능
            //float collisionForce = collision.relativeVelocity.magnitude;
            //amplitude = Mathf.Clamp01(collisionForce / 5f); // 세기 정규화 (조절 가능)
            Debug.Log(hapticPlayer.SendHapticImpulse(amplitude, duration, frequency));
            hapticPlayer.SendHapticImpulse(amplitude, duration, frequency);
        }
        else if (collision.transform.CompareTag("E_Weapon") && equipType == EquipType.shield)
        {
            Debug.Log(hapticPlayer.SendHapticImpulse(amplitude, duration, frequency));
            hapticPlayer.SendHapticImpulse(amplitude, duration, frequency);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("E_Weapon") && equipType == EquipType.shield)
        {
            Debug.Log(hapticPlayer.SendHapticImpulse(amplitude, duration, frequency));
            hapticPlayer.SendHapticImpulse(amplitude, duration, frequency);
        }
    }

    public void GetHapticController(GameObject controller)
    {
        Debug.Log("컨트롤러 받아옴 동작함");
        Debug.Log(controller.transform.name);
        hapticPlayer = controller.GetComponent<HapticImpulsePlayer>();

    }
}
