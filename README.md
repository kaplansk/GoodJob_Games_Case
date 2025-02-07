# GoodJob_Games_Case


![Oyun Demo GIF'i](gif2.gif)



This project is a tile-matching game featuring a collapse/blast mechanic. Players can click on groups of the same-colored blocks to remove them from the board. The vacant cells are filled by blocks above, and new blocks drop from the top. The game includes dynamic icon changes based on group sizes and ensures no deadlocks occur by optimizing color distribution.

#Features
- Clickable blocks with collapse/blast mechanics
- Dynamic block icons that change based on group size
- Smooth animations for block destruction and falling
- New blocks generated to avoid deadlocks

Requirements
- Unity 6 
- Basic knowledge of Unity and C#

 Project Structure

```
Assets/
|-- _scripts/
|   |-- Block.cs
|   |-- BlockColorData.cs
|   |-- BlockColorManager.cs
|   |-- BlockPool.cs
|   |-- BoardManager.cs
|   |-- CollapseManager.cs
|   |-- LevelData.cs
|-- Prefabs/
|   |-- BlockPrefab.prefab
|-- Scenes/
|   |-- mainScene.unity
    
```

How to Run
1. Clone or download the repository.
2. Open the project in Unity.
3. Assign the following scripts to their respective GameObjects:
   - **BoardManager**: Attach `BoardManager.cs`
   - **CollapseManager**: Attach `CollapseManager.cs`
   - **BlockPool**: Attach `BlockPool.cs`
   - **BlockColorManager**: Attach `BlockColorManager.cs` and configure color data.
4. Assign the `BlockPrefab` to the `BlockPool` in the Inspector.
5. Click **Play** in Unity to start the game.

Customization
- **Block Sizes and Grid:** You can adjust the number of rows, columns, and block sizes in `BoardManager.cs`.
- **Group Size Thresholds:** Modify `conditionA`, `conditionB`, and `conditionC` in `CollapseManager.cs` to set when icons change.
- **Block Colors:** Add or modify colors in `BlockColorManager` through the Inspector.



License
This project is for educational purposes.

