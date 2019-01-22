using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class roundsSurvived : MonoBehaviour {

	public Text roundsText;

	void OnEnable(){
		StartCoroutine (animateText ());
	}

	IEnumerator animateText(){
		roundsText.text = "0";
		int round = 0;

		yield return new WaitForSeconds (.7f);

		while (round < Spawner.wavesSpawned - 1) {
			round++;
			roundsText.text = round.ToString();

			yield return new WaitForSeconds (.05f);
		}
	}
}
