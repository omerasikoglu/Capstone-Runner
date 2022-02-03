using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class PlatformGod : MonoBehaviour
{
    [SerializeField, BoxGroup("[Prefabs]")] private GameObject platform;
    [SerializeField, BoxGroup("[Prefabs]")] private List<GameObject> platformList;
    [SerializeField, BoxGroup("[Settings]")] private float distanceBetweenPlatforms = 10f;

    private Vector3 currentPos;

    private void Awake()
    {
        currentPos = new Vector3(0, 0, platformList.Count * distanceBetweenPlatforms);
    }

    [Button]
    private void Create()
    {
        //GameObject platformm = new GameObject($"platform{platformList.Count}");

        GameObject platform = Instantiate(this.platform, Vector3.forward * platformList.Count * distanceBetweenPlatforms,
            Quaternion.identity, transform);
        platformList.Add(platform);

        platform.name = $"platform{platformList.Count}";
        currentPos += Vector3.forward * distanceBetweenPlatforms;

    }

    [Button]
    private void RemoveLastOne()
    {
        if (platformList.Count <= 1 || transform.childCount <= 1) return;

        //world space'den siler
        DestroyImmediate(platformList[platformList.Count - 1]);

        //listeden çýkarýr
        platformList.RemoveAt(platformList.Count - 1);

        currentPos -= Vector3.forward * distanceBetweenPlatforms;


    }




}
