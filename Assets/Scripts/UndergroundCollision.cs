using UnityEngine;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver)
        {
            string tag = other.tag;

            if (tag.Equals("Object"))
            {
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();

                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);

                Destroy(other.gameObject);

                //check if win
                if(Level.Instance.objectsInScene <= 0)
                {
                    //no more objects to collect
                    UIManager.Instance.ShowLevelCompletedUI();
                    //Play confetti efect
                    Level.Instance.PlayWinFX();

                    Invoke("NextLevel", 2f);
                }
            }
            else if (tag.Equals("Obstacle"))
            {
                //Restart level
                Game.isGameOver = true;
                //Camera shaking
                Camera.main.transform.DOShakePosition(1f, .2f, 20, 90f)
                    .OnComplete(() => {
                        Level.Instance.LoadRestartLevel();
                    });
            }
        }
    }

    void NextLevel()
    {
        Level.Instance.LoadNextLevel();
    }
}
