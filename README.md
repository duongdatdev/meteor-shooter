# Meteor Shooter

Meteor Shooter is a compact, finished 2D arcade game for Unity. Move a small starfighter along the bottom of the screen, shoot incoming asteroids, build score, and survive an increasingly fast barrage with three lives.

## Requirements

- Unity `6000.6.1f1`
- Windows, macOS, or Linux Unity Editor with the project packages restored
- Active Input Handling is set to the Input System package

## Open and run

1. Open this repository folder in Unity Hub with Unity `6000.6.1f1`.
2. Open `Assets/Scenes/MainMenu.unity`.
3. Press Play and select **PLAY**.

The build scene order is already configured as `MainMenu` then `Gameplay`.

## Controls

- `A` / `D` or Left / Right Arrow: move
- `Space`: fire (hold for repeated fire with cooldown)
- `Escape`: pause or resume

Pause and Game Over screens provide restart and Main Menu actions. The local high score is stored with `PlayerPrefs`.

## Build

Use **File > Build Profiles**, select a desktop profile, then choose **Build**. The default player resolution is 720×1280; the background and UI also adapt to other aspect ratios.

## Project structure

- `Assets/Art`: accepted production sprites and background
- `Assets/Animations`: generated animation clips/controllers
- `Assets/Prefabs`: player, projectile, three asteroids, and explosion FX
- `Assets/Scenes`: `MainMenu` and `Gameplay`
- `Assets/Scripts`: focused gameplay, player, UI, and system components
- `Assets/Tests/EditMode`: lightweight gameplay-configuration tests
- `Assets/Editor/MeteorShooterBuilder.cs`: reproducible scene/prefab/animation builder
- `ASSET_PLAN.md`: art direction, generation layouts, import contract, and QC criteria

## Art and animation workflow

Visible game art was created with the current Agent Sprite Forge `generate2dsprite` workflow. Raw sheets were generated with built-in image generation, generally against `#FF00FF`, then deterministically cleaned, split, aligned, and checked with `generate2dsprite.py`. Accepted transparent frames alone were copied into `Assets/Art`; temporary raw/processed work remains under the ignored `.sprite-forge/` workspace.

Player idle uses four frames at 8 fps. Player and asteroid explosions use six-frame one-shots at 12 and 14 fps. Asteroids use three distinct static sprites with runtime rotation. See `ASSET_PLAN.md` for the complete generation and QC contract.

## Regenerating project content

After scripts and art have imported without compile errors, run **Meteor Shooter > Build Complete Game** in the Unity Editor. This rebuilds production prefabs, animation assets, both scenes, build settings, and desktop defaults from the accepted art.

## Verification

Run EditMode tests from **Window > General > Test Runner**. The project was also checked in Play Mode for menu/HUD rendering, asteroid spawning, projectile collision and scoring, single-life damage with invulnerability, pause/resume, Game Over flow, and console errors.

## Known limitations

The game intentionally has one weapon and one survival loop. It uses simple generated visual FX and no background music; this keeps the project small and avoids third-party or copyrighted audio dependencies.
