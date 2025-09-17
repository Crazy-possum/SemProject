using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private NavMeshAgent _enemyAgent;
    private bool _followPlayer;
    /* void Start()
     {
         StartCoroutine(ChangePoint(0));
     }

     private IEnumerator ChangePoint(int point)
     {
         for (int i = point; i < _points.Length; i++)
         {
             _enemyAgent.SetDestination(_points[i].localPosition);
             Debug.Log(i);
             while (_enemyAgent.remainingDistance <  0.1f)
             {
                 yield return new WaitForSeconds(0.2f);
                 Debug.Log(i + " wait");
             }
             //yield return new WaitForSeconds(0.5f);
         }
         StartCoroutine(ChangePoint(0));
     }*/

   
  
    private int _currentWayPoint;
    private void Awake()
    {
        _currentWayPoint = 0;
        _enemyAgent.SetDestination(_points[_currentWayPoint].position);
    }
    private
    void Update()
    {
        if (_enemyAgent.remainingDistance < .1f)
        {
            if (_currentWayPoint < _points.Length - 1)
                _currentWayPoint++;
            else
                _currentWayPoint = 0;
        }
        _enemyAgent.SetDestination(_points[_currentWayPoint].position);

    }

}
