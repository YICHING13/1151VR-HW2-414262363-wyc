
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Texture2D winImage;
    public Font winFont;

    private bool won = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ButterController>() != null)
        {
            won = true;
            other.GetComponent<ButterController>().WinGame();
        }
    }

    void OnGUI()
    {
        if (!won) return;

        GUI.color = new Color(0, 0, 0, 0.55f);
        GUI.DrawTexture(
            new Rect(0, 0, Screen.width, Screen.height),
            Texture2D.whiteTexture
        );
        GUI.color = Color.white;

    
        float imageSize = Screen.height * 0.55f;

        if (winImage != null)
        {   
            GUI.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            GUI.DrawTexture(
                new Rect(
                    (Screen.width - imageSize) / 2,
                    Screen.height * 0.08f,
                    imageSize,
                    imageSize
                ),
                winImage,
                ScaleMode.ScaleToFit
            );
            GUI.color = Color.white;
        }

        GUIStyle style = new GUIStyle();
        style.font = winFont;
        style.fontSize = Mathf.RoundToInt(Screen.height * 0.16f);
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;

        GUI.Label(
            new Rect(
                0,
                Screen.height * 0.68f,
                Screen.width,
                Screen.height * 0.18f
            ),
            "YOU WIN!",
            style
        );
    }
}

