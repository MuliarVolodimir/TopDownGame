using UnityEngine;

public class SoohtController
{
    public void CheckShoot(Character character, Weapon weapon)
    {
        if (character != null && weapon != null)
        {
            var charResources = character.GetResources();
            var weaponResources = weapon.GetResources();

            for (int i = 0; i < weaponResources.Count; i++)
            {
                var resource = charResources.Find(res => res == weaponResources[i]);

                if (resource != null)
                {
                    if (resource.Count >= weaponResources[i].Count)
                    {
                        resource.Count -= weaponResources[i].Count;
                        weapon.DoAction();
                    }
                    else
                    {
                        Debug.Log("not resource");
                        break;
                    }
                }
            }
        }
    }
}
       
