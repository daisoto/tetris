using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetramineBehaviour : MonoBehaviour
{
    private ITetramine tetramine = null;

    public void Construct(ITetramine tetramine)
    {
        this.tetramine = tetramine;
    }
}
