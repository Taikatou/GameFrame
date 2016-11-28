EXTERNAL battle(scriptName)

===dialog===
VAR to_move = false
How did you get in here, you're not getting past if that is what you want?
*   Yes, move it.
    {battle("farmer_fight.ink")}
*   No
    Then get out of here.
    
- ->END

=== function battle(scriptName) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return 1
