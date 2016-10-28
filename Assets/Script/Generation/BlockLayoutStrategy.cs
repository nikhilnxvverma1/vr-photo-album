using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockLayoutStrategy: LayoutStrategy{
	
	public Tile[][] BuildLayout(ImageBank imageBank,double unitWidth){

		List<Room> rooms=this.BuildRooms(imageBank,unitWidth);
		int columnSpan=30;
		Room[][] occupancy=this.FixPositionsForRoom(rooms,columnSpan);
		return MergeRoomsInToSingleTileGrid(occupancy,occupancy.Length,columnSpan);	
	}

	private List<Room> BuildRooms(ImageBank imageBank,double unitWidth){
		List<Room> rooms=new List<Room>();
		while(imageBank.getTotalContainers()>0){
			int imagesForThisRoom=Random.Range(5,10);
			ImageContainer imageList=null;
			if(imagesForThisRoom>imageBank.getTotalContainers()){
				imagesForThisRoom=imageBank.getTotalContainers();
				imageList=imageBank.PopAll();
			}else{
				imageList=imageBank.PopImageContainers(imagesForThisRoom);
			}
			Room room=new Room();
			room.SetImageList(imageList,imagesForThisRoom);
			room.ComputeRoomDimensionsBasisImageList(imageBank,unitWidth);
			rooms.Add(room);
		}
		return rooms;
	}
		
	private Tile[][] MergeRoomsInToSingleTileGrid(Room[][] roomInfo,int totalRows,int totalColumns){
		Tile[][] grid=new Tile[totalRows][];
		for(int i=0;i<totalRows;i++){
			grid[i]=new Tile[totalColumns];
			for(int j=0;j<totalColumns;j++){
				if (roomInfo[i][j]!=null) {
					Room room = roomInfo [i] [j];
					int r = i - room.row;
					int c = j - room.column;
					grid [i] [j] = room.grid [r] [c];
				}
			}
		}
		return grid;
	}

	private Room[][] FixPositionsForRoom(List<Room> roomList,int maxWidth){
		
		//for maxHeight just compute the summation of height of all rooms
		int maxHeight=0;
		foreach (Room room in roomList){
			maxHeight+=room.height;
		}

		//max height is only the upper limit of the layout
		//occupancy holds the information about slots that are occupied
		Room[][] occupancy=new Room[maxHeight][];
		for(int i=0;i<maxHeight;i++){
			occupancy[i]=new Room[maxWidth];
			for(int j=0;j<maxWidth;j++){
				occupancy[i][j]=null;
			}
		}

		HeightLevel start=new HeightLevel(0,0,maxWidth);
		int maxHeightLevel=0;
		foreach(Room room in roomList){
			//try to fit this room in the lowest height level

			HeightLevel lowestFittingHeightLevel=FindHeightLevelForRoom(start,room);
			int columnStart=FindColumnStartOnHeightLevel(room,lowestFittingHeightLevel,occupancy,maxWidth);
			start=FixateRoomOnHeightLevel(room,lowestFittingHeightLevel,columnStart,start);
			int heightReached=room.row+room.height;
			if(heightReached>maxHeightLevel){
				maxHeightLevel=heightReached;
			}
			FillOccupancyWithRoom(occupancy,room);
			room.MakeTileGrid();
//			room.FindAndMakeOpeningsWithAdjacentRooms(occupancy,maxHeight,maxWidth);//not ready yet
		}
		return occupancy;
	}



	/** 
	 * Traverse the height list and find the appropriate height to fit this room,
	 * Keep in mind that this might also turn the room
	*/
	private HeightLevel FindHeightLevelForRoom(HeightLevel start,Room room){		
		HeightLevel t=start;
		int smallestHeight=9999;
		HeightLevel fittingHeightLevel=null;
		HeightLevel biggestHightLevel=null;
		int biggestHeight=0;
		bool roomNeedsToTurnToFit=false;
		while(t!=null){
			if(t.rowHeight<smallestHeight){
				if(t.width>room.width){
					roomNeedsToTurnToFit=false;
					smallestHeight=t.rowHeight;
					fittingHeightLevel=t;
				}else if(t.width>room.height){
					roomNeedsToTurnToFit=true;
					smallestHeight=t.rowHeight;
					fittingHeightLevel=t;
				}
				if(t.rowHeight>=biggestHeight){
					biggestHeight=t.rowHeight;
					biggestHightLevel=t;
				}
			}
			t=t.next;
		}

		if(fittingHeightLevel==null){
			fittingHeightLevel=biggestHightLevel;
		}

		if(roomNeedsToTurnToFit){
			room.Turn();
		}
		return fittingHeightLevel;
	}

	/**
	 * Finds the start column based on the height level and keeping in mind to allow maximum adjacency to
	 * other rooms
	 */
	private int FindColumnStartOnHeightLevel(Room room, HeightLevel heightLevel, Room[][] grid,int maxWidth){
		int columnStart=heightLevel.columnStart;

		while (columnStart + room.width + 1 < maxWidth && grid [heightLevel.rowHeight] [columnStart + room.width + 1] == null) {
			columnStart++;
		}			
			
		//if it didn't touch an adjacent room, 
		if(columnStart-1<0||grid[heightLevel.rowHeight][columnStart-1]==null){
			//go in the other direction
			columnStart=heightLevel.columnStart;

			while(columnStart-1>=0&& grid[heightLevel.rowHeight][columnStart-1]==null){
				columnStart--;
			}		
		}
		return columnStart;
	}

	/**
	 * Sets the position of the room for the specified height level and column on that height level.
	 * Also modifies the HeightLevel list to create new height level and returns the start of the list
	 */
	private HeightLevel FixateRoomOnHeightLevel(Room room,HeightLevel heightLevel,int column,HeightLevel start){
		room.column=column;
		room.row=heightLevel.rowHeight;

		HeightLevel movingBack=heightLevel;
		for(int shift=heightLevel.columnStart;shift>=column;shift--){				
			if((movingBack.previous!=null)&&
				(shift<(movingBack.previous.columnStart+movingBack.previous.width))){
				movingBack=movingBack.previous;
			}
		}

		HeightLevel movingForward=heightLevel;
		for(int shift=heightLevel.columnStart+heightLevel.width;shift<(column+room.width);shift++){
			if((movingForward.next!=null)&&
				(shift>(movingForward.next.columnStart))){
				movingForward=movingForward.next;
			}
		}

		// combine and divide moving backward and moving forward
		movingBack.DivideByIntroducingNewHeight(heightLevel.rowHeight+room.height,room.column,room.width,movingBack);
		return start;	//start will almost never change because we modify the start not change it.
	}



	/** Fills the room grid with the specified room for its position */
	private void FillOccupancyWithRoom(Room[][] grid, Room room){
		for(int i=room.row;i<room.row+room.height;i++){
			for(int j=room.column;j<room.column+room.width;j++){
				try {
					grid [i] [j] = room;
				} catch (System.Exception ex) {
					Debug.Log(ex.ToString());
				}
			}
		}
	}
}

class HeightLevel{
	public int rowHeight;
	public int columnStart;
	public int width;
	public HeightLevel next;
	public HeightLevel previous;

	public HeightLevel(int rowHeight,int columnStart,int width){
		this.rowHeight=rowHeight;
		this.columnStart=columnStart;
		this.width=width;
	}

	public void DivideByIntroducingNewHeight(int newHeight,int newHeightColumn,int newHeightWidth,HeightLevel overlappingEnd){
		//retain the old height and width before overwriting
		int oldHeight=rowHeight;
		int oldWidth=this.width;

		// if new height belongs to this height level
		if(newHeightColumn==this.columnStart){
			//retain this object and increase its height 
			this.rowHeight=newHeight;
			this.width=newHeightWidth;
			if(newHeightWidth<oldWidth){
				if(overlappingEnd==this){//this will always be true in such a case
					//create a new height level and insert it in the next 
					HeightLevel remainingWidthLevel=new HeightLevel(oldHeight,newHeightColumn+newHeightWidth,oldWidth-newHeightWidth);
					remainingWidthLevel.previous=this;
					remainingWidthLevel.next=this.next;
					this.next=remainingWidthLevel;
					if(remainingWidthLevel.next!=null){
						remainingWidthLevel.next.previous=remainingWidthLevel;
					}
				}
			}else if(newHeightWidth>oldWidth){
				if(overlappingEnd!=this){//this assertion will always be true
					//make the overlapping end the next to this
					int amountOverlapped=(newHeightColumn+newHeightWidth)-overlappingEnd.columnStart;
					overlappingEnd.columnStart=newHeightColumn+newHeightWidth;
					overlappingEnd.width=overlappingEnd.width-amountOverlapped;
					overlappingEnd.previous=this;
					this.next=overlappingEnd;
				}
			}

		}else if(newHeightColumn<this.columnStart){
			int oldColumnStart=this.columnStart;
			this.columnStart=newHeightColumn;
			this.width=newHeightWidth;
			HeightLevel elevatedLevel=new HeightLevel(newHeight,newHeightColumn,newHeightWidth);
			elevatedLevel.previous=this;

			if(overlappingEnd==this){
				int amountOverlapped=(newHeightColumn+newHeightWidth)-oldColumnStart;
				HeightLevel remainingWidthLevel=new HeightLevel(oldHeight,newHeightColumn+newHeightWidth,oldWidth-amountOverlapped);
				elevatedLevel.next=remainingWidthLevel;
				remainingWidthLevel.previous=elevatedLevel;
				remainingWidthLevel.next=this.next;
				if(this.next!=null){
					this.next.previous=remainingWidthLevel;
				}
				this.next=elevatedLevel;
			}else{
				int amountOverlapped=(newHeightColumn+newHeightWidth)-overlappingEnd.columnStart;
				overlappingEnd.columnStart=newHeightColumn+newHeightWidth;
				overlappingEnd.width=overlappingEnd.width-amountOverlapped;
				this.next=overlappingEnd;
				overlappingEnd.previous=this;					
			}

		}else{			
			this.width=newHeightColumn-this.columnStart;
			HeightLevel elevatedLevel=new HeightLevel(newHeight,newHeightColumn,newHeightWidth);
			elevatedLevel.previous=this;

			if(overlappingEnd==this){
				elevatedLevel.next=this.next;
				if(this.next!=null){
					this.next.previous=elevatedLevel;
				}

			}else{
				int amountOverlapped=(newHeightColumn+newHeightWidth)-overlappingEnd.columnStart;
				overlappingEnd.columnStart=newHeightColumn+newHeightWidth;
				overlappingEnd.width=overlappingEnd.width-amountOverlapped;								

				elevatedLevel.next=overlappingEnd;
				if(overlappingEnd.next!=null){
					overlappingEnd.next.previous=elevatedLevel;
				}

			}
			this.next=elevatedLevel;
		}

	}
}


