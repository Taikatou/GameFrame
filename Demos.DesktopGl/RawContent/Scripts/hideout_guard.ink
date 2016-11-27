EXTERNAL battle(scriptName)

===dialog===
VAR to_move = false
You want some?
*   Yes
    Well here goes
    {battle("cow_fight.ink")}
*   No
    I thought so.
- ->END

=== function battle(scriptName) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return 1
