using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Example_gyro : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
		Enum.GetValues( typeof( Joycon.Button ) ) as Joycon.Button[];

	private List<Joycon>    m_joycons;
	private Joycon          m_joyconL;
	private Joycon          m_joyconR;
	private Joycon.Button?  m_pressedButtonL;
	private Joycon.Button?  m_pressedButtonR;


	//以下が追記のコード
	private GameObject rCube,lCube;
	private Quaternion rciq,lciq,riq,liq;
	private Animator anim;
	private Transform RS_bone,LS_bone;

	private void Start()
	{

		m_joycons = JoyconManager.Instance.j;

		if ( m_joycons == null || m_joycons.Count <= 0 ) return;

		m_joyconL = m_joycons.Find( c =>  c.isLeft );
		m_joyconR = m_joycons.Find( c => !c.isLeft );

		// 以下が追記のコード
		anim = (Animator)FindObjectOfType (typeof(Animator));

		rCube = GameObject.Find ("RightCube");
		lCube = GameObject.Find ("LeftCube");

		RS_bone = anim.GetBoneTransform (HumanBodyBones.RightUpperArm);
		LS_bone = anim.GetBoneTransform (HumanBodyBones.LeftUpperArm);

		rciq = rCube.transform.rotation;
		lciq = lCube.transform.rotation;
		riq = RS_bone.rotation;
		liq = LS_bone.rotation;
	}

	private void Update()
	{

		// 以下が追記のコード
		const float MOVE_PER_CLOCK = 0.01f;
		Vector3 joyconGyro;
		// 右の箱
		joyconGyro = m_joycons[0].GetGyro();
		Quaternion rcqt = rCube.transform.rotation;
		rcqt.x += -joyconGyro[1] * MOVE_PER_CLOCK;
		rcqt.y += -joyconGyro[0] * MOVE_PER_CLOCK;
		rcqt.z += -joyconGyro[2] * MOVE_PER_CLOCK;
		rCube.transform.rotation = rcqt;

		// 右肩
		Quaternion rb = RS_bone.transform.rotation * Quaternion.Inverse(riq);
		rb.x += -joyconGyro [1] * MOVE_PER_CLOCK;
		rb.y += -joyconGyro [0] * MOVE_PER_CLOCK;
		rb.z += -joyconGyro [2] * MOVE_PER_CLOCK;
		RS_bone.rotation = rb * riq;

		// 右のジョイコンのAボタンが押されたら、右の箱と右肩は初期ポジションに戻る
		if (m_joyconR.GetButtonDown(m_buttons[1])) {
			rCube.transform.rotation = rciq;
			RS_bone.rotation = riq;
		}

		// 左の箱
		joyconGyro = m_joycons[1].GetGyro();
		Quaternion lcqt = lCube.transform.rotation;
		lcqt.x += -joyconGyro[1] * MOVE_PER_CLOCK;
		lcqt.y += -joyconGyro[0] * MOVE_PER_CLOCK;
		lcqt.z += -joyconGyro[2] * MOVE_PER_CLOCK;
		lCube.transform.rotation = lcqt;

		// 左肩
		Quaternion lb = LS_bone.transform.rotation * Quaternion.Inverse(liq);
		lb.x += -joyconGyro [1] * MOVE_PER_CLOCK;
		lb.y += -joyconGyro [0] * MOVE_PER_CLOCK;
		lb.z += -joyconGyro [2] * MOVE_PER_CLOCK;
		LS_bone.rotation = lb * liq;

		// 左のジョイコンのAボタンが押されたら、左の箱と左肩は初期ポジションに戻る
		if (m_joyconL.GetButtonDown(m_buttons[1])) {
			lCube.transform.rotation = lciq;
			LS_bone.rotation = liq;
		}

	}

    //TRUC VISUEL PAS BESOIN

	private void OnGUI()
	{
		var style = GUI.skin.GetStyle( "label" );
		style.fontSize = 24;

		if ( m_joycons == null || m_joycons.Count <= 0 )
		{
			GUILayout.Label( "Joy-Con が接続されていません" );
			return;
		}

		if ( !m_joycons.Any( c => c.isLeft ) )
		{
			GUILayout.Label( "Joy-Con (L) が接続されていません" );
			return;
		}

		if ( !m_joycons.Any( c => !c.isLeft ) )
		{
			GUILayout.Label( "Joy-Con (R) が接続されていません" );
			return;
		}

		GUILayout.BeginHorizontal( GUILayout.Width( 960 ) );

		foreach ( var joycon in m_joycons )
		{
			var isLeft      = joycon.isLeft;
			var name        = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
			var key         = isLeft ? "Z キー" : "X キー";
			var button      = isLeft ? m_pressedButtonL : m_pressedButtonR;
			var stick       = joycon.GetStick();
			var gyro        = joycon.GetGyro();
			var accel       = joycon.GetAccel();
			var orientation = joycon.GetVector();

//			GUILayout.BeginVertical( GUILayout.Width( 480 ) );
//			GUILayout.Label( name );
//			GUILayout.Label( key + "：振動" );
//			GUILayout.Label( "押されているボタン：" + button );
//			GUILayout.Label( string.Format( "スティック：({0}, {1})", stick[ 0 ], stick[ 1 ] ) );
//			GUILayout.Label( "ジャイロ：" + gyro );
//			GUILayout.Label( "加速度：" + accel );
//			GUILayout.Label( "傾き：" + orientation );
//			GUILayout.EndVertical();
		}

		GUILayout.EndHorizontal();
	}
}
