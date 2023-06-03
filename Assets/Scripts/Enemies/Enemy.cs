using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    protected CharacterStats stats;

    // Start is called before the first frame update
    void Awake ()
    {
        stats = GetComponent<CharacterStats>(); 
    }
}
