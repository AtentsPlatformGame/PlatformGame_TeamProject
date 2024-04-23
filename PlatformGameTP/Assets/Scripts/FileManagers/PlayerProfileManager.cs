using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using InventorySystem;

public class PlayerProfileManager : MonoBehaviour
{
    public UnityEvent<ItemStat> updatePlayerInvenAct;
    public InventorySlot_LNH[] playerItems = new InventorySlot_LNH[7];
    string savePath;
    public Transform inventory;
    public Transform StartPos;

    [SerializeField, Header("플레이어 기본 스텟")] PlayerStatData playerStatData;
    PlayerController player;
    GoldManager goldManager;
    ItemStat[] saveItems = new ItemStat[7];
    ItemStat[] loadItems = new ItemStat[7];


    // load, load가 할 일은 저장된 플레이어의 현재 체력과 인벤토리 정보를 읽어와 플레이어에 반영한다. 반영할 때 이전에 썼던 Calculate를 써서 스텟을 적용한다.
    private void Awake()
    {
        if (SceneChanger.instance != null)
        {
            SceneChanger.instance.savePlayerProfileAct.AddListener(SavePlayerInvenProfile);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayerProfile();
    }

    public void SavePlayerInvenProfile() // save : save가 할 일은 현재 플레이어의 체력과, 플레이어의 인벤토리 정보를 저장한다.
    {
        player = FindObjectOfType<PlayerController>();
        goldManager = FindObjectOfType<GoldManager>();
        int _gold = goldManager.GetPlayerGold();
        float _curhp = player.GetCurHP(); // 현재 체력을 가져옴
        for (int i = 0; i < 7; i++)
        {
            if (playerItems[i] != null)
            {
                saveItems[i] = playerItems[i].GetItemStat();
            }
        }


        PlayerInventoryProfile playerProfile = new PlayerInventoryProfile(_curhp, _gold, saveItems);
        //PlayerInventoryProfile playerProfile = new PlayerInventoryProfile(_curhp, saveItems);

        // 데이터를 JSON 형식으로 직렬화
        string json = JsonUtility.ToJson(playerProfile);
        // JSON 파일로 저장
        savePath = SceneChanger.instance.filepath_playerProfile;
        File.WriteAllText(savePath, json);
    }

    void LoadPlayerProfile()
    {
        StartCoroutine(LoadingPlayerInvenProfile());
    }

    IEnumerator LoadingPlayerInvenProfile()
    {
        if(inventory != null)inventory.gameObject.SetActive(true);
        player = FindObjectOfType<PlayerController>();
        goldManager = FindObjectOfType<GoldManager>();
        player.gameObject.transform.position = StartPos.position;
        if(SceneChanger.instance != null) savePath = SceneChanger.instance.filepath_playerProfile;
        Debug.Log("로드 시작");
        // JSON 파일로부터 데이터 읽기
        if (File.Exists(savePath))
        {
            Debug.Log("파일 존재");
            string json = File.ReadAllText(savePath);

            // JSON을 데이터 구조로 역직렬화
            PlayerInventoryProfile playerProfile = JsonUtility.FromJson<PlayerInventoryProfile>(json);

            if (player != null)
            {
                if (playerProfile.GetPlayerCurHP() == 0) player.Initialize(1);
                else player.Initialize(playerProfile.GetPlayerCurHP());
            }
            if (goldManager != null) goldManager.SetPlayerGold(playerProfile.GetPlayerCurGold());
            //SetGold();
            Debug.Log($"읽어온 파일 체력 : {playerProfile.GetPlayerCurHP()}");
            for (int i = 0; i < 7; i++)
            {
                if (playerProfile.GetItemProperty(i).ItemType != ITEMTYPE.NONE)
                {
                    loadItems[i] = playerProfile.GetItemProperty(i);
                    Debug.Log($"읽어온 아이템 정보 : {loadItems[i]}");
                }
            }

            Debug.Log("Character position loaded from " + savePath);
            yield return StartCoroutine(UpdatingPlayerInventory());
        }
        else
        {
            Debug.LogWarning("No saved at " + savePath);
            if (player != null && playerStatData != null)
                player.Initialize(playerStatData);
            goldManager.SetPlayerGold(0);
            inventory.gameObject.SetActive(false);
            yield return null;
        }

    }

    IEnumerator UpdatingPlayerInventory()
    {
        Debug.Log("업데이트 시작");

        for (int i = 0; i < 7; i++)
        {
            if (loadItems[i].ItemType != ITEMTYPE.NONE)
            {
                updatePlayerInvenAct?.Invoke(loadItems[i]);
            }
        }
        inventory.gameObject.SetActive(false);
        yield return null;
    }

    [System.Serializable]
    public class PlayerInventoryProfile
    {
        public float playerCurHp;
        public int playerGold;
        public ItemStat[] savedInven = new ItemStat[7];

        public PlayerInventoryProfile(float _playerCurHp, int _playerGold, ItemStat[] _savedInven)
        {
            this.playerCurHp = _playerCurHp;
            this.playerGold = _playerGold;
            this.savedInven = _savedInven;
        }

        /*public PlayerInventoryProfile(float _playerCurHp,  ItemStat[] _savedInven)
        {
            this.playerCurHp = _playerCurHp;
            this.savedInven = _savedInven;
        }*/

        public float GetPlayerCurHP()
        {
            return this.playerCurHp;
        }

        public ItemStat GetItemProperty(int idx)
        {
            return this.savedInven[idx];
        }

        public int GetPlayerCurGold()
        {
            return this.playerGold;
        }
    }

}
