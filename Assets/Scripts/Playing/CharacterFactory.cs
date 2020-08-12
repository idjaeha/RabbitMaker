using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> characterPrefabs;
    private Dictionary<string, GameObject> characterDictionary;

    private void Awake()
    {
        //characterPrefabs = new List<GameObject>();
        characterDictionary = new Dictionary<string, GameObject>();
        InitDictionary();
    }

    private void InitDictionary()
    {
        foreach (GameObject prefab in characterPrefabs)
        {
            characterDictionary.Add(prefab.name, prefab);
        }
    }
    public GameObject Create(string name, int x, int y)
    {
        GameObject character = null;
        if (characterDictionary.ContainsKey(name))
        {
            Vector3 position = new Vector3(x, y, 0);
            character = Instantiate<GameObject>(characterDictionary[name], position, Quaternion.identity);
        }
        return character;
    }
}
