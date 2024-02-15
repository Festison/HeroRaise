using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : SingleTon<EventManager>
{
    public Action OnUiEvent;
}
