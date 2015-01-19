using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
    public void destroy()
    {
        Destroy(this.gameObject);
    }

    public void destroyParent()
    {
        Destroy(this.transform.parent.gameObject);
    }

    public IEnumerator destroyParentIE(float timeWait)
    {
        yield return new WaitForSeconds(timeWait);
        Destroy(this.transform.parent.gameObject);
    }

    public static void destroyChildren(GameObject objParent, params string[] except)
    {

        int childCount = objParent.transform.childCount;
        if (except == null)
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(objParent.transform.GetChild(i).gameObject);
            }
        }
        else
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject child = objParent.transform.GetChild(i).gameObject;
                int length = except.Length;
                int count = 0;
                for (int e = 0; e < length; e++)
                {
                    if (child.name != except[e])
                        count++;
                }
                if (count == length)
                    Destroy(child);
            }
        }
    }
}
