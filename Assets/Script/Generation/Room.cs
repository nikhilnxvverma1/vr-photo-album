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

	/** Finds all rooms adjacent to the room in the room grid and creates opening between them**/
	public int FindAndMakeOpeningsWithAdjacentRooms(Room[][] grid,int totalRows,int totalColumns){
		int totalAdjacentRooms=0;
		//top
		if(this.row+this.height+1<totalRows){
			int top=this.row+this.height;
			int start=0;
			int end=0;
			Room currentRoom=null;
			for(int i=this.column;i<this.column+this.width;i++){
				if(grid[top][i]!=currentRoom){
					if(currentRoom!=null){
						// make door
						Debug.Log("Door with Top");
						int mid=(start+end)/2;
						int r = 0;
						int c = mid-currentRoom.column;
						currentRoom.grid [r] [c].floor.type = FloorType.Blank;						
						this.grid[this.height-1][mid-this.column].floor.type=FloorType.Blank;
						totalAdjacentRooms++;
					}
					currentRoom=grid[top][i];
					start=i;				
					end=i;
				}else{
					end=i;
				}
			}
		}

		//right
		if(this.column+this.width+1<totalColumns){
			int right=this.column+this.width;
			int start=0;
			int end=0;
			Room currentRoom=null;
			for(int i=this.row;i<this.row+this.height;i++){
				if(grid[i][right]!=currentRoom){
					if(currentRoom!=null){
						// make door
						Debug.Log("Door with Right");
						int mid=(start+end)/2;
						int r = mid-currentRoom.row;
						int c = 0;
						currentRoom.grid [r] [c].floor.type = FloorType.Blank;						
						this.grid[mid-this.row][this.width-1].floor.type=FloorType.Blank;
						totalAdjacentRooms++;
					}
					currentRoom=grid[i][right];
					start=i;				
					end=i;
				}else{
					end=i;
				}
			}
		}

		//bottom
		if(this.row-1>=0){
			int bottom=this.row-1;
			int start=0;
			int end=0;
			Room currentRoom=null;
			for(int i=this.column;i<this.column+this.width;i++){
				if(grid[bottom][i]!=currentRoom){
					if(currentRoom!=null){
						// make door
						Debug.Log("Door with Bottom");
						int mid=(start+end)/2;
						int r = currentRoom.height-1;
						int c = mid-currentRoom.column;
						currentRoom.grid [r] [c].floor.type = FloorType.Blank;						
						this.grid[0][mid-this.column].floor.type=FloorType.Blank;
						totalAdjacentRooms++;
					}
					currentRoom=grid[bottom][i];
					start=i;				
					end=i;
				}else{
					end=i;
				}
			}
		}

		//left
		if(this.column-1>=0){
			int left=this.column-1;
			int start=0;
			int end=0;
			Room currentRoom=null;
			for(int i=this.row;i<this.row+this.height;i++){
				if(grid[i][left]!=currentRoom){
					if(currentRoom!=null){
						// make door
						Debug.Log("Door with Left");
						int mid=(start+end)/2;
						int r = mid-currentRoom.row;
						int c = currentRoom.width-1;
						currentRoom.grid [r] [c].floor.type = FloorType.Blank;						
						this.grid[mid-this.row][0].floor.type=FloorType.Blank;
						totalAdjacentRooms++;
					}
					currentRoom=grid[i][left];
					start=i;				
					end=i;
				}else{
					end=i;
				}
			}
		}

		return totalAdjacentRooms;
	}
}