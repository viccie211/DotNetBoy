; From https://gbdev.io/gb-asm-tutorial/part1/hello_world.html

INCLUDE "hardware.inc"

SECTION "Header", ROM0[$100]

  jp EntryPoint

  ds $150 - @, 0 ; Make room for the header

EntryPoint:
  di
  nop 
  nop 
  nop
  nop 
  nop
  nop 
  nop 
  nop
  nop 
  nop  nop 
  nop 
  nop
  nop 
  nop  nop 
  nop 
  nop
  nop 






Done:
  jp Done




SECTION "Tile data", ROM0

Tiles:
 
TilesEnd:

SECTION "Tilemap", ROM0

Tilemap:

TilemapEnd:
