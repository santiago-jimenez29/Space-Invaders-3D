# Space Invaders 3D — Assignment 1

## Code Quality Fixes

**1. Removed unnecessary component lookup (Alien.cs)**

Replaced `fleet.GetComponent<AlienFleet>()` with `fleet` because the reference already existed.

**2. Removed redundant loop (AlienFleet.cs)**

Combined alien movement and boundary detection into one loop to improve efficiency.

**3. Removed hardcoded alien count (AlienFleet.cs)**

Replaced the fixed count of 30 with `initialAlienCount` so the code works with different numbers of aliens.

## Additional Features

- Aliens move faster as they are destroyed.
- Game-over detection.
- Spaceship stays within screen boundaries.
- Increased game difficulty.
- Three lives with a counter.
- Start, victory, and game-over screens.
- Restart button.
- Alien explosion effects.
