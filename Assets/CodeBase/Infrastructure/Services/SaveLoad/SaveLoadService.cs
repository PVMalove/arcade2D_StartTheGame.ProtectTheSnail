using System.IO;
using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly string _filePath;

        public  SaveLoadService(IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _filePath = $"{Application.persistentDataPath}/Save.json";
        }
        
        public void SaveProgress()
        {
            string json = JsonUtility.ToJson(_progressService.Progress);

            using (StreamWriter writer = new(_filePath))
            {
                writer.Write(json);
            }
        }

        public PlayerProgress LoadProgress()
        {
            string json = "";

            if (!File.Exists(_filePath))
                return null;

            using (StreamReader reader = new(_filePath))
            {
                string line;

                while ((line = reader.ReadLine()) != null) 
                    json += line;

                if (string.IsNullOrEmpty(json))
                    return null;

                return JsonUtility.FromJson<PlayerProgress>(json);
            }
        }
    }
}