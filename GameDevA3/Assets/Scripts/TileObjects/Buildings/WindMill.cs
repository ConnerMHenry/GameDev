using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : Building {


    public void Start()
    {
        providedRequirements = LivingRequirements.Energy;
        howMuchRequirements = 10;
        requirementTime = 60;
    }
}
