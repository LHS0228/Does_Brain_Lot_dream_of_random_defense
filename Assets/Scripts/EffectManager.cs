using UnityEngine;

public enum EffectName {
    dust,
}

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;

    public GameObject dustEffect;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnEffect(EffectName effectName, Vector3 spawnPostion)
    {
        switch(effectName)
        {
            case EffectName.dust:
                Instantiate(dustEffect, spawnPostion, Quaternion.identity);
                break;
        }
    }

    public void SpawnEffect(EffectName effectName, Vector3 spawnPostion, Quaternion quaternion)
    {
        switch (effectName)
        {
            case EffectName.dust:
                Instantiate(dustEffect, spawnPostion, quaternion);
                break;
        }
    }
}
