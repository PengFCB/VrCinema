using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryOrder : MonoBehaviour
{
    public ScrollRect scrollRect;
    private float[] rateArr;
    //获取Content的RectTransform
    public RectTransform contentTransform;
    //设置添加的预制体
    public RectTransform itemTransform;
    // Start is called before the first frame update
    void Start()
    {
        /**
        scrollRect = GetComponent<ScrollRect>();
        //contentTransform = transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        if (Global.user.GetOrderList().Count != 0)
        {
            bool flag = true;
            Order order1 = (Order)Global.user.GetOrderList()[0];
            
            _ = transform.Find("Viewport").Find("Content").Find("OrderItem").
                GetComponentsInChildren<Text>()[0].text = Global.GetFilm(order1.GetFilmId()).GetFilmName();

            foreach (Order order in Global.user.GetOrderList())
            {
                if (flag)
                {
                    flag = false;
                    continue;
                }
                Transform temp = Instantiate(itemTransform).transform;
                temp.SetParent(contentTransform);
                temp.localPosition = Vector3.zero;
                temp.localRotation = Quaternion.identity;
                temp.localScale = Vector3.one;
                temp.GetComponentsInChildren<Text>()[0].text = Global.GetFilm(order.GetFilmId()).GetFilmName();
                
            }
        }
        else
        {
            _ = transform.Find("Viewport").Find("Content").Find("OrderItem").
                GetComponentsInChildren<Text>()[0].text = "No History Order";

        }
        **/
        UpdateOrderList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateOrderList()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.content.DetachChildren();

        if (Global.user.GetOrderList().Count != 0)
        {
            //ArrayList arr = new ArrayList();
            foreach (Order order in Global.user.GetOrderList())
            {
                Transform temp = Instantiate(itemTransform).transform;
                temp.SetParent(contentTransform);
                temp.localPosition = Vector3.zero;
                temp.localRotation = Quaternion.identity;
                temp.localScale = Vector3.one;
                temp.GetComponentsInChildren<Text>()[0].text = Global.GetFilm(order.GetFilmId()).GetFilmName();
            }
            
        }
        else
        {
            Transform temp = Instantiate(itemTransform).transform;
            temp.SetParent(contentTransform);
            temp.localPosition = Vector3.zero;
            temp.localRotation = Quaternion.identity;
            temp.localScale = Vector3.one;
            temp.GetComponentsInChildren<Text>()[0].text = "No History Order";
        }

    }
}


