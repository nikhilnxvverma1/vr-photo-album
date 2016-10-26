using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockLayoutStrategy: LayoutStrategy{
	
	public Room BuildLayout(ImageBank imageBank,double unitWidth){

		List<Room> rooms=this.BuildRooms(imageBank,unitWidth);
		this.FixPositionsForRoom(rooms,30);
		return null;	
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

	private void FixPositionsForRoom(List<Room> roomList,int maxWidth){
		
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

		foreach(Room room in roomList){
			//try to fit this room in the lowest height level

			HeightLevel lowestFittingHeightLevel=FindHeightLevelForRoom(start,room);
			int columnStart=FindColumnStartOnHeightLevel(room,lowestFittingHeightLevel,occupancy);
			start=FixateRoomOnHeightLevel(room,lowestFittingHeightLevel,columnStart,start);
			FillOccupancyWithRoom(occupancy,room);
			FindAndMakeOpeningsWithAdjacentRooms(room,occupancy,maxHeight,maxWidth);
		}
	}

	/** 
	 * Traverse the height list and find the appropriate height to fit this room,
	 * Keep in mind that this might also turn the room
	*/
	private HeightLevel FindHeightLevelForRoom(HeightLevel start,Room room){
		HeightLevel t=start;
		while(t!=null){

			t=t.next;
		}
		return null;
	}

	/**
	 * Finds the start column based on the height level and keeping in mind to allow maximum adjacency to
	 * other rooms
	 */
	private int FindColumnStartOnHeightLevel(Room room, HeightLevel heightLevel, Room[][] grid){
		return 0;
	}

	/**
	 * Sets the position of the room for the specified height level and column on that height level.
	 * Also modifies the HeightLevel list to create new height level and returns the start of the list
	 */
	private HeightLevel FixateRoomOnHeightLevel(Room room,HeightLevel heightLevel,int column,HeightLevel start){
		return null;	
	}

	/** Finds all rooms adjacent to the room in the room grid and creates opening between them**/
	private int FindAndMakeOpeningsWithAdjacentRooms(Room room,Room[][] grid,int rows,int columns){
		return 0;
	}

	/** Fills the room grid with the specified room for its position */
	private void FillOccupancyWithRoom(Room[][] grid, Room room){
		for(int i=room.row;i<room.height;i++){
			for(int j=room.column;j<room.width;j++){
				grid[i][j]=room;
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

}


