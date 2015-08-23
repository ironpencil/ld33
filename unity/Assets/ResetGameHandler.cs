using UnityEngine;
using System.Collections;

public class ResetGameHandler : MonoBehaviour {

    public GameObject monsterPrefab;
    public GameObject tankPrefab;

    public GameObject buildingPrefab;

    public Transform monsterStart;
    public Transform tankStart;    

    private GameObject monsterInstance;
    private GameObject tankInstance;
    private GameObject buildingInstance;

    public Transform worldParent;

    public MessageBox startRoundMessageBox;    

    public void ResetGame()
    {

        Globals.Instance.Pause(true);

        if (monsterInstance != null)
        {
            Destroy(monsterInstance);
        }

        if (tankInstance != null)
        {
            Destroy(tankInstance);
        }

        if (buildingInstance != null)
        {
            Destroy(buildingInstance);
        }

        monsterInstance = (GameObject)GameObject.Instantiate(monsterPrefab, monsterStart.position, monsterStart.rotation);

        tankInstance = (GameObject)GameObject.Instantiate(tankPrefab, tankStart.position, tankStart.rotation);

        buildingInstance = (GameObject)GameObject.Instantiate(buildingPrefab, Vector3.zero, Quaternion.identity);

        monsterInstance.transform.parent = worldParent;
        tankInstance.transform.parent = worldParent;
        buildingInstance.transform.parent = worldParent;

        monsterInstance.gameObject.SetActive(true);
        tankInstance.gameObject.SetActive(true);
        buildingInstance.gameObject.SetActive(true);

        StartCoroutine(WaitAndStartGame());
    }

    public IEnumerator WaitAndStartGame()
    {
        yield return StartCoroutine(startRoundMessageBox.OpenAndWait(startRoundMessageBox.openTime));

        while (startRoundMessageBox.isOpen)
        {
            yield return null;
        }

        Globals.Instance.Pause(false);
        Globals.Instance.acceptPlayerGameInput = false;

        yield return new WaitForSeconds(startRoundMessageBox.closeTime + 0.5f);

        Globals.Instance.acceptPlayerGameInput = true;
    }
}
