using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivePerc
{ 
    void ActivatedSkill();
    void Init(HeroController hero);
    void Init(AliveEnemy alivies);
}
