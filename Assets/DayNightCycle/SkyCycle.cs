// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;
using TMPro;
public class SkyCycle : MonoBehaviour {


/*DAY/NIGHT CYCLE SCRIPT
WRITTEN BY SAM BOYER
(so basically, please don't steal it :D)*/


//the secondsPerMinute changes the length of a minute. A lower value 
public float secondsPerMinute = 0.625f; 

//starting time in hours, use decimal points for minutes
public float startTime = 12.0f; 

//show date/time information?
public bool  showGUI = false;

//this variable is for the position of the area in degrees from the equator, therfore it must stay between 0 and 90.
//It determines now high the sun rises throughout the day, but not the length of the day yet.
public float latitudeAngle = 45.0f;

//The transform component of the empty that tilts the sun's roataion.(the SunTilt object, not the Sun object itself)
public Transform sunTilt;


private float day;
private float min;
private float smoothMin;

private float texOffset;
private Material skyMat;
private Transform sunOrbit;

 public static float headX;
 public static float headY;
  public static float headYY;
  public TMP_Text dayText;
void  Start (){
	skyMat = GetComponent<Renderer>().sharedMaterial;
	sunOrbit = sunTilt.GetChild(0);

    headX = sunTilt.eulerAngles.x;    
	headX = Mathf.Clamp(latitudeAngle,0,90); //set the sun tilt

	if(secondsPerMinute == 0){
		Debug.Log("Error! Can't have a time of zero, changed to 0.01f instead.");
		secondsPerMinute = 0.01f;
	}

}

void  UpdateSky (){
	smoothMin = (Time.time/secondsPerMinute) + (startTime*60);
	day = Mathf.Floor(smoothMin/1440)+1;

	smoothMin = smoothMin - (Mathf.Floor(smoothMin/1440)*1440); //clamp smoothMin between 0-1440
	min = Mathf.Round(smoothMin);

    headY = sunOrbit.localEulerAngles.y;
	headYY = smoothMin/4;
	headY = headYY;
	texOffset = Mathf.Cos((((smoothMin)/1440)*2)*Mathf.PI)*0.25f+0.25f;
	skyMat.mainTextureOffset = new Vector2(Mathf.Round((texOffset-(Mathf.Floor(texOffset/360)*360))*1000)/1000,0);
}

void  Update (){
	UpdateSky();
}

void  OnGUI (){
	if(showGUI){
			dayText.text = "Day" + day.ToString();
		// GUI.Label( new Rect(10,0,100,20),"Day "+day.ToString());
		// GUI.Label( new Rect(10,20,100,40),digitalDisplay(Mathf.Floor(min/60).ToString()), " " digitalDisplay((min-Mathf.Floor(min/60)*60).ToString()));
	}
	// GUI.Label( new Rect(10,40,100,60),texOffset.ToString()); //texture offset
}

void  digitalDisplay (string num){ //converts a number into a digital display (adds a zero if it's a single figure)
	if(num.Length==2){
		num = num.ToString();
	}else{
		num = "0";
	}
}
}


// using UnityEngine;
// using System.Collections;

// public class SkyCycle : MonoBehaviour {


// /*DAY/NIGHT CYCLE SCRIPT
// WRITTEN BY SAM BOYER
// (so basically, please don't steal it :D)*/


// //the secondsPerMinute changes the length of a minute. A lower value 
// public float secondsPerMinute = 0.625f; 

// //starting time in hours, use decimal points for minutes
// public float startTime = 12.0f; 

// //show date/time information?
// public bool  showGUI = false;

// //this variable is for the position of the area in degrees from the equator, therfore it must stay between 0 and 90.
// //It determines now high the sun rises throughout the day, but not the length of the day yet.
// public float latitudeAngle = 45.0f;

// //The transform component of the empty that tilts the sun's roataion.(the SunTilt object, not the Sun object itself)
// public Transform sunTilt;


// private float day;
// private float min;
// private float smoothMin;

// private float texOffset;
// private Material skyMat;
// private Transform sunOrbit;
// private   int angleX= 0;
// void  Start (){
// 	// skyMat = GetComponent<Renderer>().sharedMaterial;
// 	// sunOrbit = sunTilt.GetChild(0);
   
//         angleX = Mathf.Clamp(angleX,0,90);
//         sunOrbit.eulerAngles.x = angleX;
// 	//set the sun tilt

// 	if(secondsPerMinute==0){
// 		Debug.LogError("Error! Can't have a time of zero, changed to 0.01f instead.");
// 		secondsPerMinute = 0.01f;
// 	}
// }

// void  UpdateSky (){
// 	smoothMin = (Time.time/secondsPerMinute) + (startTime*60);
// 	day = Mathf.Floor(smoothMin/1440)+1;

// 	smoothMin = smoothMin - (Mathf.Floor(smoothMin/1440)*1440); //clamp smoothMin between 0-1440
// 	min = Mathf.Round(smoothMin);

// 	sunOrbit.localEulerAngles.y = smoothMin/4;
// 	texOffset = Mathf.Cos((((smoothMin)/1440)*2)*Mathf.PI)*0.25f+0.25f;
//     var data = (Mathf.Floor(texOffset/360)*360);
//     var cal = ((texOffset-data)*1000)/1000;
//     var cal2 = Mathf.Round(cal,0);
// 	skyMat.mainTextureOffset = Vector3(0);
// }

// void  Update (){
// 	UpdateSky();
// }
// }
// // // void  OnGUI (){
// // // 	if(showGUI){
// // // 		GUI.Label( new Rect(10,0,100,20),"Day "+day.ToString());
// // // 		GUI.Label( new Rect(10,20,100,40),digitalDisplay(Mathf.Floor(min/60).ToString()) ":" digitalDisplay((min-Mathf.Floor(min/60)*60).ToString()));
// // // 	}
// // // 	//GUI.Label( new Rect(10,40,100,60),texOffset.ToString()); //texture offset
// // // }

// // // void  digitalDisplay (string num  ){ //converts a number into a digital display (adds a zero if it's a single figure)
// // // 	if(num.Length==2){
// // // 		return $"num {num}";
// // // 	}else{
// // // 		return "0";
// // // 	}
// // // }
// // }