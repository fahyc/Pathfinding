To Run: open the Assets/AStar/AStar.unity scene 

Directions are on the UI. Camera controls differ between the different pathfinding maps. 

When switching types of pathfinding or switching between maps on the non-tilebased map, there is a significant (~20 seconds) load time. Please be patient!

Manhattan distance only works on tilebased maps. Manhattan does not give great results unless the heuristic weight is less than 1. Euclidean distance seems to work most efficiently with a high heuristic weight. 