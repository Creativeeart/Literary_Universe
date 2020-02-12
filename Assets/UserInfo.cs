using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security;
using System.Security.Cryptography;
using System.Text;
public class UserInfo : MonoBehaviour {
    public TextMeshProUGUI RealNameUI;
    public string UserName;
    public string RealName;
    public string Password;
    public string DateRegistration;
    public int Score;
    public string DateScore;
    public string DonationName;
    public bool IsLocked = false;
    public GameObject LockedIcon;
    public GameObject UnlockedIcon;

    // Use this for initialization
    void Start () {
        RealNameUI.text = RealName;
        if (Password == "guest")
        {
            IsLocked = false;
            LockedIcon.SetActive(false);
            UnlockedIcon.SetActive(true);
        }
        else
        {
            IsLocked = true;
            LockedIcon.SetActive(true);
            UnlockedIcon.SetActive(false);
        }
    }

    public void Show()
    {
        if (IsLocked)
        {
            FB_GameMenuController.Instance.ShowConfirmAfterSelectUserMenu(UserName, RealName, Password, true);
        }
        else
        {
            FB_GameMenuController.Instance.ShowConfirmAfterSelectUserMenu(UserName, RealName, Password, false);
        }
    }

    public string GetHash(string input)
    {
        var md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return System.Convert.ToBase64String(hash);
    }

}
