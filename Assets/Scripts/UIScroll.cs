using UnityEngine;

public class UIScroll : MonoBehaviour
{
    public RectTransform UIObject;
    public float speed;

    private bool allowScroll = false;

	void Start()
    {

    }
	public void AllowScroll(bool allow)
    {
        if (allowScroll == allow)
            return;
        allowScroll = allow;
        if (allowScroll)
        {
            Vector3 pos = UIObject.transform.localPosition;
            pos.y = 0;
            UIObject.transform.localPosition = pos;
        }
    }
	void Update()
    {
        if (!allowScroll)
            return;

        Vector3 pos = UIObject.transform.localPosition;
        pos.y += speed * Time.deltaTime;
        UIObject.transform.localPosition = pos;
    }
}
