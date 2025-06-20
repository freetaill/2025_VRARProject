using UnityEngine;

public enum MagicType
{
    FireBall,
    IceBall,
    Lightning
}

[CreateAssetMenu(fileName = "NewMagicData", menuName = "Magic/Magic Data")]
public class MagicData : ScriptableObject
{
    public MagicType magicType;
    public int damage;
    public float speed;
    public GameObject hitEffect;
}

