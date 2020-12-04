using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GroupState { PassingBy, WaitingForEntry, Entering, WaitingForTable, Dining, Leaving}
public enum PatronState { Idle, Talking, WalkingToTable, Sitting, WalkingToToilet, UsingToilet }