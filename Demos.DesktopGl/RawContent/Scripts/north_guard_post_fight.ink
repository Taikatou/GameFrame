EXTERNAL battle(scriptName)

===dialog===
VAR to_move = false
What do you want
*   I want to to fight
    Ok then
    {battle("yo_moma.ink")}
*   Nothing
- ->END

=== function battle(scriptName) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return 1 