EXTERNAL battle(scriptName)

Hello Taikatou,
Do you not recogise me?
*   You are the dojo master.
    Very well done.
    ->pre_fight
*   No not at all.
    Hahaha how foolish
    I am the dojo master.
    ->pre_fight
- ->END

===pre_fight===
I kidnapped the princess, she is an important player in the politics of this war. We tricked her by communicating with her for peace. I need you to understand that we did this to save the village, having her as a hostage will improve our relations with our allies and we can use her to bargain.
However I don't think you are happy with that
*   Attack him
    ->final_battle
*   Slap him
    ->final_battle
- ->DONE

===final_battle===
{battle("final_battle.ink")}
- ->DONE

=== function battle(scriptName) ===
// Usually external functions can only return placeholder
// results, otherwise they'd be defined in ink!
~ return 1
