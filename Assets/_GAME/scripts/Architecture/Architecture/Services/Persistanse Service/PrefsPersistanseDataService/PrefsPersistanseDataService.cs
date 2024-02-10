﻿namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service.PrefsPersistanseDataService
{
    using System;
    using UnityEngine;
    
    /// <summary>
    ///  Save player data use PlayerPrefs
    /// </summary>
    public class PrefsPersistanseDataService: IPersistanseDataService
    {
        private const string PREFS_NAME = "PLAYER_DATA";
        private GameData _data;

        protected override void Initialization()
        {
        }

        public override void SetData(GameData newValue)
        {
            _data = newValue;
        }

        public override GameData GetData()
        {
            return _data ?? LoadProgress();
        }

        public override GameData LoadProgress()
        {
            try
            {
                var jsonString = PlayerPrefs.GetString(PREFS_NAME);
                GameData progress = JsonUtility.FromJson<GameData>(jsonString)?? new GameData();
                Data = progress;
                return progress;
            }
            catch
            {
                return new GameData();
            }
        }

        public override void SaveProgress(GameData gameData)
        {
            try
            {
                string jsonString = JsonUtility.ToJson(gameData);
                Data = gameData;
                PlayerPrefs.SetString(PREFS_NAME, jsonString);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}