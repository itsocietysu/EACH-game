using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour {
    IEnumerator Start() {
        if (!Input.location.isEnabledByUser)
            yield break;

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1) {
            print("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed) {
            print("Unable to determine device location");
            yield break;
        }
        else
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        Input.location.Stop();
    }
}