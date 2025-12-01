using UnityEngine;
using TMPro;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;
using UnityEngine.UI;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_Text pageNo;
        [SerializeField] private Button nextPageButt;
        [SerializeField] private Button prevPageButt;
        [SerializeField] private int maxPages;

        // ------------------------------------------------------------
        private int highScore;
        private string userName;
        private int curPageNum = 1;
        private int totalPages = 1;
        // ------------------------------------------------------------

        private void Start()
        {
            highScore = PlayerPrefs.GetInt("Highscore", 0);
            userName = PlayerPrefs.GetString("Name", "nullName");
            LoadEntries(1);
        }

        private void LoadEntries(int page)
        {
            curPageNum = page;
            foreach (var t in _entryTextObjects)
                t.text = "";
            nextPageButt.interactable = false;
            prevPageButt.interactable = false;
            pageNo.text = $"{curPageNum} of {totalPages}";
            Leaderboards.Sinful.GetEntries(entries =>
            {
                // Sort by score ASCENDING (lower = better)
                System.Array.Sort(entries, (a, b) => a.Score.CompareTo(b.Score));

                totalPages = Mathf.Min(
                    (int)Mathf.Ceil((float)entries.Length / (float)_entryTextObjects.Length),
                    maxPages
                );

                var length = Mathf.Min(_entryTextObjects.Length, entries.Length - (curPageNum - 1) * _entryTextObjects.Length);

                for (int i = 0; i < length; i++)
                {
                    int realIndex = i + (curPageNum - 1) * _entryTextObjects.Length;

                    _entryTextObjects[i].text =
                        $"{realIndex + 1}- {entries[realIndex].Username} - {entries[realIndex].Score}";
                }

                pageNo.text = $"{curPageNum} of {totalPages}";
                nextPageButt.interactable = curPageNum < totalPages;
                prevPageButt.interactable = curPageNum > 1;
            });}

        public void UploadEntry()
        {
            Leaderboards.Sinful.UploadNewEntry(userName, highScore, isSuccessful =>
            {
                if (isSuccessful)
                    LoadEntries(1);
            });
        }

        public void NextPage()
        {
            LoadEntries(curPageNum + 1);
                UploadEntry();
        }

        public void PrevPage()
        {
            LoadEntries(curPageNum - 1);
        }
    }
}
