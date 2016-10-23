using UnityEngine;
using System.Collections;

public interface LayoutStrategy {
	Room BuildLayout(ImageBank imageBank,double unitWidth);
}
