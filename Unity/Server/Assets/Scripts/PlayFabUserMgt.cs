using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayFabUserMgt : MonoBehaviour
{
    [Header("Registration")]
    [SerializeField] TMP_InputField userEmail;
    [SerializeField] TMP_InputField userPassword;
    [SerializeField] TMP_InputField userName;
    [SerializeField] TMP_InputField displayName;

    [Header("Result Message")]
    [SerializeField] TextMeshProUGUI Msg;

    [Header("Login")]
    [SerializeField] TMP_InputField Email;
    [SerializeField] TMP_InputField Password;

    [Header("Score")]
    [SerializeField] TMP_InputField currentScore;
    [SerializeField] TextMeshProUGUI Score;

    public void OnButtonRegUser()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Email = userEmail.text,
            Password=userPassword.text,
            Username=userName.text

        };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegSuccess, OnError);
    }

    void OnRegSuccess(RegisterPlayFabUserResult r)
    {
        Debug.Log("Register Success!");
        Msg.text = ("Register Success");

        var req = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(req, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult r)
    {
        Msg.text=("Display name Updated!" + r.DisplayName);
    }
    void OnError(PlayFabError e)
    {
        Debug.Log("Error" + e.GenerateErrorReport());
        Msg.text = ("Error" + e.GenerateErrorReport());
    }

    public void OnButtonLogin()
    {
        var loginReuqest = new LoginWithEmailAddressRequest
        {
            Email = Email.text,
            Password = Password.text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
    };
        PlayFabClientAPI.LoginWithEmailAddress(loginReuqest, OnLoginSuccess, OnError);

       
    }

    void OnLoginSuccess(LoginResult r)
    {
        Msg.text = ("Successfully LogIn");
        Debug.Log("Login Success");
    }

    void OnResetSuccess(SendAccountRecoveryEmailResult r)
    {
        Msg.text = ("Successfully Send Email Recovery");
        Debug.Log("EMail sent");
    }

    public void OnResetPassword()
    {
        var ResetPassword = new SendAccountRecoveryEmailRequest
        {
            Email = Email.text,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(ResetPassword, OnResetSuccess, OnError);
        
            
    }

    public void OnButtonGetLeaderboard()
    {
        var lbreq = new GetLeaderboardRequest
        {
            StatisticName = "highscore",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(lbreq, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult r)
    {
        string LeaderboardStr = "Leaderboard\n";
        foreach (var item in r.Leaderboard)
        {
            string onerow = item.Position + "/" + item.PlayFabId + "/" + item.DisplayName + "/" + item.StatValue + "\n";
            LeaderboardStr += onerow;
        }
        Score.text = LeaderboardStr;
    }

    public void OnButtonSendLeaderboard()
    {
        var req = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName="highscore",
                    Value=int.Parse(currentScore.text)
                }
            }
        };
        Msg.text = "Submitting Score: " + currentScore.text;
        PlayFabClientAPI.UpdatePlayerStatistics(req, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult r)
    {
        Msg.text = "Successful leaderboard sent: " + r.ToString();
    }

    public void OnButtonLogOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();
    }
    void FailureCallback(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your API call. Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
