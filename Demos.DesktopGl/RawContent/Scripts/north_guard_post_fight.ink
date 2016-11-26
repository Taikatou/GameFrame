EXTERNAL battle(scriptName)

VAR to_fight = false
What do you want
*   I want to to fight
    Ok then
    {battle("yo_moma.ink")}
    ~ to_fight = true
*   Nothing
- ->END

=== function battle(scriptName) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return 1 