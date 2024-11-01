using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        protected IEnumerator WriteText(string input, Text textHolder, Color textColor, Font textFont, float delay, AudioClip sound, float delayBetweenLines)
        {
            if (textHolder == null)
            {
                Debug.LogError("TextHolder is null!");
                yield break; // Exit coroutine early if there's no text holder
            }

            textHolder.font = textFont;

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];

                if (sound != null && SoundManager.instance != null)
                {
                    SoundManager.instance.PlaySound(sound);
                }
                else
                {
                    Debug.LogWarning("SoundManager instance or sound clip is missing.");
                }

                yield return new WaitForSeconds(delay);
            }

            // Wait until the player presses the mouse button
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            finished = true;
        }
    }
}

