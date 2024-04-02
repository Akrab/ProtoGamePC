using RSG;
using UnityEngine;

public interface IResourseService
{
    IPromise<GameObject> LoadPlayer();
    IPromise<GameObject> LoadEnemy();
}