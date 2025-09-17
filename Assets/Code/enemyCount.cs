using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCount : MonoBehaviour
{
    // Start is called before the first frame update
    private int _score = 0;

    // Вызывается когда другой коллайдер входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        
        // Проверяем тэг объекта
        if (other.CompareTag("Enemy"))
        {
            // Увеличиваем счет
            _score++;

            // Выводим в консоль
            Debug.Log("Счет: " + _score.ToString());

            // Опционально: уничтожаем объект врага после столкновения
            // Destroy(other.gameObject);
        }
    }
}
