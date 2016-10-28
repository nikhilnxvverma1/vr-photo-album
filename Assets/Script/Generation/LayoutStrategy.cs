using UnityEngine;
using System.Collections;

public interface LayoutStrategy {
	Tile[][] BuildLayout(ImageBank imageBank,double unitWidth);
}
