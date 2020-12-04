using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Entry : MonoBehaviour
{
    [SerializeField] private GameObject patronGroupPrefab;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        CreateAndSpawnGroup(4);
    }

    private void CreateAndSpawnGroup(int groupSize)
    {
        var newGroupObject = Instantiate(patronGroupPrefab, transform);
        var patronGroup = newGroupObject.GetComponentInChildren<PatronGroup>();
        patronGroup.SetSpawnAmount(groupSize);
        patronGroup.Spawn();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
        {
            var random = Random.Range(3, 5);
            CreateAndSpawnGroup(random);
            timer = 0f;
        }
    }

}
