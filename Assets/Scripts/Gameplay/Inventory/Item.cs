using UnityEngine;

public abstract class Item : MonoBehaviour, IItem
{
    public abstract void DoAction(PlayerCharacter playerCharacter);
}
