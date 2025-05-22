; From https://gbdev.io/gb-asm-tutorial/part1/hello_world.html

INCLUDE "hardware.inc"

SECTION "VBlank Interrupt", ROM0[$0040]
VBlankInterrupt:
	; This instruction is equivalent to `ret` and `ei`
	reti


SECTION "Header", ROM0[$100]

  jp EntryPoint

  ds $150 - @, 0 ; Make room for the header

EntryPoint:
  ld a, IEF_VBLANK
	ldh [rIE], a
  ei

Done:
  jp Done




SECTION "Tile data", ROM0

Tiles:
 
TilesEnd:

SECTION "Tilemap", ROM0

Tilemap:

TilemapEnd:
