using UnityEngine;

public class CharacterAvatar : MonoBehaviour
{
    [SerializeField] Transform _armPosition;

    public Transform GetArmPosition()
    {
        return _armPosition;
    }
}
