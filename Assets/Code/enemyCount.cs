using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCount : MonoBehaviour
{
    // Start is called before the first frame update
    private int _score = 0;

    // ���������� ����� ������ ��������� ������ � �������
    private void OnTriggerEnter(Collider other)
    {
        
        // ��������� ��� �������
        if (other.CompareTag("Enemy"))
        {
            // ����������� ����
            _score++;

            // ������� � �������
            Debug.Log("����: " + _score.ToString());

            // �����������: ���������� ������ ����� ����� ������������
            // Destroy(other.gameObject);
        }
    }
}
