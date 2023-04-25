using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] TextMeshPro scoreText;

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            playerMovement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        } else if (collisionInfo.collider.tag == "Point")
        {
            // detruire l'objet point et incrementer le score de 100
            Destroy(collisionInfo.collider.gameObject);
            float score = float.Parse(scoreText.text) + 100;
            scoreText.SetText(score.ToString("0"));

        }
    }
}
