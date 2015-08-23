using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndGameHandler : MonoBehaviour {

    public MessageBox endGameMessageBox;
    public TextBox endGameTextBox;

    [Multiline]
    public string tankWinText = "TANK WINS! CLICK OK TO PLAY AGAIN.";

    [Multiline]
    public string monsterWinText = "MONSTER WINS! CLICK OK TO PLAY AGAIN.";

    public bool gameEnding = false;

    public void TankWon()
    {
        if (!gameEnding)
        {
            gameEnding = true;
            endGameTextBox.textList = new List<string>();

            endGameTextBox.textList.Add(tankWinText);

            StartCoroutine(WaitThenRestartGame());
        }

    }

    public void MonsterWon()
    {
        if (!gameEnding)
        {
            gameEnding = true;
            endGameTextBox.textList = new List<string>();

            endGameTextBox.textList.Add(monsterWinText);

            StartCoroutine(WaitThenRestartGame());
        }
        
    }

    public IEnumerator WaitThenRestartGame()
    {
        Globals.Instance.Pause(true);

        yield return StartCoroutine(endGameMessageBox.OpenAndWait(endGameMessageBox.openTime));

        while (endGameMessageBox.isOpen)
        {
            yield return null;
        }

        Globals.Instance.Pause(false);
        Globals.Instance.acceptPlayerGameInput = false;

        yield return new WaitForSeconds(2.0f);

        //reset game will unpause for us
        Globals.Instance.resetGameHandler.ResetGame();

        gameEnding = false;

    }
}
