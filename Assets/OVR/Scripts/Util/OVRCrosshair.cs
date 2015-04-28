/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;

/// <summary>
/// OVRCrosshair is a component that adds a stereoscoppic cross-hair into a scene.
/// </summary>
public class OVRCrosshair
{
	#region Variables
	public Texture ImageCrosshair 	  = null;
	
	public OVRCameraRig CameraController = null;
	public OVRPlayerController PlayerController = null;
	
	public float   FadeTime			  = 0.3f;
	public float   FadeScale      	  = 0.6f;
	public float   CrosshairDistance  = 1.0f;

	
	private bool   DisplayCrosshair;
	private bool   CollisionWithGeometry;
	private float  FadeVal;
	private Transform UIAnchor;
	
	private float  XL 				  = 0.0f;
	private float  YL 				  = 0.0f;
	
	private float  ScreenWidth		  = 1280.0f;
	private float  ScreenHeight 	  =  800.0f;
	
	#endregion
	
	#region Public Functions
	
	/// <summary>
	/// Sets the crosshair texture.
	/// </summary>
	/// <param name="image">Image.</param>
	public void SetCrosshairTexture(ref Texture image)
	{
		ImageCrosshair = image;
	}
	
	/// <summary>
	/// Sets the OVR camera controller.
	/// </summary>
	/// <param name="cameraController">Camera controller.</param>
	public void SetOVRCameraController(ref OVRCameraRig cameraController)
	{
		CameraController = cameraController;
		UIAnchor = CameraController.centerEyeAnchor;
	}
	
	/// <summary>
	/// Sets the OVR player controller.
	/// </summary>
	/// <param name="playerController">Player controller.</param>
	public void SetOVRPlayerController(ref OVRPlayerController playerController)
	{
		PlayerController = playerController;
	}
	
	/// <summary>
	/// Determines whether the crosshair is visible.
	/// </summary>
	/// <returns><c>true</c> if this instance is crosshair visible; otherwise, <c>false</c>.</returns>
	public bool IsCrosshairVisible()
	{
		if(FadeVal > 0.0f)
			return true;
		
		return false;
	}
	
	/// <summary>
	/// Init this instance.
	/// </summary>
	public void Init()
	{
		DisplayCrosshair 		= true;
		CollisionWithGeometry 	= false;
		FadeVal 		 		= 0.0f;
		
		ScreenWidth  = Screen.width;
		ScreenHeight = Screen.height;
		
		// Initialize screen location of cursor
		XL = ScreenWidth * 0.5f;
		YL = ScreenHeight * 0.5f;
	}
	
	/// <summary>
	/// Updates the crosshair.
	/// </summary>
	public void UpdateCrosshair()
	{
		if (ShouldDisplayCrosshair())
		{
			// Do not do these tests within OnGUI since they will be called twice
			CollisionWithGeometryCheck();
		}
	}
	
	/// <summary>
	/// The GUI crosshair event.
	/// </summary>
	public void OnGUICrosshair()
	{
		if ((DisplayCrosshair == true) && (CollisionWithGeometry == false))
			FadeVal += Time.deltaTime / FadeTime;
		else
			FadeVal -= Time.deltaTime / FadeTime;
		
		FadeVal = Mathf.Clamp(FadeVal, 0.0f, 1.0f);
		
		// Check to see if crosshair influences mouse rotation

		bool skipMouseRotation = true;

		if(PlayerController != null)
			PlayerController.GetSkipMouseRotation(ref skipMouseRotation);
		
	
		GUI.DrawTexture(new Rect(	XL - (ImageCrosshair.width * 0.5f),
		                         YL - (ImageCrosshair.height * 0.5f), 
		                         ImageCrosshair.width,
		                         ImageCrosshair.height), 
		                ImageCrosshair);
	
		}
	#endregion
	
	#region Private Functions
	/// <summary>
	/// Shoulds the crosshair be displayed.
	/// </summary>
	/// <returns><c>true</c>, if display crosshair was shoulded, <c>false</c> otherwise.</returns>
	bool ShouldDisplayCrosshair()
	{	
		
		DisplayCrosshair = true;
		
		// Always initialize screen location of cursor to center
		XL = ScreenWidth * 0.5f + 30;
		YL = ScreenHeight * 0.5f;
	
		return DisplayCrosshair;
	}
	
	/// <summary>
	/// Do a collision raycast on geometry for crosshair.
	/// </summary>
	/// <returns><c>true</c>, if with geometry check was collisioned, <c>false</c> otherwise.</returns>
	bool CollisionWithGeometryCheck()
	{
		CollisionWithGeometry = false;
		
		Vector3 startPos = UIAnchor.position;
		Vector3 dir = Vector3.forward;
		dir = UIAnchor.rotation * dir;
		dir *= CrosshairDistance;
		Vector3 endPos = startPos + dir;
		
		RaycastHit hit;
		if (Physics.Linecast(startPos, endPos, out hit))
		{
			if (!hit.collider.isTrigger)
			{
				CollisionWithGeometry = true;
			}
		}
		
		return CollisionWithGeometry;
	}
	#endregion
}
