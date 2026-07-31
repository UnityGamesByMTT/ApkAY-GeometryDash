using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;


public class FireBaseLeaderBoard : MonoBehaviour
{

    FirebaseFirestore db;

    [SerializeField] TMP_Text RankTxt;
    [SerializeField] TMP_InputField NameInput;
    [SerializeField] Button SaveBtn;
    [SerializeField] GameObject LeaderboardCard;
    [SerializeField] Transform cardParent;
    private LeaderboardCard lastCard;
    // This function will be called on start to initialize Firebase.

    private int lastRank, lastScore;
    void OnEnable()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                db = FirebaseFirestore.DefaultInstance; // ✅ FIX: assign db properly here
                Debug.Log("Firebase Initialized!");

                if (!string.IsNullOrEmpty(PlayerPrefs.GetString("UserName")))
                {
                    NameInput.text = PlayerPrefs.GetString("UserName");
                    _setData(); // ✅ Only safe after db is assigned
                }

                LoadTopUsers(); // ✅ Also safe after db is assigned
            }
            else
            {
                Debug.LogError("Could not resolve Firebase dependencies: " + task.Result);
            }
        });
    }
    
    public void LoadTopUsers(int leaderboardSize = 120)
    {
        db.Collection("users")
          .OrderByDescending("score")
          .Limit(leaderboardSize)
          .GetSnapshotAsync()
          .ContinueWithOnMainThread(task =>
          {
              if (task.IsFaulted)
              {
                  Debug.LogError("Error fetching leaderboard: " + task.Exception);
                 // ShowErrorUI("Failed to load leaderboard. Please try again.");
                  return;
              }

          // Clear old cards
          foreach (Transform child in cardParent)
              {
                  Destroy(child.gameObject);
              }

              int rank = 1;
              LeaderboardCard lastCard = null;

              foreach (DocumentSnapshot doc in task.Result.Documents)
              {
                  var data = doc.ToDictionary();
                  if (!data.TryGetValue("name", out object nameObj) ||
                      !data.TryGetValue("score", out object scoreObj))
                  {
                      Debug.LogWarning($"Skipping invalid user data in doc {doc.Id}");
                      continue;
                  }

                  string name = nameObj.ToString();
                  if (!int.TryParse(scoreObj.ToString(), out int score))
                  {
                      Debug.LogWarning($"Invalid score for user {name}");
                      continue;
                  }

                  //     Debug.Log($"Rank #{rank}: {name} - {score}");

                  if (lastScore != score)
                  {
                      lastRank = rank;
                      lastScore = score;
                  }
                  if (rank % 2 == 1 || lastCard == null)
                  {
                      GameObject obj = Instantiate(LeaderboardCard, cardParent);
                      lastCard = obj.GetComponent<LeaderboardCard>();
                      lastCard.SetDataCardOne(lastRank, name, score.ToString());
                  }
                  else
                  {
                      lastCard.SetDataCardTwo(lastRank, name, score.ToString(), true);
                  }

                  rank++;
                  
              }
          });
    }

    public void _setData()
    {
        if (!string.IsNullOrEmpty(NameInput.text))
        {
            PlayerPrefs.SetString("UserName", NameInput.text);

            string deviceID = GetDeviceID();

            DocumentReference docRef = db.Collection("users").Document(deviceID);

            Dictionary<string, object> user = new Dictionary<string, object>
            {
                { "name", NameInput.text },
                { "score", PlayerPrefs.GetInt("TotalScore") }
            };

            docRef.SetAsync(user).ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && !task.IsFaulted)
                {
                    Debug.Log("User data saved/updated in Firestore!");
                }
                else
                {
                    Debug.LogError("Failed to write to Firestore: " + task.Exception);
                }
            });

            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCompleted && task.Result.Exists)
                {
                    Dictionary<string, object> data = task.Result.ToDictionary();
                    Debug.Log("Fetched from Firestore:");
                    Debug.Log("User name: " + data["name"]);
                    Debug.Log("Score: " + data["score"]);
                   
                }
            });
        }
        GetCurrentUserRank(GetDeviceID());
    }

    private string GetDeviceID()
    {
        if (PlayerPrefs.HasKey("DeviceID"))
        {
            return PlayerPrefs.GetString("DeviceID");
        }
        else
        {
            string newID = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("DeviceID", newID);
            return newID;
        }
    }   
    void GetCurrentUserRank(string userId)
    {
        DocumentReference docRef = db.Collection("users").Document(userId);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.Result.Exists)
            {
                Debug.LogWarning("User not found.");
                return;
            }

            var userData = task.Result.ToDictionary();
            int currentUserScore = int.Parse(userData["score"].ToString());

            // Count how many users have a higher score
            db.Collection("users")
              .WhereGreaterThan("score", currentUserScore)
              .GetSnapshotAsync()
              .ContinueWithOnMainThread(rankTask =>
              {
                  if (rankTask.IsFaulted)
                  {
                      Debug.LogError("Error fetching user rank: " + rankTask.Exception);
                      return;
                  }

                  int usersAbove = rankTask.Result.Count;
                  int currentUserRank = usersAbove + 1;
                  RankTxt.text = currentUserRank.ToString();
                 // Debug.Log($"Current user rank: #{currentUserRank}");
              });
        });
    }

}

