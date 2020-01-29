# Finding-Paths-AR-Visualization

This project uses Unity AR foundation.
The aim is to visualize various finding paths algorithms (BFS, DFS, Dijkstra,A-Star) in real time at various speeds (Time/step).
[Watch here](https://youtu.be/gPgH5PUekCg)


### Features 

```
1. Control the size of the grid (4x4,8x8, 16x16, 32x32)
2. Choose the finding path algorithm (BFS, DFS, Dijkstra,A-Star)
3. Randomise obstacles
4. Tap on a specific tile to generate an obstalce
5. Control the speed at which the algorithm is visualized
6. Control the scale of the gride (Size x1, Size x2, Size x3, Size x4)

```
### 2D top down view visulisation of the BFS algorithm 

![2D top down view](https://github.com/mohGhazala96/Finding-Paths-AR-Visualization/blob/master/2d-top-down-view-bfs.gif)
### 2D top down view visulisation of the A-star algorithm 
![2D top down view](https://github.com/mohGhazala96/Finding-Paths-AR-Visualization/blob/master/2d-top-down-view-a-star.gif)

#### At any current node the neigbouring cells are added in the neigbouring cells this ordered:
* Left
* Left Down
* Left Up
* Right
* Right Down
* Right Up
* Down
* Up
