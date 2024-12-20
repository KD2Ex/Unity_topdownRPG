﻿using System;
using System.Collections;
using UnityEngine;

public class Coroutines
{
    public static IEnumerator MoveTo(Transform to, Rigidbody2D rb, float speed)
    {
        var waiter = new WaitForFixedUpdate();
        
        var elapsed = 0f;
        float dist;
        
        do
        {
            dist = ( (Vector2) to.position - rb.position).magnitude;
            var dir = Vector3.Normalize( (Vector2) to.position - rb.position);

            
            rb.MovePosition(rb.position + (Vector2) dir * (speed * Time.fixedDeltaTime));

            yield return waiter;
        } while (dist > .1f);
    }

    public static IEnumerator WaitFor(float seconds, Action before = null, Action after = null)
    {
        before?.Invoke();
        yield return new WaitForSeconds(seconds);
        after?.Invoke();
    }

    public static IEnumerator WaitForRealTimeSeconds(float seconds, Action after = null)
    {
        yield return new WaitForSecondsRealtime(seconds);
        after?.Invoke();
    }
}