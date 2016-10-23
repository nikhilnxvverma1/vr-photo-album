using UnityEngine;
using System.Collections;

public class ImageContainer {
	Photo photo;
	double normalizedWidth;
	double normalizedHeight;
	double gap;
	double y;
	GameObject photoFrame;
	ImageContainer next=null;

	ImageContainer(Photo photo,double normalizedWidth,double normalizedHeight){
		this.photo=photo;
		this.normalizedWidth=normalizedWidth;
		this.normalizedHeight=normalizedHeight;
	}

	public double getWidth(){
		return photo.width+2*gap;
	}	

	public void setNext(ImageContainer imageContainer){
		next=imageContainer;
	}

}
