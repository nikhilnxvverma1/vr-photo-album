using UnityEngine;
using System.Collections;

public class ImageBank {
	private double perimeter;
	private ImageContainer unusedStart=null;
	private ImageContainer usedStart=null;
	private double maxHeight;
	private double maxWidth;
	private double largestHeightOfAPhoto;
	private double largestWidthOfAPhoto;

	public ImageBank(Album album,double maxWidth,double maxHeight){

		this.maxHeight=maxHeight;
		this.maxWidth=maxWidth;
		this.ComputeLargestDimensions(album);
		this.unusedStart=MakeImageContainerListAndComputePerimeter(album);
	}

	private void ComputeLargestDimensions(Album album){
		this.largestHeightOfAPhoto=0;
		this.largestWidthOfAPhoto=0;
		for (int i=0;i<album.photoList.Length;i++){
			Photo photo=album.photoList[i];
			if(photo.width>largestWidthOfAPhoto){
				largestWidthOfAPhoto=photo.width;
			}
			if(photo.height>largestHeightOfAPhoto){
				largestHeightOfAPhoto=photo.height;
			}
		}
	}

	private ImageContainer MakeImageContainerListAndComputePerimeter(Album album){
		ImageContainer start=null;
		ImageContainer last=null;
		this.perimeter=0;
		for( int i=0;i<album.photoList.Length;i++){
			Photo photo=album.photoList[i];
			double normalizedWidth=(photo.width/this.largestWidthOfAPhoto)*maxWidth;
			double normalizedHeight=(photo.width/this.largestHeightOfAPhoto)*maxHeight;
			ImageContainer imageContainer=new ImageContainer(photo,normalizedWidth,normalizedHeight);

			if(last==null){
				start=imageContainer;
				last=imageContainer;
			}else{
				last.setNext(imageContainer);
			}

			this.perimeter+=imageContainer.getWidth();
		}
		return start;
	}

	public double getPerimeter(){
		return perimeter;
	}


}
