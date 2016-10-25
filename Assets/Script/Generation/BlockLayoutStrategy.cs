using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockLayoutStrategy: LayoutStrategy{
	
	public Room BuildLayout(ImageBank imageBank,double unitWidth){

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
		return null;	
	}
		
}


