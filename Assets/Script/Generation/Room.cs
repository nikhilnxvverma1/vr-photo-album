using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {

	private static int LargestWidth=10;
	private static int LargestHeight=10;

	//position
	public int row ;
	public int column ;

	//dimension
	public int width ;
	public int height ;

	//tilemap for this room
	public Tile [][] grid ;

	//image containers contained in this room
	private ImageContainer imageList;
	private int countOfImages;

	public void SetImageList(ImageContainer imageList,int countOfImages){
		this.imageList=imageList;
		this.countOfImages=countOfImages;
	}

	public ImageContainer GetImageList(){
		return imageList;
	}

	public void ComputeRoomDimensionsBasisImageList(ImageBank imageBank,double unitWidth){

		//Compute the total perimeter needed
		ImageContainer t=imageList;
		double totalPerimeterNeeded=0;
		double largestImageContainerWidth=0;
		while(t!=null){
			totalPerimeterNeeded+=t.getWidth();
			if(t.getWidth()>largestImageContainerWidth){
				largestImageContainerWidth=t.getWidth();	
			}
			t=t.getNext();
		}


		// 3 units will be reserved for the opening
		if(largestImageContainerWidth<3*unitWidth){
			largestImageContainerWidth=3*unitWidth;
		}
		int p=Units(totalPerimeterNeeded+3*unitWidth,unitWidth);
		int l=Units(largestImageContainerWidth,unitWidth);

		bool randomizeWidth=(Random.value>0.5);
		if(randomizeWidth){
			width=Random.Range(l,LargestWidth+1);
			height=(p-2*width)/2+ Random.Range(3,5);
		}else{
			height=Random.Range(l,LargestWidth+1);
			width=(p-2*height)/2+ Random.Range(3,5);
		}
	}

	private int Units(double width,double unitWidth){
		return Mathf.CeilToInt((float)(width/unitWidth));
	}
}