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
			if(height<3){
				height=3;
			}
		}else{
			height=Random.Range(l,LargestWidth+1);
			width=(p-2*height)/2+ Random.Range(3,5);
			if(width<3){
				width=3;
			}
		}

		if(width<3 || height< 3){
			Debug.Log("wrong dimension");
		}
	}

	private int Units(double width,double unitWidth){
		return Mathf.CeilToInt((float)(width/unitWidth));
	}

	/** Turns the room on its side, by essentially swapping the width and the height */
	public void Turn(){
		int t=width;
		width=height;
		height=t;
	}

	/** Creates a tile grid for this room assuming the dimensions are already set */
	public void MakeTileGrid(){
		grid=new Tile[height][];
		for(int i=0;i<height;i++){
			try {
				grid [i] = new Tile[width];
			} catch (System.Exception ex) {
				Debug.Log(" ex "+ex.ToString());
			}
			for(int j=0;j<width;j++){
				if(i==0){
					if(j==0){
						grid[i][j]=new Tile(
							new Floor(FloorType.Corner,Direction.East),
							null,
							new Ceiling(CeilingType.Blank));
					}else if(j==(width-1)){
						grid[i][j]=new Tile(
							new Floor(FloorType.Corner,Direction.North),
							null,
							new Ceiling(CeilingType.Blank));
					}else{
						grid[i][j]=new Tile(
							new Floor(FloorType.Wall,Direction.North),
							null,
							new Ceiling(CeilingType.Blank));
					}
				}else if(i==(height-1)){
					if(j==0){
						grid[i][j]=new Tile(
							new Floor(FloorType.Corner,Direction.South),
							null,
							new Ceiling(CeilingType.Blank));
					}else if(j==(width-1)){
						grid[i][j]=new Tile(
							new Floor(FloorType.Corner,Direction.West),
							null,
							new Ceiling(CeilingType.Blank));
					}else{
						grid[i][j]=new Tile(
							new Floor(FloorType.Wall,Direction.South),
							null,
							new Ceiling(CeilingType.Blank));
					}
				}else{
					if(j==0){ 
						grid[i][j]=new Tile(
							new Floor(FloorType.Wall,Direction.East),
							null,
							new Ceiling(CeilingType.Blank));
					}else if(j==(width-1)){
						grid[i][j]=new Tile(
							new Floor(FloorType.Wall,Direction.West),
							null,
							new Ceiling(CeilingType.Blank));
					}else{
						grid[i][j]=new Tile(
							new Floor(FloorType.Blank),
							null,
							new Ceiling(CeilingType.Blank));
					}
				}
			}
		}
	}
}