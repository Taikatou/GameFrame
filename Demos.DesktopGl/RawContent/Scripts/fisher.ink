VAR acquire_rod = false

You start talking to the fisherman
->first_branch

===first_branch===
    What brings you here young ninja
    *   I heard about a legendary fisherman
        That is me, what would you like to know
        ->second_branch
    *   I am on a quest and am lost
        Go west to exit the forest
        Go north to enter the ninja village
        Head east for another fishing spot
        ->first_branch
    *   What's it to you
        You won't get guidance with that attitude
- ->DONE

===second_branch===
* Why do you fish?
    Times are hard due to the great ninja war.
    ->second_branch
* Can I fish?
    Here take my spare fishing rod
    ~ acquire_rod = true
    You acquired one fishing rod
    Now every time you go to water you will be able to fish too
* Nothing I am fine thank you.
    What a polite young boy
- ->DONE