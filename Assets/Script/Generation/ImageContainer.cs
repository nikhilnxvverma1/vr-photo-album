using UnityEngine;
using System.Collections;

public class ImageContainer {
	Photo photo;
	double gap;
	double y;
	GameObject photoFrame;
	ImageContainer next;

	ImageContainer(Photo photo){
		this.photo=photo;
	}

	public double getWidth(){
		return photo.width+2*gap;
	}	

}
