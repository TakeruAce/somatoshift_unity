using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Networking;

// HTTP リクエストでSomatoShiftと通信する
// ブラウザでアクセスしてコマンド送る挙動を模倣しているだけ

public class SomatoShiftManager : MonoBehaviour
{

    public string ip = "192.168.8.49"; //SomatoShiftのIPアドレス
    public string port = "80";
    [SerializeField]
    public bool isActivateTorque = false;
    [SerializeField]
    public bool isMotionCombination = false;
    [SerializeField]
    public bool resetPosition = false;
    [HideInInspector]
    public float inertiaDiffX = 0f;
    [HideInInspector]
    public float inertiaDiffY = 0f;
    [HideInInspector]
    public float inertiaDiffZ = 0f;
    [HideInInspector]
    public float viscosityDiffX = 0f;
    [HideInInspector]
    public float viscosityDiffY = 0f;
    [HideInInspector]
    public float viscosityDiffZ = 0f;    
    [HideInInspector]
    public float flyWheelSpeed = 0f;
    private Dictionary<string, string> uri_params;
    // Start is called before the first frame update
    void Start()
    {
        // uri parameterと機能の対応
        uri_params = new Dictionary<string, string>();
        uri_params.Add("flyWheelSpeed","slider1");
        uri_params.Add("targetPos1","slider2");
        uri_params.Add("targetPos2","slider3");
        uri_params.Add("targetPos3","slider4");
        uri_params.Add("targetPos4","slider5");
        uri_params.Add("inertiaDiffX","slider6");
        uri_params.Add("inertiaDiffY","slider7");
        uri_params.Add("inertiaDiffZ","slider8");
        uri_params.Add("viscosityDiffX","slider9");
        uri_params.Add("viscosityDiffY","slider10");
        uri_params.Add("viscosityDiffZ","slider11");
        uri_params.Add("manualTorqueX","slider18");
        uri_params.Add("manualTorqueY","slider19");
        uri_params.Add("manualTorqueZ","slider20");
        uri_params.Add("activateTorque","checkbox1");
        uri_params.Add("motionCombination","checkbox2");
        uri_params.Add("ResetPosition","checkbox3");
        uri_params.Add("isPitch","checkbox4");
        uri_params.Add("manualTorqueActivate","checkbox6");
    }

    public void SendCurrentStateAll() {
        SetFlywheelSpeed(flyWheelSpeed);
        SetInertiaDiff("X", inertiaDiffX);
        SetInertiaDiff("Y", inertiaDiffY);
        SetInertiaDiff("Z", inertiaDiffZ);
        SetViscosityDiff("X", viscosityDiffX);
        SetViscosityDiff("Y", viscosityDiffY);
        SetViscosityDiff("Z", viscosityDiffZ);
        SetActivateTorque(isActivateTorque);
        SetMotionCombination(isMotionCombination);
    }

    public void SetFlywheelSpeed(float speed) {
        string uri = makeUri("flyWheelSpeed", speed);
        StartCoroutine(GetRequest(uri));
    }
    // id 1 to 4
    public void SetMotorPosition(int id, int angle) {
        string uri = makeUri("targetPos"+id, angle);
        StartCoroutine(GetRequest(uri));
    }

    // direction: 方向　X, Y, Z
    // value: 差分　0.001 - 0.002ぐらいまでが良い
    public void SetInertiaDiff(string direction, float value) {
        string uri = makeUri("inertiaDiff"+direction, value);
        StartCoroutine(GetRequest(uri));
    }

    public void SetViscosityDiff(string direction, float value) {
        string uri = makeUri("viscosityDiff"+direction, value);
        StartCoroutine(GetRequest(uri));
    }

    public void SetManualTorqueDiff(string direction, float value) {
        string uri = makeUri("ManualTorque"+direction, value);
        StartCoroutine(GetRequest(uri));
    }

    public void SetActivateTorque(bool value) {
        string uri = makeUri("activateTorque", value);
        StartCoroutine(GetRequest(uri));
    }    
    public void SetMotionCombination(bool value) {
        string uri = makeUri("motionCombination", value);
        StartCoroutine(GetRequest(uri));
    }
    public void SetResetPosition(bool value) {
        string uri = makeUri("ResetPosition", value);
        StartCoroutine(GetRequest(uri));
    }
    public void SetIsPitch(bool value) {
        string uri = makeUri("resetPosition", value);
        StartCoroutine(GetRequest(uri));
    }
    public void SetManualTorqueActivate(bool value) {
        string uri = makeUri("resetPosition", value);
        StartCoroutine(GetRequest(uri));
    }




    private string makeUri(string method, float value) {
        return "http://" + ip + ":" + port + "/" + uri_params[method] + "?value=" + value;
    }    
    private string makeUri(string method, int value) {
        return "http://" + ip + ":" + port + "/" + uri_params[method] + "?value=" + value;
    }   
    private string makeUri(string method, bool value) {
        return "http://" + ip + ":" + port + "/" + uri_params[method] + "?value=" + (value? "true":"false");
    }
    // HTTP GET request
    private IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // リクエストを送信し、応答が返るのを待つ
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                // 通信エラーが発生した場合
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                // 応答テキストをログに出力
                // Debug.Log("Received: " + webRequest.downloadHandler.text);
            }
        }
    }
}
