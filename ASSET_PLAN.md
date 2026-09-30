# Meteor Shooter Asset Plan

## Art direction

Clean stylized HD 2D arcade space art viewed from directly above. Shapes are compact, friendly, and instantly readable at gameplay scale. The palette uses deep navy space, a white-and-blue player ship, cyan energy, warm orange explosions, and gray-brown asteroids. Lighting, soft outlines, and saturation stay consistent across the set.

## Production set

| Asset | Raw layout | Frames | Anchor | Playback | Production output |
| --- | --- | ---: | --- | --- | --- |
| Player idle | 2x2 | 4 | center | 8 fps loop | `Assets/Art/Player/player_idle.png` |
| Player hurt | Runtime white/cyan flash using the accepted idle frames | 4-state blink | center | 12 fps during invulnerability | Uses idle sheet; no identity drift |
| Player explosion | 2x3 | 6 | center | 12 fps one-shot | `Assets/Art/FX/player_explosion.png` |
| Asteroid explosion | 2x3 | 6 | center | 14 fps one-shot | `Assets/Art/FX/asteroid_explosion.png` |
| Asteroid small/medium/large + projectile | 2x2 prop pack | 4 static cells | center | static | Individual PNGs under `Assets/Art/Asteroids` and `Assets/Art/Projectiles` |
| Space background | single 9:16 image | 1 | center | static with two-layer runtime scroll | `Assets/Art/Background/space_background.png` |

## Identity and scale rules

- Player: one canonical white hull, cobalt-blue wing accents, cyan canopy/engine, nose pointing straight up in every frame.
- Idle motion changes engine glow and at most a tiny hover compression; hull geometry, camera, center, and scale remain fixed.
- Subjects occupy roughly 60-70% of each raw cell with magenta padding on every side.
- Explosion sheets contain only one centered effect per cell, with no baked asteroid/player body after the early frames.
- Asteroid variants share the same material and lighting but have distinct silhouettes and crater patterns.
- Production gameplay sizes: player about 1.25 world units tall; asteroid diameters about 0.65/0.95/1.3 units; projectile about 0.18 x 0.48 units.

## Sprite Forge workflow

1. Generate each action family separately with built-in image generation on flat `#FF00FF`.
2. Store untouched sheets in `.sprite-forge/raw/` and prompts/metadata beside them.
3. Process with `generate2dsprite.py`: chroma cleanup, split, centered alignment, shared scale, transparent export, and strict QC.
4. Visually inspect raw and processed outputs for identity, frame order, cropping, edge contact, magenta fringe, scale drift, and accidental extra objects.
5. Copy only accepted transparent frames into `Assets/Art/` and retain processed sheets in the ignored Sprite Forge workspace for QC/reproducibility.
6. Unity importer: Sprite texture type, alpha transparency, bilinear filtering, no compression, centered pivots, 128 pixels per unit for gameplay sprites and 155 pixels per unit for the background. Animation clips reference the individually processed frames.

## QC acceptance

- Transparent output and no visible magenta/fringe.
- No source subject is cropped and no processed frame touches an output edge.
- Exactly the requested grid/frame count; left-to-right, top-to-bottom order.
- Stable player identity, orientation, camera distance, center, and hull scale.
- Explosions expand then fade naturally without crossing cell boundaries.
- Static prop cells contain exactly one isolated asset each.
- Background remains low-noise and leaves the central playfield readable.

## Workspace

Generation-only artifacts live under `.sprite-forge/{raw,processed,references,previews,metadata}` and are not production Unity assets. Only accepted PNGs are copied into `Assets/Art/`.
