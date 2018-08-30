using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAvatar : MonoBehaviour {
    public RawImage avatar;
    public GameObject pictureDepend;

	void Start () {
		
	}
    private void PickImage(int maxSize) {
    NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
    {
        Debug.Log("Image path: " + path);
        if(path != null) {
            // Create Texture from selected image
            Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
            if(texture == null) {
                Debug.Log("Couldn't load texture from " + path);
                return;
            }
            avatar.texture = texture;
            if (pictureDepend) 
                pictureDepend.GetComponent<RawImage>().texture = texture;
                
        }
    }, "Select a PNG image", "image/png", maxSize);

    Debug.Log("Permission result: " + permission);
}
    public void Clicked() {
        PickImage(512);
    }
}
