using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(SomatoShiftManager))]
public class SomatoShiftManagerEditor : Editor
{
    //表示したログ、SerializeFieldを付ける事でInspectorに表示されるように
    [SerializeField]
    public override void OnInspectorGUI(){
        //元のInspector部分を表示
        base.OnInspectorGUI ();
        //targetを変換して対象を取得
        SomatoShiftManager somatoshiftManager = target as SomatoShiftManager;

        somatoshiftManager.inertiaDiffX = EditorGUILayout.Slider("inertia diff X",somatoshiftManager.inertiaDiffX,-0.002f,0.002f);
        somatoshiftManager.inertiaDiffY = EditorGUILayout.Slider("inertia diff Y",somatoshiftManager.inertiaDiffY,-0.002f,0.002f);
        somatoshiftManager.inertiaDiffZ = EditorGUILayout.Slider("inertia diff Z",somatoshiftManager.inertiaDiffZ,-0.002f,0.002f);
        somatoshiftManager.viscosityDiffX = EditorGUILayout.Slider("viscosity diff X",somatoshiftManager.viscosityDiffX,-0.02f,0.02f);
        somatoshiftManager.viscosityDiffY = EditorGUILayout.Slider("viscosity diff Y",somatoshiftManager.viscosityDiffY,-0.02f,0.02f);
        somatoshiftManager.viscosityDiffZ = EditorGUILayout.Slider("viscosity diff Z",somatoshiftManager.viscosityDiffZ,-0.02f,0.02f);
        somatoshiftManager.flyWheelSpeed = EditorGUILayout.Slider("flyWheelSpeed",somatoshiftManager.flyWheelSpeed,0f,1000f);
        
        //PublicMethodを実行する用のボタン
        if (GUILayout.Button("All Status Send")){
            somatoshiftManager.SendCurrentStateAll ();
        }
        if (GUILayout.Button("Flywheel Stop")){
            somatoshiftManager.SetFlywheelSpeed (0f);
        }
        if (GUILayout.Button("FlywheelSpeed Send")){
            somatoshiftManager.SetFlywheelSpeed (somatoshiftManager.flyWheelSpeed);
        }

        // if (GUILayout.Button("TargetPos Send")){
        //     somatoshiftManager.SetFlywheelSpeed (0f);
        // }
        if (GUILayout.Button("ActivateTorque Send")){
            somatoshiftManager.SetActivateTorque (somatoshiftManager.isActivateTorque);
        }
        if (GUILayout.Button("MotionCombination Send")){
            somatoshiftManager.SetMotionCombination (somatoshiftManager.isMotionCombination);
        }
        if (GUILayout.Button("ResetPosition Send")){
            somatoshiftManager.SetResetPosition (somatoshiftManager.resetPosition);
        } 
    }
}
