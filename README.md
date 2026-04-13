[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/ozVFrFMv)
### Build and run
```bash
dotnet build
dotnet run --project src/Minesweeper.Console
```
## Tests
```bash
dotnet test
```

## Rules
Classic Minesweeper rules!
tap a square!
The number on the tile is the number of adjacent mines (1-8) (Including diagonals)
A "." means an empty tile and no adjacent mines
Flagging a square to mark tiles that have mines on them!
This will prevent accidentally hitting the mines

## How To Play
First choose the size (8x8, 12x12, or 16x16)
Enter a seed (Or nothing for a seed based off current time)
Seed Predetermines the mine placement
To reveal a tile: r row col
To flag a tile: f row col
To quit: q

## Tiles
Hidden: #
Empty: .
Mine: b
Flag: f
Numbers 1-8 for adjacent mines
