﻿using UnityEngine;
using System.Collections;

public class GetCurrentColor : MonoBehaviour {

	public LayerMask targetBackgroundLayer;

	Vector3 screenPoint;
	Color targetColor;


	// Use this for initialization
	void Start () {
		targetColor = new Color ();
	}
	
	// Update is called once per frame
	void Update () {
		

		Debug.DrawRay (transform.position, Vector3.forward);
		RaycastHit hit = new RaycastHit ();
		if (Physics.Raycast (transform.position, Vector3.forward, out hit, 100f, targetBackgroundLayer)) {
			if (hit.collider != null) {
				if (GetSpritePixelColorUnderMyTransform (hit.collider.gameObject.GetComponent<SpriteRenderer> (), out targetColor)) {
					Debug.Log ("Color is " + targetColor.r + " " + targetColor.g + " " + targetColor.b + " ");
				}
				Renderer rend = hit.transform.GetComponent<Renderer>();
				MeshCollider meshCollider = hit.collider as MeshCollider;
				if (rend == null || rend.material == null || rend.material.mainTexture == null || meshCollider == null) {
					if (rend == null) {
						Debug.Log ("renderer is null!");
					}
					if (rend.material == null) {
						Debug.Log ("render.material is null!");
					}
					if (rend.material.mainTexture == null) {
						Debug.Log ("render.material.mainTexture is null!");
					}
					if (meshCollider == null) {
						Debug.Log ("meshCollider is null!");
					}
					return;
				}
				RenderTexture rt = rend.material.mainTexture as RenderTexture;
				if (rt == null) {
					Debug.Log ("Not a render texture!");
					return;
				}
				RenderTexture.active = rt;
				Texture2D tex = new Texture2D (1, 1, TextureFormat.RGB24, false);
				Vector2 pixelUV = hit.textureCoord;
				pixelUV.x *= rt.width;
				pixelUV.y *= rt.height;
				tex.ReadPixels(new Rect((int)pixelUV.x, (int)pixelUV.y,1,1),0,0);
				Color pixelColor = tex.GetPixel(0,0);
				Debug.Log("Hit pixel : " + pixelUV.x + " : " + pixelUV.y + "\n" + pixelColor.r + " " + pixelColor.g + " " + pixelColor.g + " " );
			} 
		}else {
			Debug.Log ("Hit nothing!");
		}
	}

	public bool GetSpritePixelColorUnderMyTransform(SpriteRenderer spriteRenderer, out Color color) {
		color = new Color();
		Camera cam = Camera.main;
		Vector2 mousePos = Input.mousePosition;
		Vector2 viewportPos = cam.ScreenToViewportPoint(cam.WorldToScreenPoint(transform.position));
		if(viewportPos.x < 0.0f || viewportPos.x > 1.0f || viewportPos.y < 0.0f || viewportPos.y > 1.0f) return false; // out of viewport bounds
		// Cast a ray from viewport point into world
		Ray ray = cam.ViewportPointToRay(viewportPos);

		// Check for intersection with sprite and get the color
		return IntersectsSprite(spriteRenderer, ray, out color);
	}
	private bool IntersectsSprite(SpriteRenderer spriteRenderer, Ray ray, out Color color) {
		color = new Color();
		if(spriteRenderer == null) return false;
		Sprite sprite = spriteRenderer.sprite;
		if(sprite == null) return false;
		Texture2D texture = sprite.texture;
		if(texture == null) return false;
		// Check atlas packing mode
		if(sprite.packed && sprite.packingMode == SpritePackingMode.Tight) {
			// Cannot use textureRect on tightly packed sprites
			Debug.LogError("SpritePackingMode.Tight atlas packing is not supported!");
			// TODO: support tightly packed sprites
			return false;
		}
		// Craete a plane so it has the same orientation as the sprite transform
		Plane plane = new Plane(transform.forward, transform.position);
		// Intersect the ray and the plane
		float rayIntersectDist; // the distance from the ray origin to the intersection point
		if(!plane.Raycast(ray, out rayIntersectDist)) return false; // no intersection
		// Convert world position to sprite position
		// worldToLocalMatrix.MultiplyPoint3x4 returns a value from based on the texture dimensions (+/- half texDimension / pixelsPerUnit) )
		// 0, 0 corresponds to the center of the TEXTURE ITSELF, not the center of the trimmed sprite textureRect
		Vector3 spritePos = spriteRenderer.worldToLocalMatrix.MultiplyPoint3x4(ray.origin + (ray.direction * rayIntersectDist));
		Rect textureRect = sprite.textureRect;
		float pixelsPerUnit = sprite.pixelsPerUnit;
		float halfRealTexWidth = texture.width * 0.5f; // use the real texture width here because center is based on this -- probably won't work right for atlases
		float halfRealTexHeight = texture.height * 0.5f;
		// Convert to pixel position, offsetting so 0,0 is in lower left instead of center
		int texPosX = (int)(spritePos.x * pixelsPerUnit + halfRealTexWidth);
		int texPosY = (int)(spritePos.y * pixelsPerUnit + halfRealTexHeight);
		// Check if pixel is within texture
		if(texPosX < 0 || texPosX < textureRect.x || texPosX >= Mathf.FloorToInt(textureRect.xMax)) return false; // out of bounds
		if(texPosY < 0 || texPosY < textureRect.y || texPosY >= Mathf.FloorToInt(textureRect.yMax)) return false; // out of bounds
		// Get pixel color
		color = texture.GetPixel(texPosX, texPosY);
		return true;
	}
}
