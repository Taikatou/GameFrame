VAR move_bandit = false
VAR fwacked = false

Why hello you must be Taikatou
Thank you for coming out to meet me
I heard there were bandits and thought and escort would be the safest way into the village
Are you the escort the village sent?
*   Yes
    ~ move_bandit = true
    Guard: I don't think so, I will be taking the princess now.
    Fwack!!!
    ~ fwacked = true
    The fiend kidnapped the princess and fled to the north.
    Darn it.
*   No
    Ok then I will wait here until the escort arrives
- ->END