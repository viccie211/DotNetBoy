; From https://gbdev.io/gb-asm-tutorial/part1/hello_world.html

INCLUDE "hardware.inc"

SECTION "Header", ROM0[$100]

  jp EntryPoint

  ds $150 - @, 0 ; Make room for the header

EntryPoint:
  ; Shut down audio circuitry
  ld a, 0
  ld [rNR52], a
  ld bc, $1234
  ld de, $4321
  ld hl, $2134
  ld sp, $4321
  inc bc
  inc de
  inc hl
  inc sp
  inc a
  ld [bc], a
  ld [de], a
  ld [hl-], a
  ld [hl-], a
  ld [hl+], a
  ld [hl+], a


Done:
  jp Done


SECTION "Tile data", ROM0

Tiles:
 
TilesEnd:

SECTION "Tilemap", ROM0

Tilemap:

TilemapEnd:
