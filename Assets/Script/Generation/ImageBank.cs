using UnityEngine;
using System.Collections;

public class ImageBank {
	private double perimeter;
	private ImageContainer top=null;
	private int totalContainers=0;
	private double maxHeight;
	private double maxWidth;
	private double largestHeightOfAPhoto;
	private double largestWidthOfAPhoto;

	public ImageBank(Album album,double maxWidth,double maxHeight){

		this.maxHeight=maxHeight;
		this.maxWidth=maxWidth;
		this.ComputeLargestDimensions(album);
		this.totalContainers=MakeImageContainerListAndComputePerimeter(album);
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

	private int MakeImageContainerListAndComputePerimeter(Album album){
		int total=0;
		ImageContainer top=null;
		ImageContainer last=null;
		this.perimeter=0;
		for( int i=0;i<album.photoList.Length;i++){
			Photo photo=album.photoList[i];
			double normalizedWidth=(photo.width/this.largestWidthOfAPhoto)*maxWidth;
			double normalizedHeight=(photo.width/this.largestHeightOfAPhoto)*maxHeight;
			ImageContainer imageContainer=new ImageContainer(photo,normalizedWidth,normalizedHeight);

			if(last==null){
				top=imageContainer;
				last=imageContainer;
			}else{
				last.setNext(imageContainer);
			}

			this.perimeter+=imageContainer.getWidth();
		}
		return total;
	}

	public double getPerimeter(){
		return perimeter;
	}

	public int getTotalContainers(){
		return totalContainers;
	}

	public ImageContainer PopImageContainers(int n){
		ImageContainer t=top;
		ImageContainer popStart=null;
		ImageContainer lastPopped=null;
		int i=0;
		while(t!=null && i++ < n){
			if(lastPopped==null){
				popStart=t;
				lastPopped=t;
			}
			//they are already linked so ne need to link them again
			//just move the top down
			top=top.getNext();
			//reduce the total perimeter in the image bank
			this.perimeter-=t.getWidth();
			this.totalContainers--;
			t=t.getNext();
		}
		return popStart;
	}
}
