using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Routes> transit;

    #region singleton
    private static GameController instance = null;
    public static GameController Instance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }
    #endregion

}
