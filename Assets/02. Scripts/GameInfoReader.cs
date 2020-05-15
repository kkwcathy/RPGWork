using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class GameInfoReader
{
	public enum InfoType
	{
		Team,
		Map,
		Model,
		Attack,
	}

	delegate void AddDicHandler(string[] keys);
	private AddDicHandler AddDic;

	public void CSVFileOpen(InfoType infoType)
	{
		StringBuilder filePath = new StringBuilder();

		if(Application.platform == RuntimePlatform.Android)
		{
			filePath.Append(Application.persistentDataPath);
		}
		else
		{
			filePath.Append(Application.dataPath);
		}

		filePath.Append("/CSV/");

		switch (infoType)
		{
			case InfoType.Team:
				AddDic = InfoManager.Instance.AddTeamDic;
				filePath.Append("PlayerDocument.csv");
				break;

			case InfoType.Map:
				AddDic = InfoManager.Instance.AddMapDic;
				filePath.Append("MapDocument.csv");
				break;

			case InfoType.Model:
				AddDic = InfoManager.Instance.AddModelDic;
				filePath.Append("ModelDocument.csv");
				break;

			case InfoType.Attack:
				AddDic = InfoManager.Instance.AddAttackDic;
				filePath.Append("AttackDocument.csv");
				break;

			default:
				Debug.Log("File open fail");
				break;
		}
		
		StreamReader sr = new StreamReader(filePath.ToString());
		string line = sr.ReadLine(); // 첫째 라인은 안읽으므로 미리 한번 읽어줌

		while (line != null)
		{
			line = sr.ReadLine();

			if (string.IsNullOrEmpty(line))
			{
				break;
			}

			AddDic(line.Split(','));
		}
	}

	public void ReadGameInfo()
    {
		CSVFileOpen(InfoType.Team);
		CSVFileOpen(InfoType.Model);
		CSVFileOpen(InfoType.Attack);
		CSVFileOpen(InfoType.Map);

		InfoManager.Instance.MapID = 100;

		Debug.Log("File Open Complete");

		//GameObject.Find("Team").SendMessage("Generate");

		//GameObject.Find("WaveController").SendMessage("StartGame");

		//SetText();
		//GenPlayer();
	}

	//void SetText()
	//{
	//	GameObject.Find("CharView").SendMessage("SetText");
	//}

	//void GenPlayer()
	//{
	//	GameObject.Find("GeneratePractice").SendMessage("SibalLetsGeneratePlayers");
	//}

	//private void TeamFileOpen()
	//{
	//	string fileName = Application.dataPath + "/CSV/PlayerDocument.csv";

	//	StreamReader sr = new StreamReader(fileName);
	//	string line = sr.ReadLine();

	//	while (line != null)
	//	{
	//		line = sr.ReadLine();

	//		if (string.IsNullOrEmpty(line))
	//		{
	//			break;
	//		}

	//		InfoManager.Instance.AddTeamDic(line.Split(','));
	//	}
	//}

	//private void ModelFileOpen()
	//{
	//	string fileName = Application.dataPath + "/CSV/ModelDocument.csv";

	//	StreamReader sr = new StreamReader(fileName);
	//	string line = sr.ReadLine();

	//	while (line != null)
	//	{
	//		line = sr.ReadLine();

	//		if (string.IsNullOrEmpty(line))
	//		{
	//			break;
	//		}

	//		InfoManager.Instance.AddModelDic(line.Split(','));
	//	}
	//}

	//private void AttackFileOpen()
	//{
	//	string fileName = Application.dataPath + "/CSV/AttackDocument.csv";

	//	StreamReader sr = new StreamReader(fileName);
	//	string line = sr.ReadLine();
	//	//string effect = null;

	//	while (line != null)
	//	{
	//		line = sr.ReadLine();

	//		if (string.IsNullOrEmpty(line))
	//			break;
		
	//		InfoManager.Instance.AddAttackDic(line.Split(','));
	//		//effect = split[split.Length - 1];

	//	}

	//	//Debug.Log("DONE");
	//	//Debug.Log(effect);

	//	//string effectPrefab = "Prefabs/Effects/" + effect;
	//	//GameObject obj = Resources.Load(effectPrefab) as GameObject;

	//	//Instantiate(obj, transform);
	//}

}
