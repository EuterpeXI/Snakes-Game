using System.Collections;
using UnityEngine;

class TimeUtilites
{
    /// <summary>
    /// Use this to start a coroutine that will wait before doing some action.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="waitTime">Seconds spent waiting before action</param>
    /// <example>
    /// hasCooldown = true;
    /// StartCoroutine(WaitToDoAction(() =>{
    ///     hasCooldown = false;
    /// },cooldownTime));
    /// </example>
    public static IEnumerator WaitToDoAction(System.Action action, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    
}
