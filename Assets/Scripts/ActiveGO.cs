using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveGO : MonoBehaviour
{
    public List<GameObject> HideList;
    public List<GameObject> showList;
    private void Awake()
    {
        var btn = GetComponent<Button>();
        if (btn == null) 
        {
            btn = gameObject.AddComponent<Button>();
        }
        btn.onClick.AddListener(()=> {
            foreach (var hide in HideList) 
            {
                hide.SetActive(false);
                Debug.Log("hide");
            }
            foreach (var show in showList)
            {
                show.SetActive(true);
            }
        });
    }
}
